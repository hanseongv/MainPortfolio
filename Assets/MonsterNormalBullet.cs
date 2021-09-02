using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterNormalBullet : MonoBehaviour
{
    public int damage;
    public MonsterNormal monsterNormal;
    public PlayerController playerController;

    private void Start()
    {
        monsterNormal = GetComponentInParent<MonsterNormal>();
        damage = monsterNormal.monsterDamage;
    }

    // Update is called once per frame
    private void Update()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("온트리거엔터 발생");
        if (other.CompareTag("Player"))
        {
            playerController = other.GetComponent<PlayerController>();
            playerController.GetHit(damage);
            Debug.Log("플레이어한테 온트리거엔터 발생");
            //playerController = null;
        }
    }
}