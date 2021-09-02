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

    //bool isBorder;
    private void Start()
    {
        playerData = GameObject.Find("GameManager").GetComponent<PlayerData>();
        anim = GetComponentInChildren<Animator>();
        rigid = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    private void Update()
    {
        GetInput();
        Move();
        Attack();
        Turn();
        Jump();
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

    private void Attack()
    {
        if (playerData.equipWeapon == null)
            return;

        if (fireDelay < 10)
            fireDelay += Time.deltaTime;

        isFireReady = playerData.equipWeaponRate < fireDelay;
        if (playerData.equipWeaponRate < fireDelay)
            isFireReady2 = false;

        if (fDown)
        {
            if (isFireReady2 && fireDelay > 0.5f)
            {
                anim.SetBool("isSwing2", true);
                isFireReady2 = false;
                fireDelay = -playerData.equipWeaponRate;
            }
            else if (isFireReady)
            {
                anim.SetTrigger("doSwing1");
                anim.SetBool("isSwing2", false);
                fireDelay = 0;
                isFireReady2 = true;
            }
        }
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