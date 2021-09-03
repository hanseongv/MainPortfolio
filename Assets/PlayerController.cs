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
    public GameObject skill1Sword8Effect;
    private Vector3 moveVec;

    public float fireDelay;

    private int fireCount;
    private int jumpCount;
    private float hAxis;
    private float vAxis;

    private bool fDown;
    private bool wDown;
    private bool jDown;
    private bool isFireReady;
    private bool isFireReady2;
    private bool isJump;
    private bool isSkill1;
    public TrailRenderer trailRenderer;
    public GameObject hitEffect;
    private Vector3 camPos;
    [SerializeField] [Range(0.01f, 0.1f)] private float shakeRange = 0.05f;
    [SerializeField] [Range(0.1f, 1f)] private float duration = 0.5f;
    private Camera cam;

    public GameObject sword8;

    public Renderer sword8Mat;

    //bool isBorder;
    public float timed;

    private void Start()
    {
        playerData = GameObject.Find("GameManager").GetComponent<PlayerData>();
        anim = GetComponentInChildren<Animator>();
        rigid = GetComponent<Rigidbody>();
        skill1Pos = transform.GetChild(0).GetComponent<Transform>();

        //trailRenderer.emitting = true;
        //임시
        sword8Mat = sword8.GetComponent<Renderer>();
    }

    // Update is called once per frame
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
        //Skill();
        Test();
    }

    private void Test()
    {
        if (/*playerData.equipWeapon == sword8 &&*/ Input.GetKeyDown(KeyCode.R) && !isSkill1)
        {
            StartCoroutine(tes());
        }
    }

    public float i = 0.4f;

    //private IEnumerator tes()
    //{
    //    isSkill1 = true;
    //    anim.Play("Skill01"); //0프레임
    //    Debug.Log(Time.time);
    //    yield return new WaitForSeconds(i);//트레일 시작
    //    Debug.Log(Time.time);
    //    playerData.equipWeaponTrail.emitting = true;
    //    yield return new WaitForSeconds(0.2f);
    //    playerData.equipWeaponTrail.emitting = false;
    //    yield return new WaitForSeconds(0.183f);
    //    //기모으기 켜기
    //    Debug.Log("기모으기 킴");
    //    yield return new WaitForSeconds(0.2f);
    //    sword8Mat.material.color = Color.yellow;
    //    yield return new WaitForSeconds(0.517f);
    //    //기모으기 끄기
    //    Debug.Log("기모으기 끔");
    //    yield return new WaitForSeconds(0.18f);
    //    //화면 흔들기 약하게 켜기
    //    Debug.Log("화면 흔들기 킴");
    //    yield return new WaitForSeconds(0.12f);
    //    //화면 흔들기 약하게 끄기
    //    Debug.Log("화면 흔들기 끔");
    //    playerData.equipWeaponTrail.emitting = true;
    //    yield return new WaitForSeconds(0.13f);
    //    //참격 오브젝트 생성
    //    Debug.Log("참격 날림");
    //    yield return new WaitForSeconds(0.13f);
    //    playerData.equipWeaponTrail.emitting = false;
    //    sword8Mat.material.color = Color.white;
    //    isSkill1 = false;
    //}
    public GameObject flyingSlash;

    private IEnumerator tes()
    {
        isSkill1 = true;
        anim.Play("Skill01");
        Debug.Log("애니메이션 시작" + Time.time);
        yield return new WaitForSeconds(0.45f);
        Debug.Log("트레일 시작" + Time.time);
        playerData.equipWeaponTrail.emitting = true;
        yield return new WaitForSeconds(0.233f);
        Debug.Log("트레일 끝" + Time.time);
        playerData.equipWeaponTrail.emitting = false;
        yield return new WaitForSeconds(0.5f);

        ////
        ////기모으기 파티클 켜기
        yield return new WaitForSeconds(0.2f);
        StartCoroutine(SwordEffectCo());

        sword8Mat.material.color = Color.yellow;
        //색 변환 시 두근! 하는 효과 주기 예) 카메라 흔들기, 무기 커지면서 사라지기
        yield return new WaitForSeconds(0.8f);

        //화면 흔들기 약하게 켜기
        Debug.Log("화면 흔들기 킴");
        yield return new WaitForSeconds(0.3f);
        ////화면 흔들기 약하게 끄기
        Debug.Log("화면 흔들기 끔");
        playerData.equipWeaponTrail.emitting = true;
        yield return new WaitForSeconds(0.2f);
        Instantiate(flyingSlash, skill1Pos.position, transform.rotation);
        //  Instantiate(hitEffect, hitPos.position, transform.rotation);
        Debug.Log("참격 날림");
        yield return new WaitForSeconds(0.15f);
        playerData.equipWeaponTrail.emitting = false;
        sword8Mat.material.color = Color.white;

        isSkill1 = false;
    }

    public int tenum = 30;

    private IEnumerator SwordEffectCo()
    {
        Renderer renderer = playerData.equipWeaponEffect.GetComponent<Renderer>();

        playerData.equipWeaponEffect.SetActive(true);
        int i = 10;
        Color c = renderer.material.color;
        while (i > 0)
        {
            i -= 1;
            float f = i / 10.0f;

            c.a = f;
            renderer.material.color = c;
            playerData.equipWeaponEffect.transform.localScale += new Vector3(0.4f, 0.4f, 0.4f);
            yield return new WaitForSeconds(0.015f);
        }

        c.a = 1;
        playerData.equipWeaponEffect.transform.localScale = new Vector3(1, 1, 1);
        playerData.equipWeaponEffect.SetActive(false);
        //for (int i = tenum; 0 > i; i--) //i=30 0>30
        //{
        //    playerData.equipWeaponEffect.transform.localScale += new Vector3(0.1f, 0.1f, 0.1f); // 커지게
        //    c.a = 0;
        //    renderer.material.color = c;
        //    //c.a = i * (100 / tenum);
        //    //renderer.material.color = c;
        //    yield return null;
        //}
    }

    //private void Swap()
    //{
    //    int weaponNum = 0;
    //    //if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKey(KeyCode.Alpha2) || Input.GetKey(KeyCode.Alpha3) || Input.GetKey(KeyCode.Alpha4))
    //    //{
    //    if (Input.GetKeyDown(KeyCode.Alpha1))
    //    {
    //        anim.Play("Swap");
    //        weaponNum = 1;
    //        playerData.equipWeapon.SetActive(false);
    //        playerData.equipWeapon = (playerData.equipWeapon == playerData.hasWeapon[weaponNum]) ? playerData.hasWeapon[0] : playerData.hasWeapon[weaponNum];
    //        if (playerData.equipWeapon != playerData.hasWeapon[0])
    //        {
    //            playerData.equipWeaponTrail = playerData.equipWeapon.GetComponentInChildren<TrailRenderer>();
    //            StopCoroutine(swapAnimCoroutine);
    //            StartCoroutine(swapAnimCoroutine);
    //        }
    //    }
    //    if (Input.GetKeyDown(KeyCode.Alpha2))
    //    {
    //        anim.Play("Swap");
    //        weaponNum = 2;
    //        playerData.equipWeapon.SetActive(false);
    //        playerData.equipWeapon = (playerData.equipWeapon == playerData.hasWeapon[weaponNum]) ? playerData.hasWeapon[0] : playerData.hasWeapon[weaponNum];
    //        if (playerData.equipWeapon != playerData.hasWeapon[0])
    //        {
    //            playerData.equipWeaponTrail = playerData.equipWeapon.GetComponentInChildren<TrailRenderer>();
    //            StopCoroutine(swapAnimCoroutine);
    //            StartCoroutine(swapAnimCoroutine);
    //        }
    //    }
    //    if (Input.GetKeyDown(KeyCode.Alpha3))
    //    {
    //        anim.Play("Swap");
    //        weaponNum = 3;
    //        playerData.equipWeapon.SetActive(false);
    //        playerData.equipWeapon = (playerData.equipWeapon == playerData.hasWeapon[weaponNum]) ? playerData.hasWeapon[0] : playerData.hasWeapon[weaponNum];
    //        if (playerData.equipWeapon != playerData.hasWeapon[0])
    //        {
    //            playerData.equipWeaponTrail = playerData.equipWeapon.GetComponentInChildren<TrailRenderer>();
    //            StopCoroutine(swapAnimCoroutine);
    //            StartCoroutine(swapAnimCoroutine);
    //        }
    //    }
    //    if (Input.GetKeyDown(KeyCode.Alpha4))
    //    {
    //        anim.Play("Swap");
    //        weaponNum = 4;
    //        playerData.equipWeapon.SetActive(false);
    //        playerData.equipWeapon = (playerData.equipWeapon == playerData.hasWeapon[weaponNum]) ? playerData.hasWeapon[0] : playerData.hasWeapon[weaponNum];
    //        if (playerData.equipWeapon != playerData.hasWeapon[0])
    //        {
    //            playerData.equipWeaponTrail = playerData.equipWeapon.GetComponentInChildren<TrailRenderer>();
    //            StopCoroutine(swapAnimCoroutine);
    //            StartCoroutine(swapAnimCoroutine);
    //        }
    //    }
    //}
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
                //equipWeaponEffect = equipWeapon.transform.GetChild(0).gameObject;
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
        //sDown1 = Input.GetButtonDown("Swap1");
        //sDown2 = Input.GetButtonDown("Swap2");
        //sDown3 = Input.GetButtonDown("Swap3");
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
            isJump = true;
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

    public float trailOnTime = 0.2f;
    public float trailOffTime = 0.5f;

    private IEnumerator TrailOn()
    {
        yield return new WaitForSeconds(trailOnTime);
        playerData.equipWeaponTrail.emitting = true;
        yield return new WaitForSeconds(trailOffTime);
        playerData.equipWeaponTrail.emitting = false;
    }

    private IEnumerator TrailOn2()
    {
        yield return new WaitForSeconds(trailOnTime + 0.4f);
        playerData.equipWeaponTrail.emitting = true;
        yield return new WaitForSeconds(trailOffTime);
        playerData.equipWeaponTrail.emitting = false;
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
            isJump = false;
            jumpCount = 0;
        }
    }

    //private void StopToWall()
    //{
    //    Debug.DrawRay(transform.position, transform.forward * 1, Color.green);
    //    isBorder = Physics.Raycast(transform.position, transform.forward, 1, LayerMask.GetMask("Wall"));
    //}
}