using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Boss : MonoBehaviour
{
    public enum MonsterAttackType { ShortDistace, LongDistance }

    public MonsterAttackType monsterAttackType;

    public int monsterHp = 2000;
    public int monsterMaxHp = 2000;
    public float monsterMp = 0;
    public float monsterMaxMp = 40;

    public int monsterDamage = 110;
    public int skillFireDamage = 150;

    public int skill2AttackDamage = 500;
    public int Skill3Damage = 40;

    public float knockBackForce = 2;
    public int monsterExp = 2;
    public float monsterSpeed = 1;

    public GameObject bossSkill2Pos;
    public bool bossOn;
    public bool isSkill;
    public bool isHit;
    public bool isDie;
    public bool isAttack;
    public bool bossSleepOff;

    /// <summary>
    /// ////////////////////////////////////////
    /// </summary>
    public float hissingRange = 10;

    public float attackShortArea = 6f;
    public float attackSkillArea = 13;
    public float attackShortRange = 3f; // 기본값 8로
    //

    private NavMeshAgent nav;
    public GameObject target;
    public Rigidbody rigid;
    public new Collider collider;
    public Animator anim;
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

    public float fireDelay;
    public float skill1Delay;
    public float fireRate = 3f;
    public float skillRate = 9f;
    public float dis;
    public GameObject hpBar;
    private IEnumerator attackCo;
    public Transform hitDamageNumPos;
    public Transform skillBulletPos;
    public Image hpBarImage;
    public HitDamageScript hitDamageScript;

    public GameObject skillHandStone;
    public GameObject skillBulletStone;

    private void Awake()
    {
        bossSkill2Pos = GameObject.Find("Event/BossSkill2Pos");

        hitDamageNumPos = transform.GetChild(1).GetComponent<Transform>();
        skillBulletPos = transform.GetChild(2).GetComponent<Transform>();
        nav = GetComponent<NavMeshAgent>();
        target = GameObject.Find("Player");
        anim = GetComponentInChildren<Animator>();
        bullet = transform.GetChild(0).GetComponent<BoxCollider>();
        bullet.enabled = false;

        renderers = GetComponentsInChildren<Renderer>();

        playerData = GameObject.Find("GameManager").GetComponent<PlayerData>();

        rigid = GetComponent<Rigidbody>();

        //hitDamageScript = hpBar.GetComponent<HitDamageScript>();
        //hitDamageScript.target = gameObject;
        //hpBarImage = hitDamageScript.hitBarImage;
        //hpBar.SetActive(false);

        anim.Play("Sleep");
    }

    private void NavMove()
    {
        if (nav.enabled == false)
            return;

        nav.isStopped = !isChase;//펄스면 스탑
        nav.SetDestination(target.transform.position);

        if (!isHit && !isSkill && !isAttack)
        {
            //if (dis > attackShortArea)
            //{
            //    isBattle = false;
            //}
            if (dis < attackShortRange)//공격 범위 안 : 정지
            {
                isChase = false;
                anim.SetBool("isWalk", false);

                if (fireRate < fireDelay && !isHit)//공격속도<공격딜레이
                {
                    //StartCoroutine(attackCo);
                    attackCo = Attack();
                    StartCoroutine(attackCo);
                    //Debug.Log("z");
                }
            }
            else if (fireRate <= fireDelay && dis >= attackShortRange)//공격 범위 밖 : 움직임
            {
                anim.SetBool("isWalk", true);
                isChase = true;
            }
        }
    }

    public GameObject bossBar;

    private IEnumerator OnBoss()
    {
        yield return new WaitForSeconds(3f);
        bossBar.SetActive(true);
        bossOn = true;
        bossSleepOff = false;
    }

    public GameObject skill1FireObj;
    public Transform skill1Pos;
    public float skill1Time1;
    public float skill1Time2;

    private IEnumerator Skill1()
    {
        yield return new WaitForSeconds(skill1Time1);
        Instantiate(skill1FireObj, skill1Pos.position, Quaternion.identity);
        yield return new WaitForSeconds(skill1Time2);
        Instantiate(skill1FireObj, skill1Pos.position, Quaternion.identity);
        yield return new WaitForSeconds(skill1Time2);
        Instantiate(skill1FireObj, skill1Pos.position, Quaternion.identity);
        yield return new WaitForSeconds(skill1Time2);
        Instantiate(skill1FireObj, skill1Pos.position, Quaternion.identity);
        yield return new WaitForSeconds(skill1Time2);
        //Instantiate(skill1FireObj, skill1Pos.position, Quaternion.LookRotation(transform.forward));
        Instantiate(skill1FireObj, skill1Pos.position, Quaternion.identity);
        isSkill = false;
    }

    public float posY;

    //public float posYTime;
    private float speed;

    private Vector3 targetMVec;
    public bool flyAttackBullet;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Ground"))
        {
            if (flyAttackBullet)
            {
                anim.SetBool("FlyOff", true);

                Debug.Log("땅바닥");

                transform.rotation = Quaternion.Euler(0, 0, 0);
                flyAttackEnd = false;
                flyAttackBullet = false;
                StartCoroutine(Skill2FlyDown());
            }
        }
    }

    public GameObject skill2Explosion2;
    public GameObject skill2Bullet2;

    private IEnumerator Skill2FlyDown()
    {
        nav.enabled = true;
        yield return null;
        nav.enabled = false;
        Instantiate(skill2Bullet2, transform.position, Quaternion.identity);
        Instantiate(skill2Explosion2, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(0.04f);
        Instantiate(skill2Explosion2, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(0.05f);
        Instantiate(skill2Explosion2, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(0.06f);
        Instantiate(skill2Explosion2, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(4f);
        nav.enabled = true;

        anim.SetBool("FlyOff", false);
        isSkill = false;
    }

    private void Update()

    {
        //if (Input.GetKeyDown(KeyCode.F7))
        //{
        //    anim.Play("Skill1");
        //    StartCoroutine(Skill1());
        //}

        if (flyAttackEnd /*&& isSkill*/)
        {
            if (posY < 2)
            {
                posY += 0.05f;
                transform.position = new Vector3(transform.position.x, transform.position.y + posY, transform.position.z);
                Debug.Log("업");
            }
            else
            {
                flyAttackBullet = true;
                //if (posYTime < 1)
                //{
                //posYTime += 0.05f;
                Debug.Log("돌진");

                //transform.position = Vector3.LerpUnclamped(transform.position, targetMVec, Time.deltaTime);
                transform.Translate(transform.forward * Time.deltaTime * 20, Space.World);
                Vector3 directionVec = targetMVec - transform.position;
                Quaternion qua = Quaternion.LookRotation(directionVec);
                transform.rotation = Quaternion.Slerp(transform.rotation, qua, Time.deltaTime * 2f);
                //}
                //else
                //{
                //    transform.Translate(transform.forward * speed, Space.World);
                //}
            }
            return;
        }

        //if (Input.GetKeyDown(KeyCode.F7))
        //{
        //    Debug.Log(testTime);
        //}
        //if (testTimeB)
        //{
        //    testTime += Time.deltaTime;
        //}
        //if (Input.GetKeyDown(KeyCode.F6))
        //{
        //    StartCoroutine(Skill2());
        //}
        if (bossSleepOff)
        {
            StartCoroutine(OnBoss());
        }
        if (!bossOn)
            return;
        if (isDie)
            return;

        NavMove();
        Distance();
        //Anim();
        FireDelay();//임시
        Dying();
        Skill1On();
        Skill2On();
    }

    public float on2CollTime;
    public float on2CollTime2;
    public GameObject skill2EffectObj;
    public GameObject skill2ExplosionObj;
    public GameObject skill2Bullet;

    //private Vector3 skill2Vec;
    public float testTime;

    public bool testTimeB;
    public bool flyAttackEnd;

    private IEnumerator Skill2()
    {
        posY = 0;
        isSkill = true;
        isAttack = false;
        isHit = false;
        isChase = false;
        anim.Play("Skill2On");
        testTimeB = true;

        //skill2Vec = bossSkill2Pos.transform.position;
        //skill2Vec.x += Random.Range(0, 17);
        //skill2Vec.z += Random.Range(0, 17);
        //Instantiate(testObj, skill2Vec, Quaternion.identity);
        yield return new WaitForSeconds(on2CollTime);

        StartCoroutine(Test());
        yield return new WaitForSeconds(on2CollTime2);
        StartCoroutine(Test());
        yield return new WaitForSeconds(on2CollTime2);
        StartCoroutine(Test());
        yield return new WaitForSeconds(on2CollTime2);
        StartCoroutine(Test());
        yield return new WaitForSeconds(on2CollTime2);
        StartCoroutine(Test());
        yield return new WaitForSeconds(on2CollTime2);
        StartCoroutine(Test());
        yield return new WaitForSeconds(on2CollTime2);
        StartCoroutine(Test());
        yield return new WaitForSeconds(on2CollTime2);
        StartCoroutine(Test());
        yield return new WaitForSeconds(on2CollTime2);
        StartCoroutine(Test());
        yield return new WaitForSeconds(on2CollTime2);
        StartCoroutine(Test());
        yield return new WaitForSeconds(on2CollTime2);
        StartCoroutine(Test());
        yield return new WaitForSeconds(0.2f);
        anim.Play("FlyAttack");
        targetMVec = target.transform.position;
        nav.enabled = false;
        flyAttackEnd = true;

        //isSkill = false;
    }

    private IEnumerator Test()
    {
        Vector3 skill2Vec = bossSkill2Pos.transform.position;
        skill2Vec.x += Random.Range(-17, 17);
        skill2Vec.z += Random.Range(-17, 17);
        Instantiate(skill2EffectObj, skill2Vec, Quaternion.identity);
        yield return new WaitForSeconds(2.5f);
        Instantiate(skill2ExplosionObj, skill2Vec, Quaternion.identity);
        Instantiate(skill2Bullet, skill2Vec, Quaternion.identity);
    }

    private void Skill1On()
    {
        if (skill1Delay > 30)
        {
            skill1Delay = 0;
            isSkill = true;
            isAttack = false;
            isHit = false;
            isChase = false;
            anim.Play("Skill1");
            StartCoroutine(Skill1());
        }
    }

    private void Skill2On()
    {
        if (monsterMp > monsterMaxMp)
        {
            monsterMp = 0;
            //isSkill = true;
            //isAttack = false;
            //isHit = false;
            //isChase = false;
            //anim.Play("Skill1");
            StartCoroutine(Skill2());
        }
    }

    private void FireDelay()
    {
        if (isSkill) return;

        if (fireDelay < 10)
            fireDelay += Time.deltaTime;

        if (skill1Delay < 30)
            skill1Delay += Time.deltaTime;

        if (monsterMp >= monsterMaxMp)
            monsterMp = monsterMaxMp;
        else
            monsterMp += Time.deltaTime;
    }

    public float onStoneTime;
    public float offSkillTime;

    private IEnumerator Skill()
    {
        //attackCount++;
        isChase = false;
        isSkill = true;
        skill1Delay = 0;
        anim.Play("Skill");
        transform.LookAt(target.transform);

        yield return new WaitForSeconds(onStoneTime);
        //콜라이더 온
        skillHandStone.SetActive(true);

        yield return new WaitForSeconds(offSkillTime);

        Instantiate(skillBulletStone, skillBulletPos.position, skillBulletPos.transform.rotation/*, transform*/);

        skillHandStone.SetActive(false);

        isSkill = false;
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

    public GameObject hitDamageNumObj;
    public Text hitDamageNumText;

    public void GetHit(int hitDamage, Transform hitPos)
    {
        if (isDie || isSkill)
            return;
        //attackCo = Attack();
        if (attackCo != null)
            StopCoroutine(attackCo);
        isAttack = false;
        //hitDamageScript.hitBarTime = 0;
        //hpBarImage.fillAmount = ((float)monsterHp - hitDamage) / (float)monsterMaxHp;
        //hpBar.SetActive(true);
        hitDamageNumText = hitDamageNumObj.GetComponentInChildren<Text>();
        hitDamageNumText.text = $"{hitDamage}";
        Instantiate(hitDamageNumObj, hitDamageNumPos.position, hitDamageNumObj.transform.rotation);
        ShakeHit();
        //Shake();
        isHit = true;
        isChase = false;

        Invoke("HitEnd", hitEndTime);
        monsterHp -= hitDamage;

        //Debug.Log("데미지 입음" + hitDamage);

        anim.Play("Hit");

        //Instantiate(hitEffect, hitPos.position, transform.rotation);
    }

    private void HitEnd()
    {
        isHit = false;
        //isChase = true;
        //anim.SetBool("isWalk", true);
    }

    private void Distance()
    {
        dis = Vector3.Distance(target.transform.position, transform.position);
    }

    public GameObject ExclamationMark;

    public float onCollTime = 1;

    public float onCollTime2 = 1;
    public int attackCount;

    private IEnumerator Attack()
    {
        isAttack = true;
        attackCount++;
        isChase = false;
        fireDelay = 0;
        int i = Random.Range(1, 3);
        anim.Play($"Attack" + i);

        transform.LookAt(target.transform);
        yield return new WaitForSeconds(onCollTime);
        bullet.enabled = true;
        yield return new WaitForSeconds(0.1f);
        bullet.enabled = false;
        if (i == 1)
        {
            yield return new WaitForSeconds(onCollTime2);
            bullet.enabled = true;
            yield return new WaitForSeconds(0.1f);
            bullet.enabled = false;
        }
        isAttack = false;
    }

    public Renderer[] renderers;

    private void Dying()
    {
        if (monsterHp <= 0)
        {
            isDie = true;
            hitDamageScript.hitBarTime = 0;
            hpBarImage.fillAmount = (float)monsterHp / (float)monsterMaxHp;
            Destroy(bossBar);
            nav.enabled = false;
            anim.Play("Die");
            //StopCoroutine(attackCo);
            Invoke("Die", 1.5f);
        }
    }

    public List<GameObject> dieDropItemList;
    public GameObject coin;

    private void Die()
    {
        //float randomDropCount = Random.Range(0.05f, 0.15f);
        //InvokeRepeating("DieDropItem", 0f, 0.05f);
        //Invoke("DieDropItemStop", randomDropCount);
        //float randomDropCount2 = Random.Range(0.05f, 0.2f);
        //InvokeRepeating("DieDropCoin", 0f, 0.05f);
        //Invoke("DieDropCoinStop", randomDropCount2);

        //Debug.Log(randomDropCount);
        for (int i = 0; i < renderers.Length; i++)
            renderers[i].material.color = Color.black;
        rigid.constraints = RigidbodyConstraints.None;
        gameObject.layer = 31;
        playerData.curentExp += monsterExp;
        Destroy(gameObject, 1f);
    }

    private void DieDropItemStop()
    {
        CancelInvoke("DieDropItem");
    }

    private void DieDropCoinStop()
    {
        CancelInvoke("DieDropCoin");
    }
}