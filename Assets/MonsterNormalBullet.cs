using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterNormalBullet : MonoBehaviour
{
    public MonsterNormal monsterNormal;
    public PlayerController playerController;
    private BoxCollider boxCollider;

    private void Start()
    {
        monsterNormal = GetComponentInParent<MonsterNormal>();
        boxCollider = GetComponent<BoxCollider>();
    }

    private void Update()
    {
    }

    public float force = 100;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("온트리거엔터 발생");
        if (other.CompareTag("Player"))
        {
            playerController = other.GetComponent<PlayerController>();
            playerController.GetHit(monsterNormal.monsterDamage, gameObject.transform);
            Debug.Log("플레이어한테 온트리거엔터 발생");

            Rigidbody rigid = other.GetComponent<Rigidbody>();

            rigid.AddForce((transform.forward + Vector3.up) * monsterNormal.knockBackForce, ForceMode.Impulse);
            boxCollider.enabled = false;
        }
        //else
        //{
        //    boxCollider.enabled = false;
        //}
    }
}