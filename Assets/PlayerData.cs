using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public GameObject player;
    public MonsterNormal monsterNormal;
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

    public int skill1Level = 1;//테스트때만1
    public List<GameObject> hasWeapon;
    public List<GameObject> testHaveWeapon;
    public List<GameObject> testHaveWeapon2;
    public GameObject equipWeapon;
    public GameObject equipWeaponEffect;
    public TrailRenderer equipWeaponTrail;
    public float equipWeaponRate;
    public List<GameObject> camList;
    public GameObject curentCam;
    public BoxCollider equipWeaponBoxColl;

    //public List<int> itemIndex;
    //public List<int> hasEquipItem;
    public List<int> itemId;

    public List<Sprite> itemSprite;
    public int itemECount;
    public int itemCCount;

    public int itemOCount;

    private void Awake()
    {
        player = GameObject.Find("Player");
        maxExp = new int[] { 15, 50, 100, 500, 1000 };

        equipWeapon = hasWeapon[0];
    }

    // Update is called once per frame
    private void Update()
    {
        LevelUp();
        Test();
        PlayerStata();
        EquipWeapon();
    }

    private void EquipWeapon()
    {
        equipWeapon.SetActive(true);
        equipWeaponBoxColl = equipWeapon.GetComponent<BoxCollider>();
        //if (equipWeapon != hasWeapon[0])
        //    //    equipWeaponEffect = equipWeapon.GetComponent<GameObject>();
        //    //GameObject.Find("Panel").transform.GetChild(0).gameObject;
        //    equipWeaponEffect = equipWeapon.transform.GetChild(0).gameObject;
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

    public void GetItem(int id, ItemData.Type type, Sprite sprite)
    {
        switch (type)
        {
            case ItemData.Type.Equipment:
                itemECount++;
                break;

            case ItemData.Type.Consumable:
                itemCCount++;
                break;

            case ItemData.Type.Other:
                itemOCount++;
                break;
        }

        itemId.Add(id);
        itemSprite.Add(sprite);
    }

    private int i;
    private int b;

    private void Test()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            i++;
            hasWeapon[i] = testHaveWeapon[i - 1];
        }
        if (Input.GetKeyDown(KeyCode.J))
        {
            b++;
            hasWeapon[b] = testHaveWeapon2[b - 1];
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