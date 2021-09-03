using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public PlayerData playerData;
    private Animator anim;
    private Rigidbody rigid;
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
    public TrailRenderer trailRenderer;

    //bool isBorder;
    private void Start()
    {
        playerData = GameObject.Find("GameManager").GetComponent<PlayerData>();
        anim = GetComponentInChildren<Animator>();
        rigid = GetComponent<Rigidbody>();

        //trailRenderer.emitting = true;
    }

    // Update is called once per frame
    private void Update()
    {
        GetInput();
        Move();
        Attack();
        Turn();
        Jump();
        Swap();
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

    private bool isJump;

    public void GetHit(int hitDamage)
    {
        playerData.curentHp -= hitDamage;
        Debug.Log("데미지 입음" + hitDamage);
        anim.Play("GetHit");
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

    private void Trail()
    {
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
            if (playerData.equipWeapon = playerData.hasWeapon[0]) return;

            if (isFireReady2 && fireDelay > 0.5f) //공격 2
            {
                anim.SetBool("isSwing2", true);
                isFireReady2 = false;
                fireDelay = -playerData.equipWeaponRate;
            }
            else if (isFireReady) //공격 1
            {
                anim.SetTrigger("doSwing1");
                anim.SetBool("isSwing2", false);
                fireDelay = 0;
                isFireReady2 = true;
            }
        }
    }

    private float trailOnTime = 0.1f;

    private void TrailOn()
    {
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