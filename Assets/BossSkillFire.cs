using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSkillFire : MonoBehaviour
{
    public int fireDamage = 150;
    public float speed = 1;
    public float speed2 = 1;

    public Transform target;
    public PlayerController playerController;
    public Boss boss;

    private void Start()
    {
        boss = GameObject.Find("Boss").GetComponent<Boss>();
        speed2 = Random.Range(2, 5);
        target = GameObject.Find("Player").GetComponent<Transform>();
        dis = Vector3.Distance(transform.position, target.position);
    }

    private float dis;

    private float waitTime;

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("온트리거엔터 발생");
        if (other.CompareTag("Player"))
        {
            playerController = other.GetComponent<PlayerController>();
            playerController.GetHit(fireDamage, gameObject.transform);
            //Debug.Log("플레이어한테 온트리거엔터 발생");

            Rigidbody rigid = other.GetComponent<Rigidbody>();

            rigid.AddForce((transform.forward + Vector3.up) * boss.knockBackForce, ForceMode.Impulse);
            Destroy(gameObject);
            //boxCollider.enabled = false;
        }
    }

    // Update is called once per frame
    private void Update()
    {
        //float translateMove = speed * Time.deltaTime;
        //transform.LookAt(target);
        //transform.Translate(transform.forward * translateMove);

        if (target == null) return;

        waitTime += Time.deltaTime;
        //1.5초 동안 천천히 forward 방향으로 전진합니다
        if (waitTime < 1.5f)
        {
            speed = Time.deltaTime;
            transform.Translate(transform.forward * speed, Space.World);
        }
        else if (waitTime < 3.0f)
        {
            // 1.5초 이후 타겟방향으로 lerp위치이동 합니다

            speed += Time.deltaTime;
            float t = speed / dis;

            transform.position = Vector3.LerpUnclamped(transform.position, target.position, t);

            Vector3 directionVec = target.position - transform.position;
            Quaternion qua = Quaternion.LookRotation(directionVec);
            transform.rotation = Quaternion.Slerp(transform.rotation, qua, Time.deltaTime * 2f);
        }
        else
        {
            transform.Translate(transform.forward * speed, Space.World);
        }

        // 매프레임마다 타겟방향으로 포탄이 방향을바꿉니다
        //타겟위치 - 포탄위치 = 포탄이 타겟한테서의 방향
    }
}