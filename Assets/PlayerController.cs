using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerController : MonoBehaviour
{
    public PlayerData playerData;
    private Animator anim;
    private Rigidbody rigid;
    public Transform skill1Pos;

    //public GameObject skill1SwordEffect;
    private Vector3 moveVec;

    private float fireDelay;

    private int fireCount;
    private int jumpCount;
    private float hAxis;
    private float vAxis;
    public float trailOnTime = 0.5f;
    public float trailOffTime = 0.2f;

    //public float i = 0.4f;
    private bool fDown;

    private bool wDown;
    private bool jDown;
    private bool isFireReady;
    private bool isFireReady2;
    private bool isJump;
    private bool isSkill1;

    //public TrailRenderer trailRenderer;
    public GameObject hitEffect;

    public GameObject shorkWaveEffect;
    public GameObject flyingSlash;

    public GameObject fireYellowEffect;
    private Vector3 camPos;
    /*    [SerializeField] [Range(0.01f, 0.1f)] */
    private float shakeRange = 0.05f;
    /*[SerializeField] [Range(0.1f, 1f)] */
    private float duration = 0.5f;
    private Camera cam;

    //public GameObject sword8;

    private Renderer skill1SwordRenderer;

    //bool isBorder;
    //public float timed;

    private void Start()
    {
        playerData = GameObject.Find("GameManager").GetComponent<PlayerData>();
        anim = GetComponentInChildren<Animator>();
        rigid = GetComponent<Rigidbody>();
        skill1Pos = transform.GetChild(0).GetComponent<Transform>();

        //trailRenderer.emitting = true;
        //임시
    }

    private void Update()
    {
        //timed += Time.deltaTime;
        if (isSkill1)
            return;
        GetInput();
        Move();
        Attack();
        Turn();
        Jump();
        Swap();

        Skill1();
    }

    private void Skill1()
    {
        if (playerData.equipWeapon != playerData.hasWeapon[0] && Input.GetKeyDown(KeyCode.R) && !isSkill1)
        {
            StartCoroutine(Skill1Co());
        }
    }

    public void Skill1Shake()
    {
        cam = playerData.curentCam.GetComponent<Camera>();

        camPos = cam.transform.position;
        InvokeRepeating("StartShake", 0f, 0.02f); //바로 스타트쉐이크를 시작하며 0.005f마다 실행
        Invoke("StopShake", 0.3f);
    }

    public void Skill1Shake2()//테스트
    {
        cam = playerData.curentCam.GetComponent<Camera>();

        camPos = cam.transform.position;
        InvokeRepeating("Skill1StartShake", 0f, 0.001f); //바로 스타트쉐이크를 시작하며 0.005f마다 실행
        Invoke("Skill1StopShake", 0.15f);
    }

    private void Skill1StartShake()
    {
        float camPosX = Random.value * shakeRange * 2.5f - shakeRange;
        float camPosY = Random.value * shakeRange * 2.5f - shakeRange;
        camPos = cam.transform.position;
        camPos.x += camPosX;
        camPos.y += camPosY;
        cam.transform.position = camPos;
    }

    private void Skill1StopShake()
    {
        CancelInvoke("Skill1StartShake");
        //cam.transform.position = camPos;
    }

    public GameObject OnEffect;

    private IEnumerator Skill1Co()
    {
        isSkill1 = true;
        Color originC = skill1SwordRenderer.material.color;
        anim.Play("Skill01");

        yield return new WaitForSeconds(0.45f);

        playerData.equipWeaponTrail.emitting = true;
        yield return new WaitForSeconds(0.233f);

        playerData.equipWeaponTrail.emitting = false;
        yield return new WaitForSeconds(0.5f);
        fireYellowEffect.SetActive(true);
        ////
        ////기모으기 파티클 켜기
        yield return new WaitForSeconds(0.2f);
        StartCoroutine(SwordEffectCo());
        Instantiate(shorkWaveEffect, transform.position, transform.rotation);

        skill1SwordRenderer.material.color = Color.yellow;

        yield return new WaitForSeconds(0.8f);
        Skill1Shake();

        yield return new WaitForSeconds(0.3f);

        playerData.equipWeaponTrail.emitting = true;
        yield return new WaitForSeconds(0.2f);
        Skill1Shake2();

        Instantiate(OnEffect, transform.position, transform.rotation);
        Instantiate(flyingSlash, skill1Pos.position, transform.rotation);
        yield return new WaitForSeconds(0.15f);
        playerData.equipWeaponTrail.emitting = false;
        skill1SwordRenderer.material.color = originC;
        fireYellowEffect.SetActive(false);
        yield return new WaitForSeconds(0.6f);
        isSkill1 = false;
    }

    //public int tenum = 30;

    private IEnumerator SwordEffectCo()
    {
        Renderer renderer = playerData.equipWeaponEffect.GetComponent<Renderer>();

        playerData.equipWeaponEffect.SetActive(true);
        int effectI = 10;
        Color c = renderer.material.color;
        while (effectI > 0)
        {
            effectI -= 1;
            float f = effectI / 10.0f;

            c.a = f;
            renderer.material.color = c;
            playerData.equipWeaponEffect.transform.localScale += new Vector3(0.4f, 0.4f, 0.4f);
            yield return new WaitForSeconds(0.015f);
        }

        c.a = 1;
        playerData.equipWeaponEffect.transform.localScale = new Vector3(1, 1, 1);
        playerData.equipWeaponEffect.SetActive(false);
    }

    private void Swap()
    {
        int weaponNum = 0;
        if (Input.GetKeyDown(KeyCode.Alpha1)) weaponNum = 1;

        if (Input.GetKeyDown(KeyCode.Alpha2)) weaponNum = 2;

        if (Input.GetKeyDown(KeyCode.Alpha3)) weaponNum = 3;

        if (Input.GetKeyDown(KeyCode.Alpha4)) weaponNum = 4;

        if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Alpha3) || Input.GetKeyDown(KeyCode.Alpha4))
        {
            if (playerData.hasWeapon[weaponNum] == null)
                return;
            IEnumerator swapAnimCoroutine;
            swapAnimCoroutine = SwapTrail();
            playerData.equipWeapon.SetActive(false);

            playerData.equipWeapon = (playerData.equipWeapon == playerData.hasWeapon[weaponNum]) ? playerData.hasWeapon[0] : playerData.hasWeapon[weaponNum];
            if (playerData.equipWeapon != playerData.hasWeapon[0]) // 현재 착용 무기가 널무기가 아닐 때
            {
                anim.Play("Swap");
                playerData.equipWeaponTrail = playerData.equipWeapon.GetComponentInChildren<TrailRenderer>();
                playerData.equipWeaponEffect = playerData.equipWeapon.transform.GetChild(0).gameObject;
                skill1SwordRenderer = playerData.equipWeapon.GetComponent<Renderer>();

                playerData.equipWeaponEffect.SetActive(false);

                StopCoroutine(swapAnimCoroutine);
                StartCoroutine(swapAnimCoroutine);
            }
            else anim.Play("SwapRe");//현재 착용 무기가 널무기일 때
        }
    }

    private IEnumerator SwapTrail()
    {
        playerData.equipWeaponTrail.emitting = true;
        yield return new WaitForSeconds(0.3f);
        playerData.equipWeaponTrail.emitting = false;
    }

    private void GetInput()
    {
        hAxis = Input.GetAxisRaw("Horizontal");
        vAxis = Input.GetAxisRaw("Vertical");
        //wDown = Input.GetButton("Walk");
        jDown = Input.GetButtonDown("Jump");
        fDown = Input.GetButton("Fire1");
        //dDown = Input.GetButton("Dodge");
        //iDown = Input.GetButton("Interation");
    }

    public void Shake()
    {
        cam = playerData.curentCam.GetComponent<Camera>();

        camPos = cam.transform.position;
        InvokeRepeating("StartShake", 0f, 0.005f); //바로 스타트쉐이크를 시작하며 0.005f마다 실행
        Invoke("StopShake", duration);
    }

    private void StartShake()
    {
        float camPosX = Random.value * shakeRange * 2 - shakeRange;
        float camPosY = Random.value * shakeRange * 2 - shakeRange;
        camPos = cam.transform.position;
        camPos.x += camPosX;
        camPos.y += camPosY;
        cam.transform.position = camPos;
    }

    private void StopShake()
    {
        CancelInvoke("StartShake");
        //cam.transform.position = camPos;
    }

    public void GetHit(int hitDamage, Transform hitPos)
    {
        Shake();
        playerData.curentHp -= hitDamage;

        Debug.Log("데미지 입음" + hitDamage);
        if (!isSkill1)
        {
            anim.Play("GetHit");
            Instantiate(hitEffect, hitPos.position, transform.rotation);
        }
    }

    private void Jump()
    {
        if (jDown && jumpCount < 2)
        {
            jumpCount++;

            rigid.AddForce(Vector3.up * playerData.playerJumpPower, ForceMode.Impulse);
            anim.SetBool("isJump", true);
            anim.SetTrigger("doJump");
            //isJump = true;
        }
    }

    private void Move()
    {
        moveVec = new Vector3(hAxis, 0, vAxis).normalized;

        transform.position += moveVec * (wDown ? 0.2f : 1f) * playerData.playerSpeed * Time.deltaTime;
        anim.SetBool("isRun", moveVec != Vector3.zero);
    }

    private void Attack()
    {
        if (fireDelay < 10)
            fireDelay += Time.deltaTime;

        isFireReady = playerData.equipWeaponRate < fireDelay;
        if (playerData.equipWeaponRate < fireDelay)
            isFireReady2 = false;

        if (fDown)
        {
            if (playerData.equipWeapon == playerData.hasWeapon[0]) return;
            IEnumerator trailon;
            trailon = TrailOn();
            if (isFireReady2 && fireDelay > 0.5f) //공격 2
            {
                //StopCoroutine(trailon);
                StartCoroutine(TrailOn2());
                anim.SetBool("isSwing2", true);
                isFireReady2 = false;
                fireDelay = -playerData.equipWeaponRate;
            }
            else if (isFireReady) //공격 1
            {
                //StopCoroutine(trailon);
                StartCoroutine(trailon);
                anim.SetTrigger("doSwing1");
                anim.SetBool("isSwing2", false);
                fireDelay = 0;
                isFireReady2 = true;
            }
        }
    }

    private IEnumerator TrailOn()
    {
        yield return new WaitForSeconds(trailOnTime);
        playerData.equipWeaponBoxColl.enabled = true;
        playerData.equipWeaponTrail.emitting = true;
        yield return new WaitForSeconds(trailOffTime);
        playerData.equipWeaponTrail.emitting = false;
        playerData.equipWeaponBoxColl.enabled = false;
    }

    private IEnumerator TrailOn2()
    {
        yield return new WaitForSeconds(trailOnTime + 0.4f);
        playerData.equipWeaponBoxColl.enabled = true;

        playerData.equipWeaponTrail.emitting = true;
        yield return new WaitForSeconds(trailOffTime);
        playerData.equipWeaponTrail.emitting = false;

        playerData.equipWeaponBoxColl.enabled = false;
    }

    private void Turn()
    {
        transform.LookAt(transform.position + moveVec);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            anim.SetBool("isJump", false);
            //isJump = false;
            jumpCount = 0;
        }
    }

    //private void StopToWall()
    //{
    //    Debug.DrawRay(transform.position, transform.forward * 1, Color.green);
    //    isBorder = Physics.Raycast(transform.position, transform.forward, 1, LayerMask.GetMask("Wall"));
    //}
}