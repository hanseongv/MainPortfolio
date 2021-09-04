﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public float rate = 0.5f;
    public int damage = 1;
    private PlayerData playerData;
    public BoxCollider boxCollider;
    private MonsterNormal monsterNormal;

    private void Awake()
    {
        playerData = GameObject.Find("GameManager").GetComponent<PlayerData>();

        playerData.EquipWeapon(gameObject, rate);
        gameObject.SetActive(false);
        boxCollider = GetComponent<BoxCollider>();
        boxCollider.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Monster"))
        {
            boxCollider.enabled = false;
            monsterNormal = other.GetComponent<MonsterNormal>();
            monsterNormal.GetHit(damage + playerData.playerPhyDamage, transform);
            Debug.Log($"무기 공격력 {damage}, 플레이어 데미지{playerData.playerPhyDamage}");
        }
    }

    // Update is called once per frame
    private void Update()
    {
    }
}