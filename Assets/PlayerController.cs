using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class PlayerController : MonoBehaviour
{
    public PlayerData playerData;
    private Animator anim;
    private Rigidbody rigid;
    public Transform skill1Pos;
    public float autoSwapTime;
    public GameObject skill2Wave;
    public bool talkToNpc;

    //public GameObject skill1SwordEffect;
    private Vector3 moveVec;

    private float fireDelay;

    private int fireCount;
    private int jumpCount;
    private float hAxis;
    private float vAxis;

    private float moveX;
    private float moveZ;

    public float trailOnTime = 0.5f;
    public float trailOffTime = 0.2f;

    //public float i = 0.4f;
    private bool fDown;

    private bool wDown;
    private bool jDown;
    public bool isFireReady;
    private bool isFireReady2;
    private bool isJump;
    private bool isSkill;
    private float isSkillTime;
    private bool inventoryUIB;

    public bool characterStatsUIB;
    public bool skillUIB;

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

    public Renderer skill1SwordRenderer;

    //bool isBorder;
    //public float timed;

    public List<GameObject> testListObj;
    public GameObject inventoryUI;
    public GameObject characterStats;
    public GameObject skillUI;
    public InventoryUI inventoryUIC;
    private UiScript uiScript;
    public GameObject skill3Range;
    public Vector3 skill3Pos;
    public bool isStun;

    private void Awake()
    {
        playerData = GameObject.Find("GameManager").GetComponent<PlayerData>();
        anim = GetComponentInChildren<Animator>();
        rigid = GetComponent<Rigidbody>();
        skill1Pos = transform.GetChild(0).GetComponent<Transform>();
        skill3Range = GameObject.Find("Skill3Range");
        inventoryUI = GameObject.Find("UI/InventoryUI");
        characterStats = GameObject.Find("UI/CharacterStats");
        inventoryUIC = GameObject.Find("UI").GetComponent<InventoryUI>();
        skillUI = GameObject.Find("UI/CharacterSkill");
        uiScript = GameObject.Find("UI").GetComponent<UiScript>();
        skill2Wave = GameObject.Find("Skill2Wave");
        //trailRenderer.emitting = true;
        //임시
    }

    private void Update()
    {
        //timed += Time.deltaTime;
        if (isSkill || talkToNpc || isStun)
        {
            anim.SetBool("isRun", false);
            Debug.Log("토크중");
            return;
        }
        GetInput();
        if (inventoryUIB || characterStatsUIB || skillUIB)
            return;
        Move();
        Attack();
        Turn();
        Jump();
        Swap();
        Interation();
        Skill1();
        Skill2();
        Skill3();
        AutoSwap();
        Portion();
        if (Input.GetKeyDown(KeyCode.F3))
        {
            StartCoroutine(Stun(1));
        }
        if (Input.GetKeyDown(KeyCode.F4))
        {
            StartCoroutine(Stun(4));
        }
    }

    public IEnumerator Stun(float time)
    {
        anim.SetBool("isStun", true);
        isStun = true;
        anim.Play("Stun");
        yield return new WaitForSeconds(time);
        anim.SetBool("isStun", false);
        isStun = false;
    }

    private void Skill3()
    {
        if (Input.GetKeyDown(KeyCode.F) && !isSkill && playerData.skill1On)
        {
            if (!(playerData.skill3B))
            {
                if (playerData.curentMp >= 5)
                {
                    playerData.skill3B = true;
                    playerData.curentMp -= 5;
                    isSkillTime = 0.2f;
                    StartCoroutine(IsSkillOnOffCo());
                    anim.Play("Skill03");
                    skill3Range.SetActive(true);
                }
                else if (playerData.curentMp < 5)
                {
                    //explanationText.text = "마나가 부족합니다.";
                    uiScript.explanationTextUI.text = "마나가 부족합니다.";
                    uiScript.ExplanationUI();
                }
            }
            else
            {
                uiScript.explanationTextUI.text = "스킬이 쿨타임 입니다.";
                uiScript.ExplanationUI();
            }
        }
    }

    private void Skill2()
    {
        if (Input.GetKeyDown(KeyCode.D) && !isSkill && playerData.skill2On)
        {
            if (!(playerData.skill2B))
            {
                if (playerData.curentMp >= 20)
                {
                    playerData.skill2B = true;
                    playerData.curentMp -= 20;
                    isSkillTime = 1.8f;
                    StartCoroutine(IsSkillOnOffCo());
                    anim.Play("Skill02");
                    skill2Wave.SetActive(true);
                }
                else if (playerData.curentMp < 20)
                {
                    //explanationText.text = "마나가 부족합니다.";
                    uiScript.explanationTextUI.text = "마나가 부족합니다.";
                    uiScript.ExplanationUI();
                }
            }
            else
            {
                uiScript.explanationTextUI.text = "스킬이 쿨타임 입니다.";
                uiScript.ExplanationUI();
            }
        }
    }

    public Text explanationText;

    private void Skill1()
    {
        if (playerData.equipWeapon != playerData.hasWeapon[0] && Input.GetKeyDown(KeyCode.R) && !isSkill && playerData.skill3On)
        {
            if (!(playerData.skill1B))
            {
                if (playerData.curentMp >= 50)
                {
                    playerData.curentMp -= 50;
                    playerData.skill1B = true;
                    StartCoroutine(Skill1Co());
                }
                else if (playerData.curentMp < 50)
                {
                    //explanationText.text = "마나가 부족합니다.";
                    uiScript.explanationTextUI.text = "마나가 부족합니다.";
                    uiScript.ExplanationUI();
                }
            }
            else
            {
                uiScript.explanationTextUI.text = "스킬이 쿨타임 입니다.";
                uiScript.ExplanationUI();
            }
        }
    }

    private void AutoSwap()
    {
        if (autoSwapTime < 15 && playerData.equipWeapon != playerData.hasWeapon[0])
            autoSwapTime += Time.deltaTime;

        if (autoSwapTime > 15 && playerData.equipWeapon != playerData.hasWeapon[0])
        {
            IEnumerator swapAnimCoroutine;
            swapAnimCoroutine = SwapTrail();
            playerData.equipWeapon.SetActive(false);

            playerData.equipWeapon = playerData.hasWeapon[0];

            anim.Play("SwapRe");//현재 착용 무기가 널무기일 때
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
        float camPosX = Random.value * shakeRange * 3 - shakeRange;
        float camPosY = Random.value * shakeRange * 3 - shakeRange;
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

    private IEnumerator IsSkillOnOffCo()
    {
        isSkill = true;
        yield return new WaitForSeconds(isSkillTime);
        isSkill = false;
    }

    private IEnumerator Skill1Co()
    {
        isSkill = true;
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
        isSkill = false;
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

        //if (Input.GetKeyDown(KeyCode.Alpha2)) weaponNum = 2;

        //if (Input.GetKeyDown(KeyCode.Alpha3)) weaponNum = 3;

        //if (Input.GetKeyDown(KeyCode.Alpha4)) weaponNum = 4;

        if (Input.GetKeyDown(KeyCode.Alpha1)/* || Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Alpha3) || Input.GetKeyDown(KeyCode.Alpha4)*/)
        {
            if (playerData.hasWeapon[weaponNum] == null)
            {
                uiScript.explanationTextUI.text = "무기를 장착하세요.";
                uiScript.ExplanationUI();
                return;
            }
            IEnumerator swapAnimCoroutine;
            swapAnimCoroutine = SwapTrail();
            playerData.equipWeapon.SetActive(false);

            playerData.equipWeapon = (playerData.equipWeapon == playerData.hasWeapon[weaponNum]) ? playerData.hasWeapon[0] : playerData.hasWeapon[weaponNum];
            if (playerData.equipWeapon != playerData.hasWeapon[0]) // 현재 착용 무기가 널무기가 아닐 때
            {
                autoSwapTime = 0;
                anim.Play("Swap");
                playerData.equipWeaponTrail = playerData.equipWeapon.GetComponentInChildren<TrailRenderer>();
                playerData.equipWeaponEffect = playerData.equipWeapon.transform.GetChild(0).gameObject;
                skill1SwordRenderer = playerData.equipWeapon.GetComponent<Renderer>();

                playerData.equipWeaponEffect.SetActive(false);

                StopCoroutine(swapAnimCoroutine);
                StartCoroutine(swapAnimCoroutine);
            }
            else
            {
                playerData.equipWeaponTrail = null;
                playerData.equipWeaponEffect = null;
                skill1SwordRenderer = null;
                anim.Play("SwapRe");//현재 착용 무기가 널무기일 때
            }
        }
    }

    private IEnumerator SwapTrail()
    {
        playerData.equipWeaponTrail.emitting = true;
        yield return new WaitForSeconds(0.3f);
        playerData.equipWeaponTrail.emitting = false;
    }

    private void Move()
    {
        //moveVec = new Vector3(camPlayerMove.hAxis2, 0, camPlayerMove.vAxis2).normalized;
        moveVec = new Vector3(hAxis, 0, vAxis).normalized;
        //moveVec = new Vector3(moveX, 0, moveZ).normalized;
        transform.position += moveVec * (wDown ? 0.2f : 1f) * playerData.playerSpeed * Time.deltaTime;
        anim.SetBool("isRun", moveVec != Vector3.zero);
    }

    //public CamPlayerMove camPlayerMove;

    private void GetInput()
    {
        hAxis = Input.GetAxisRaw("Horizontal");
        vAxis = Input.GetAxisRaw("Vertical");
        //moveVec = Vector3.zero;
        //moveZ = 0;
        //moveX = 0;
        //if (Input.GetKey(KeyCode.UpArrow))
        //    moveZ = 1;
        //if (Input.GetKey(KeyCode.DownArrow))
        //    transform.rotation = Quaternion.Euler(0, 180, 0);
        //moveZ = -1;
        //if (Input.GetKey(KeyCode.LeftArrow))
        //    moveX = -1;
        //if (Input.GetKey(KeyCode.RightArrow))
        //    moveX = 1;

        jDown = Input.GetButtonDown("Jump");
        fDown = Input.GetButtonDown("Fire1");
        if (Input.GetKeyDown(KeyCode.I))
        {
            inventoryUIB = !inventoryUIB;
            inventoryUI.SetActive(inventoryUIB);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            characterStatsUIB = !characterStatsUIB;
            characterStats.SetActive(characterStatsUIB);
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            skillUIB = !skillUIB;
            skillUI.SetActive(skillUIB);
        }
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
        if (!isSkill)
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

    private void Attack()
    {
        if (fireDelay < 10)
            fireDelay += Time.deltaTime;

        isFireReady = playerData.equipWeaponRate < fireDelay;
        if (playerData.equipWeaponRate < fireDelay)
            isFireReady2 = false;

        if (fDown)
        {
            if (playerData.hasWeapon[1] == null)
            {
                uiScript.explanationTextUI.text = "무기를 장착하세요.";
                uiScript.ExplanationUI();
            }

            if (playerData.equipWeapon == playerData.hasWeapon[0])
            {
                if (playerData.hasWeapon[1] != null)
                {
                    autoSwapTime = 0;

                    IEnumerator swapAnimCoroutine;
                    swapAnimCoroutine = SwapTrail();
                    playerData.equipWeapon.SetActive(false);
                    playerData.equipWeapon = playerData.hasWeapon[1];
                    anim.Play("Swap");
                    playerData.equipWeaponTrail = playerData.equipWeapon.GetComponentInChildren<TrailRenderer>();
                    playerData.equipWeaponEffect = playerData.equipWeapon.transform.GetChild(0).gameObject;
                    skill1SwordRenderer = playerData.equipWeapon.GetComponent<Renderer>();

                    playerData.equipWeaponEffect.SetActive(false);

                    StopCoroutine(swapAnimCoroutine);
                    StartCoroutine(swapAnimCoroutine);
                }
            }
            else if (playerData.equipWeapon == playerData.hasWeapon[1])
            {
                IEnumerator trailon;
                trailon = TrailOn();
                if (isFireReady2 && fireDelay > 0.5f) //공격 2
                {
                    autoSwapTime = 0;
                    //StopCoroutine(trailon);
                    StartCoroutine(TrailOn2());
                    anim.SetBool("isSwing2", true);
                    isFireReady2 = false;
                    fireDelay = -playerData.equipWeaponRate;
                }
                else if (isFireReady) //공격 1
                {
                    autoSwapTime = 0;
                    //StopCoroutine(trailon);
                    StartCoroutine(trailon);
                    anim.SetTrigger("doSwing1");
                    anim.SetBool("isSwing2", false);
                    fireDelay = 0;
                    isFireReady2 = true;
                }
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

    private Item item;
    public GameObject nearObject;
    public Npc npc;

    private void Interation()
    {
        if (Input.GetKeyDown(KeyCode.G) && nearObject != null)
        {
            if (nearObject.CompareTag("Item"))
            {
                item = nearObject.GetComponent<Item>();
                playerData.GetItem(item.id, item.type, item.sprite, item.count);
                Destroy(nearObject);
            }
            else if (nearObject.CompareTag("Npc"))
            {
                //item = nearObject.GetComponent<Item>();
                //playerData.GetItem(item.id, item.type, item.sprite, item.count);
                //Destroy(nearObject);

                npc = nearObject.GetComponent<Npc>();
                npc.onUI(gameObject);
            }
        }
    }

    private void Portion()
    {
        if (Input.GetKeyDown(KeyCode.F1) && playerData.hpCount >= 1)
        {
            playerData.itemCCount--;
            inventoryUIC.itemOldCCount--;
            playerData.hpCount--;
            uiScript.portionText.text = $"{playerData.hpCount}";

            inventoryUIC.PortionC();

            playerData.curentHp += (int)(playerData.maxHp * 0.3f);

            if (playerData.curentHp > playerData.maxHp)
                playerData.curentHp = playerData.maxHp;
            Debug.Log("포션 먹음");
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Item"))
        {
            nearObject = other.gameObject;
        }
        else if (other.CompareTag("Npc"))
        {
            nearObject = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Item"))
            nearObject = null;
        else if (other.CompareTag("Npc"))
        {
            nearObject = null;
        }
    }

    //private void StopToWall()
    //{
    //    Debug.DrawRay(transform.position, transform.forward * 1, Color.green);
    //    isBorder = Physics.Raycast(transform.position, transform.forward, 1, LayerMask.GetMask("Wall"));
    //}
}