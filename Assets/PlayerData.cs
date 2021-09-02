using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public GameObject player;
    public string playerName;
    public int level = 1;
    public int playerStr = 0;

    public int playerInt = 0;
    public int statPoints = 0;
    public int skillPoints = 0;

    public float playerSpeed = 1;
    public float playerJumpPower = 5;
    public int playerPhyDamage = 0;
    public int playerMagDamage = 0;
    public int curentHp = 50;
    public int maxHp = 50;
    public int curentMaxHp = 50;
    public int curentMp = 50;
    public int maxMp = 50;
    public int curentMaxMp = 50;
    public int[] maxExp;
    public int curentExp = 0;
    public int playerGold = 0;
    public List<int> playerPortion;

    public List<GameObject> haveWeapon;
    public GameObject equipWeapon;
    public float equipWeaponRate;
    public List<GameObject> camList;
    public GameObject curentCam;

    private void Awake()
    {
        player = GameObject.Find("Player");
        maxExp = new int[] { 15, 50, 100, 500, 1000 };
    }

    // Update is called once per frame
    private void Update()
    {
        LevelUp();
        Test();
        PlayerStata();
    }

    private void PlayerStata()
    {
        playerPhyDamage = playerStr * 2;
        playerMagDamage = playerInt * 2;
        maxHp = playerStr * 10 + curentMaxHp;
        maxMp = playerInt * 10 + curentMaxMp;
    }

    public void EquipWeapon(GameObject weaponObj, float rate)
    {
        equipWeapon = weaponObj;
        equipWeaponRate = rate;
    }

    private void Test()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            equipWeapon.SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            playerStr++;
            playerInt++;
        }
    }

    private void LevelUp()
    {
        if (maxExp.Length < level) return;
        else if (maxExp[level - 1] <= curentExp)
        {
            curentExp -= maxExp[level - 1];
            level++;
            statPoints += 5;
            skillPoints += 1;
        }
    }
}