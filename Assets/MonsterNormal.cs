﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class MonsterNormal : MonoBehaviour
{
    //몬스터 스테이트
    public enum MonsterType { Slime }

    public MonsterType monsterType;

    public enum MonsterAttackType { ShortDistace, LongDistance }

    public MonsterAttackType monsterAttackType;

    public int monsterHp = 10;
    public int monsterMaxHp = 10;
    public int monsterDamage = 1;
    public float knockBackForce = 2;
    public int monsterExp = 2;
    public float monsterSpeed = 1;
    public float hissingRange = 10;
    public float attackShortArea = 6f;
    public float attackShortRange = 1.7f; // 기본값 8로
    //

    private NavMeshAgent nav;
    public GameObject target;
    public Rigidbody rigid;
    public new Collider collider;
    private Animator anim;
    public BoxCollider bullet;
    public Vector3 homeVec;
    public GameObject ExclamationMarkImage;
    public GameObject hitEffect;
    private PlayerData playerData;
    public float hitEndTime = 0.4f;
    public bool isChase;

    public bool isBattle;
    public bool isHissing;
    public bool isFireReady;
    public bool isHit;
    public bool isDie;
    public float fireDelay;
    public float fireRate = 1.5f;
    public float dis;

    private IEnumerator attackCo;

    private void Awake()
    {
        nav = GetComponent<NavMeshAgent>();
        target = GameObject.Find("Player");
        anim = GetComponentInChildren<Animator>();
        bullet = transform.GetChild(0).GetComponent<BoxCollider>();
        bullet.enabled = false;
        homeVec = transform.position;
        renderers = GetComponentsInChildren<Renderer>();
        //ExclamationMarkImage = GameObject.Find("Canvas/ExclamationMarkImage");
        playerData = GameObject.Find("GameManager").GetComponent<PlayerData>();
        attackCo = Attack();
        rigid = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (isDie)
            return;
        NavMove();
        Distance();
        Anim();
        FireDelay();//임시
        Dying();

        //ExclamationMarkImage.transform.position = Camera.main.WorldToScreenPoint(transform.position + f);
    }

    private void FireDelay()
    {
        if (fireDelay < 10)
            fireDelay += Time.deltaTime;
    }

    private void Anim()
    {
        if (!isBattle)
        {
            isChase = false;
            if (dis > hissingRange) //시야 밖에 있음
            {
                anim.SetBool(Random.Range(0, 2) == 0 ? "isIdle2" : "isIdle3", true);
                anim.SetBool(Random.Range(0, 2) == 0 ? "isIdle2" : "isIdle3", false);
                anim.SetBool("isHissing", false);
                isHissing = false;
            }
            else if (dis <= hissingRange && !isHissing) // 시야 안에 들어와서 하악질함. 한 번 실행
            {
                anim.SetBool("isIdle2", false);
                anim.SetBool("isIdle3", false);
                //anim.SetTrigger("doHissing");

                anim.SetBool("isHissing", true);
                isHissing = true;
                Instantiate(ExclamationMark, transform.position, ExclamationMark.transform.rotation);
            }
            else if (dis > attackShortArea && dis < hissingRange)//dis > 7 전투시작전 하악질 상태
            {
                transform.LookAt(target.transform);

                anim.SetBool("isBattle", false);
            }
            else if (dis <= attackShortArea)// 공격 범위 안에 들어옴 배틀 시작
            {
                anim.SetBool("isHissing", false);
                isHissing = false;
                isChase = true;
                isBattle = true;

                anim.SetBool("isBattle", true);
            }
        }
    }

    private Camera cam;
    private Vector3 camPos;
    private float shakeRange = 0.05f;

    public void ShakeHit()
    {
        cam = playerData.curentCam.GetComponent<Camera>();
        //cam = playerData.curentCam;
        camPos = cam.transform.position;
        InvokeRepeating("StartShakeHit", 0f, 0.01f); //바로 스타트쉐이크를 시작하며 0.005f마다 실행
        Invoke("StopShakeHit", 0.1f);
    }

    private void StartShakeHit()
    {
        float camPosX = Random.value * shakeRange * 0.5f - shakeRange;
        float camPosY = Random.value * shakeRange * 0.5f - shakeRange;
        camPos = cam.transform.position;
        camPos.x += camPosX;
        camPos.y += camPosY;
        cam.transform.position = camPos;
    }

    private void StopShakeHit()
    {
        CancelInvoke("StartShakeHit");
        //cam.transform.position = camPos;
    }

    public void GetHit(int hitDamage, Transform hitPos)
    {
        if (isDie)
            return;
        ShakeHit();
        //Shake();
        isChase = false;
        isHit = true;

        Invoke("HitEnd", hitEndTime);
        monsterHp -= hitDamage;

        //Debug.Log("데미지 입음" + hitDamage);

        anim.Play("GetHit");
        Instantiate(hitEffect, hitPos.position, transform.rotation);
    }

    private void HitEnd()
    {
        isHit = false;
        isChase = true;
    }

    private void Distance()
    {
        dis = Vector3.Distance(target.transform.position, transform.position);
    }

    public GameObject ExclamationMark;

    private void NavMove()
    {
        nav.isStopped = !isChase;//펄스면 스탑
        nav.SetDestination(target.transform.position); //좀비 위치 이동 후 랜덤 시간 넣기 yield return new WaitForSeconds(Random.Range(0.5f, 2f));

        if (isBattle && !isHit)
        {
            if (monsterAttackType == MonsterAttackType.ShortDistace) // 근거리 공격 몬스터일 경우
            {
                if (dis > attackShortArea)
                {
                    isBattle = false;
                }
                if (dis < attackShortRange)//공격 범위 안 : 정지
                {
                    isChase = false;
                    anim.SetBool("isBattleIdle", true);
                    if (fireRate < fireDelay)//공격속도<공격딜레이
                    {
                        //StartCoroutine(attackCo);
                        StartCoroutine(Attack());
                        Debug.Log("z");
                    }
                }
                else//공격 범위 밖 : 움직임
                {
                    anim.SetBool("isBattleIdle", false);
                    isChase = true;
                }
            }
            else
            //원거리
            {
            }
        }
    }

    public float onCollTime = 1;
    public int attackCount;

    private IEnumerator Attack()
    {
        attackCount++;
        isChase = false;
        fireDelay = 0;
        anim.Play(Random.Range(0, 2) == 0 ? "Attack1" : "Attack2");
        transform.LookAt(target.transform);
        //Vector3 dir = target.transform.position - transform.position;
        //transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(dir), Time.deltaTime * 50);
        yield return new WaitForSeconds(onCollTime);
        //콜라이더 온
        bullet.enabled = true;
        //Debug.Log("켜짐");
        //yield return null;
        yield return new WaitForSeconds(0.1f);
        bullet.enabled = false;
        //Debug.Log("꺼짐 공격 횟수" + attackCount);
        //콜라이더 오프
    }

    public Renderer[] renderers;

    private void Dying()
    {
        if (monsterHp <= 0)
        {
            isDie = true;

            nav.enabled = false;
            anim.Play("Die");
            StopCoroutine(attackCo);
            Invoke("Die", 1.5f);
        }
    }

    private void Die()
    {
        for (int i = 0; i < renderers.Length; i++)
            renderers[i].material.color = Color.black;
        rigid.constraints = RigidbodyConstraints.None;
        gameObject.layer = 31;
        playerData.curentExp += monsterExp;
        Destroy(gameObject, 1f);
    }
}