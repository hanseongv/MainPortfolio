using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class MonsterNormal : MonoBehaviour
{
    public enum MonsterType { Slime }

    public MonsterType monsterType;

    public enum MonsterAttackType { ShortDistace, LongDistance }

    public MonsterAttackType monsterAttackType;

    private NavMeshAgent nav;
    public GameObject target;
    public Rigidbody rigid;
    public new Collider collider;
    private Animator anim;

    public float attackShortRange = 1.7f; // 기본값 8로
    public float attackShortArea = 6f;
    public float hissingRange = 10;

    //public bool attackRange;
    //public bool attackRangeDis;
    public bool isChase;

    public bool isBattle;
    public bool isHissing;
    public bool isFireReady;
    public float fireDelay;
    public float fireRate = 1.5f;
    public float dis;

    public BoxCollider bullet;
    public Vector3 homeVec;

    private void Awake()
    {
        nav = GetComponent<NavMeshAgent>();
        target = GameObject.Find("Player");
        anim = GetComponentInChildren<Animator>();
        bullet = transform.GetChild(0).GetComponent<BoxCollider>();
        bullet.enabled = false;
        homeVec = transform.position;
    }

    private void Update()
    {
        NavMove();
        Distance();
        Anim();
        FireDelay();//임시
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
                Debug.Log("시야 밖에 있음.");
            }
            else if (dis < hissingRange && !isHissing) // 시야 안에 들어와서 하악질함.
            {
                anim.SetBool("isIdle2", false);
                anim.SetBool("isIdle3", false);
                anim.SetTrigger("doHissing");
                anim.SetBool("isHissing", true);
                isHissing = true;
                Debug.Log("시야 안에 들어와서 하악질함.");
            }
            else if (dis > attackShortArea && dis < hissingRange)//dis > 7 전투시작전 하악질 상태
            {
                transform.LookAt(target.transform);
                Debug.Log("전투시작전 하악질 상태");
                anim.SetBool("isBattle", false);
            }
            else if (dis < attackShortArea)// 공격 범위 안에 들어옴 배틀 시작
            {
                isChase = true;
                isBattle = true;
                Debug.Log("배틀");
                anim.SetBool("isBattle", true);
            }
        }
    }

    private void Distance()
    {
        dis = Vector3.Distance(target.transform.position, transform.position);
    }

    private void NavMove()
    {
        nav.isStopped = !isChase;//펄스면 스탑
        nav.SetDestination(target.transform.position); //좀비 위치 이동 후 랜덤 시간 넣기 yield return new WaitForSeconds(Random.Range(0.5f, 2f));

        if (isBattle)
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
                        StartCoroutine("Attack");
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

    private IEnumerator Attack()
    {
        isChase = false;
        fireDelay = 0;
        anim.Play(Random.Range(0, 2) == 0 ? "Attack1" : "Attack2");
        transform.LookAt(target.transform);
        yield return new WaitForSeconds(1);
        //콜라이더 온
        yield return null;
        //콜라이더 오프
    }
}