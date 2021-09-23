using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    //public List<int> playerPortion;

    public int skill1Level = 1;//테스트때만1
    public List<GameObject> hasWeapon;
    public List<GameObject> weaponList;

    public GameObject equipWeapon;
    public GameObject equipWeaponEffect;
    public TrailRenderer equipWeaponTrail;
    public float equipWeaponRate;
    public List<GameObject> camList;
    public GameObject curentCam;
    public BoxCollider equipWeaponBoxColl;
    private PlayerController playerController;

    //public List<int> itemIndex;
    //public List<int> hasEquipItem;
    public List<int> equipmentItemId;

    public List<int> equipmentItemIntText;

    public List<Sprite> equipmentItemSprite;

    public int hasEquipmentItemId;

    public int hasEquipmentItemIntText;

    public Sprite hasEquipmentItemSprite;
    //public List<int> changeId;
    //public List<int> changeIntText;
    //public List<Sprite> changeSprite;

    public int changeId;
    public int changeIntText;
    public Sprite changeSprite;

    public List<int> changeNum;
    public List<int> changeCNum;
    public List<int> changeONum;

    public ItemData.ItemType changeItemType;

    public List<int> consumableItemId;

    public List<int> consumableItemIntText;

    public List<Sprite> consumableItemSprite;

    public List<int> otherItemId;
    public List<int> otherItemIntText;
    public List<Sprite> otherItemSprite;

    public int itemECount;
    public int itemCCount;

    public int itemOCount;
    public InventoryUI inventoryUI;

    public float skill1Time = 15;
    public float skill1TimeMax = 15;
    public bool skill1B;

    public int hpCount;

    private void Skill()
    {
        if (skill1B)
        {
            skill1Time -= Time.deltaTime;
            if (skill1Time <= 0)
            {
                skill1Time = skill1TimeMax;
                skill1B = false;
            }
        }
    }

    public void DropItem()
    {
        switch (changeItemType)
        {
            case ItemData.ItemType.Equipment:
                equipmentItemId[changeNum[0]] = 0;
                equipmentItemIntText[changeNum[0]] = 0;
                equipmentItemSprite[changeNum[0]] = null;
                break;

            case ItemData.ItemType.Consumable:
                if (consumableItemId[changeCNum[0]] == 101)
                    hpCount--;
                if (consumableItemIntText[changeCNum[0]] >= 1)
                    return;
                else
                {
                    consumableItemId[changeCNum[0]] = 0;
                    consumableItemIntText[changeCNum[0]] = 0;
                    consumableItemSprite[changeCNum[0]] = null;
                }
                break;

            case ItemData.ItemType.Other:
                otherItemId[changeONum[0]] = 0;
                otherItemIntText[changeONum[0]] = 0;
                otherItemSprite[changeONum[0]] = null;
                break;
        }
    }

    public void SwapItem()
    {
        changeId = equipmentItemId[changeNum[0]];
        changeIntText = equipmentItemIntText[changeNum[0]];
        changeSprite = equipmentItemSprite[changeNum[0]];

        equipmentItemId[changeNum[0]] = equipmentItemId[changeNum[1]];
        equipmentItemIntText[changeNum[0]] = equipmentItemIntText[changeNum[1]];
        equipmentItemSprite[changeNum[0]] = equipmentItemSprite[changeNum[1]];

        equipmentItemId[changeNum[1]] = changeId;
        equipmentItemIntText[changeNum[1]] = changeIntText;
        equipmentItemSprite[changeNum[1]] = changeSprite;
        Debug.Log("플레이어첸지");
    }

    public void SwapItemConsumable()
    {
        changeId = consumableItemId[changeCNum[0]];
        changeIntText = consumableItemIntText[changeCNum[0]];
        changeSprite = consumableItemSprite[changeCNum[0]];

        consumableItemId[changeCNum[0]] = consumableItemId[changeCNum[1]];
        consumableItemIntText[changeCNum[0]] = consumableItemIntText[changeCNum[1]];
        consumableItemSprite[changeCNum[0]] = consumableItemSprite[changeCNum[1]];

        consumableItemId[changeCNum[1]] = changeId;
        consumableItemIntText[changeCNum[1]] = changeIntText;
        consumableItemSprite[changeCNum[1]] = changeSprite;
        Debug.Log("플레이어첸지");
    }

    public void SwapItemOther()
    {
        changeId = otherItemId[changeONum[0]];
        changeIntText = otherItemIntText[changeONum[0]];
        changeSprite = otherItemSprite[changeONum[0]];

        otherItemId[changeONum[0]] = otherItemId[changeONum[1]];
        otherItemIntText[changeONum[0]] = otherItemIntText[changeONum[1]];
        otherItemSprite[changeONum[0]] = otherItemSprite[changeONum[1]];

        otherItemId[changeONum[1]] = changeId;
        otherItemIntText[changeONum[1]] = changeIntText;
        otherItemSprite[changeONum[1]] = changeSprite;
        Debug.Log("플레이어첸지");
    }

    public void UnEquip()
    {
        equipWeapon.SetActive(false);
        hasWeapon[1] = null;//weaponList[0]; 20210914 수정

        equipWeapon = hasWeapon[0];
        equipWeaponTrail = null;
        equipWeaponEffect = null;
        playerController.skill1SwordRenderer = null;
    }

    public void EuqipItem()
    {
        equipWeapon.SetActive(false);
        hasWeapon[1] = weaponList[equipmentItemId[changeNum[0]]];
        if (equipWeapon != hasWeapon[0])
        {
            equipWeapon = hasWeapon[1];
            equipWeaponTrail = equipWeapon.GetComponentInChildren<TrailRenderer>();
            equipWeaponEffect = equipWeapon.transform.GetChild(0).gameObject;
            playerController.skill1SwordRenderer = equipWeapon.GetComponent<Renderer>();
            equipWeaponTrail.emitting = false;
            equipWeaponEffect.SetActive(false);
        }

        Debug.Log("플레이어첸지");
    }

    private void Awake()
    {
        player = GameObject.Find("Player");
        playerController = player.GetComponent<PlayerController>();
        inventoryUI = GameObject.Find("UI").GetComponent<InventoryUI>();
        maxExp = new int[] { 15, 50, 100, 500, 1000 };

        equipWeapon = hasWeapon[0];
    }

    // Update is called once per frame
    private void Update()
    {
        LevelUp();

        PlayerStata();
        EquipWeapon();
        Skill();
    }

    private void EquipWeapon()
    {
        equipWeapon.SetActive(true);
        equipWeaponBoxColl = equipWeapon.GetComponent<BoxCollider>();
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

    public int inventoryAddNum;
    public bool consumableOnlyAddNum;
    public bool otherOnlyAddNum;
    private Slot slot;
    private Image image;

    public void GetItem(int id, ItemData.ItemType type, Sprite sprite, int count)
    {
        switch (type)
        {
            case ItemData.ItemType.Equipment:
                itemECount++;
                for (int i = 0; i < equipmentItemId.Count; i++)
                {
                    if (equipmentItemId[i] == 0)
                    {
                        equipmentItemId[i] = id;
                        equipmentItemSprite[i] = sprite;

                        equipmentItemIntText[i] += count;
                        //inventoryUI.slotEquip[i].enabled = true;
                        image = inventoryUI.slotEquip[i].GetComponent<Image>();
                        image.raycastTarget = true;
                        inventoryAddNum = i;
                        i = equipmentItemId.Count;
                    }
                }
                break;

            case ItemData.ItemType.Consumable:
                itemCCount++;
                Debug.Log("일반");
                if (id == 101)
                    hpCount += count;
                for (int i = 0; i < consumableItemId.Count; i++)
                {
                    if (consumableItemId[i] == id)
                    {
                        consumableItemIntText[i] += count;
                        inventoryAddNum = i;
                        i = consumableItemId.Count;
                        consumableOnlyAddNum = true;
                        Debug.Log("트루");
                        //if (consumableItemId[i] == 101)
                        //    hpCount += count;
                    }
                    else
                    {
                        consumableOnlyAddNum = false;
                    }
                }

                if (!consumableOnlyAddNum)
                    for (int i = 0; i < consumableItemId.Count; i++)
                    {
                        if (consumableItemId[i] == 0)
                        {
                            Debug.Log("펄스");
                            consumableItemId[i] = id;
                            consumableItemSprite[i] = sprite;

                            consumableItemIntText[i] += count;
                            inventoryAddNum = i;
                            i = consumableItemId.Count;
                            consumableOnlyAddNum = false;
                        }
                    }
                break;

            case ItemData.ItemType.Other:
                itemOCount++;
                Debug.Log("일반");
                for (int i = 0; i < otherItemId.Count; i++)
                {
                    if (otherItemId[i] == id)
                    {
                        otherItemIntText[i] += count;
                        inventoryAddNum = i;
                        i = otherItemId.Count;
                        otherOnlyAddNum = true;
                        Debug.Log("트루");
                    }
                    else
                    {
                        otherOnlyAddNum = false;
                    }
                }

                if (!otherOnlyAddNum)
                    for (int i = 0; i < otherItemId.Count; i++)
                    {
                        if (otherItemId[i] == 0)
                        {
                            Debug.Log("펄스");
                            otherItemId[i] = id;
                            otherItemSprite[i] = sprite;

                            otherItemIntText[i] += count;
                            inventoryAddNum = i;
                            i = otherItemId.Count;
                            otherOnlyAddNum = false;
                        }
                    }

                break;
        }

        //consumableItemId
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