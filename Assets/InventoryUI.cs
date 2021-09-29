using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class InventoryUI : MonoBehaviour
{
    public int itemOldECount;
    public int itemOldCCount;
    public int itemOldOCount;

    public GameObject contentsEquipment;
    public GameObject contentsConsumable;
    public GameObject contentsOther;
    public GameObject inventory;
    public List<GameObject> contentsList;

    public List<GameObject> equipmenItemList;
    public List<Image> equipmenItemImageList;
    public Image hasEquipBox;
    public List<Text> equipmenItemTextList;
    public List<Image> equipmenItemOnImageList;

    public List<GameObject> consumableItemList;
    public List<Image> consumableItemImageList;
    public List<Text> consumableItemTextList;
    public List<Image> consumableItemOnImageList;

    public List<GameObject> otherItemList;
    public List<Image> otherItemImageList;
    public List<Text> otherItemTextList;
    public List<Image> otherItemOnImageList;
    //public List<GameObject> otherList;

    //public GameObject startDragObj;
    public int startDragId;

    public int startDragNum;
    public Image startDragImage;
    public int startDragText;

    public int endDragId;
    public Image endDragImage;
    public int endDragText;

    public PlayerData playerData;
    public UiScript uiScript;

    private enum CountentsType
    { Equipment, Consumable, Other };

    public Sprite spriteNull;
    private CountentsType countentsType;
    private PointerEventData pointerEvent;
    private bool contentsEquipmentB;
    private bool contentsConsumableB;
    private bool contentsOtherB;
    private List<string> contentsName = new List<string> { "Equipment", "Consumable", "Other" };
    public GameObject dragItemObj;

    //public Image dragItemImage;
    //private List<string> contentsBName = new List<string> { "contentsWeapon", "contentsExpendables", "contentsOther" };
    //public Text itemText;
    public List<Slot> slotEquip;

    public List<Slot> slotC;
    public List<Slot> slotO;

    private void Start()
    {
        //itemText.text = "0";
        inventory = GameObject.Find("InventoryUI");
        playerData = GameObject.Find("GameManager").GetComponent<PlayerData>();
        uiScript = GameObject.Find("UI").GetComponent<UiScript>();
        //contentsWeapon = GameObject.Find("Book/ContentsWeapon");
        //contentsExpendables = GameObject.Find("Book/ContentsExpendables");
        //contentsOther = GameObject.Find("Book/ContentsOther");
        hasEquipBox = GameObject.Find("InventoryUI/EquipUI/WeaponBox/ItemImage").GetComponent<Image>();
        dragItemObj = GameObject.Find("InventoryUI/DragItemImage");
        //dragItemImage = dragItemObj.GetComponent<Image>();
        dragItemObj.SetActive(false);
        contentsList.Add(GameObject.Find("InventoryUI/Book/ContentsEquipment"));
        contentsList.Add(GameObject.Find("InventoryUI/Book/ContentsConsumable"));
        contentsList.Add(GameObject.Find("InventoryUI/Book/ContentsOther"));
        transform.Find("InventoryUI/Book/Category/Equipment").GetComponent<Button>().onClick.AddListener(
        () => OnContents(CountentsType.Equipment));
        transform.Find("InventoryUI/Book/Category/Consumable").GetComponent<Button>().onClick.AddListener(
        () => OnContents(CountentsType.Consumable));
        transform.Find("InventoryUI/Book/Category/Other").GetComponent<Button>().onClick.AddListener(
        () => OnContents(CountentsType.Other));

        contentsList[0].SetActive(true);
        for (int i = 1; i < contentsList.Count; i++)
        {
            contentsList[i].SetActive(false);
        }
        for (int i = 0; i < equipmenItemList.Count; i++)
        {
            slotEquip[i] = GameObject.Find("InventoryUI/Book/ContentsEquipment/Inventory/Viewport/Content5x4/Item (" + i + ")").GetComponent<Slot>();
            slotEquip[i].num = i;
        }
        for (int i = 0; i < consumableItemList.Count; i++)
        {
            slotC[i] = GameObject.Find("InventoryUI/Book/ContentsConsumable/Inventory/Viewport/Content5x4/Item (" + i + ")").GetComponent<Slot>();
            slotC[i].num = i;
        }
        for (int i = 0; i < otherItemList.Count; i++)
        {
            slotO[i] = GameObject.Find("InventoryUI/Book/ContentsOther/Inventory/Viewport/Content5x4/Item (" + i + ")").GetComponent<Slot>();
            slotO[i].num = i;
        }
        for (int i = 0; i < equipmenItemList.Count; i++)
        {
            equipmenItemImageList.Add(GameObject.Find("InventoryUI/Book/ContentsEquipment/Inventory/Viewport/Content5x4/Item (" + i + ")/ItemImage").GetComponent<Image>());
            equipmenItemTextList.Add(GameObject.Find("InventoryUI/Book/ContentsEquipment/Inventory/Viewport/Content5x4/Item (" + i + ")/Value").GetComponent<Text>());
            equipmenItemOnImageList.Add(GameObject.Find("InventoryUI/Book/ContentsEquipment/Inventory/Viewport/Content5x4/Item (" + i + ")/OnImage").GetComponent<Image>());
            equipmenItemOnImageList[i].enabled = false;
            playerData.equipmentItemIntText.Add(0);
            playerData.equipmentItemId.Add(0);
            playerData.equipmentItemSprite.Add(null);
        }

        for (int i = 0; i < consumableItemList.Count; i++)
        {
            consumableItemImageList.Add(GameObject.Find("InventoryUI/Book/ContentsConsumable/Inventory/Viewport/Content5x4/Item (" + i + ")/ItemImage").GetComponent<Image>());
            consumableItemTextList.Add(GameObject.Find("InventoryUI/Book/ContentsConsumable/Inventory/Viewport/Content5x4/Item (" + i + ")/Value").GetComponent<Text>());
            consumableItemOnImageList.Add(GameObject.Find("InventoryUI/Book/ContentsConsumable/Inventory/Viewport/Content5x4/Item (" + i + ")/OnImage").GetComponent<Image>());
            playerData.consumableItemIntText.Add(0);
            playerData.consumableItemId.Add(0);
            playerData.consumableItemSprite.Add(null);
        }

        for (int i = 0; i < otherItemList.Count; i++)
        {
            otherItemImageList.Add(GameObject.Find("InventoryUI/Book/ContentsOther/Inventory/Viewport/Content5x4/Item (" + i + ")/ItemImage").GetComponent<Image>());
            otherItemTextList.Add(GameObject.Find("InventoryUI/Book/ContentsOther/Inventory/Viewport/Content5x4/Item (" + i + ")/Value").GetComponent<Text>());
            otherItemOnImageList.Add(GameObject.Find("InventoryUI/Book/ContentsOther/Inventory/Viewport/Content5x4/Item (" + i + ")/OnImage").GetComponent<Image>());
            playerData.otherItemIntText.Add(0);
            playerData.otherItemId.Add(0);
            playerData.otherItemSprite.Add(null);
        }
        //    contentsList[0].SetActive(contentsWeaponB);
        //contentsExpendables.SetActive(contentsExpendablesB);
        //contentsOther.SetActive(contentsOtherB);
        inventory.SetActive(false);
    }

    private void OnContents(CountentsType type)
    {
        for (int i = 0; i < contentsList.Count; i++)
        {
            contentsList[i].SetActive(false);
        }

        switch (type)
        {
            case CountentsType.Equipment:
                contentsList[0].SetActive(true);
                //contentsList[0].SetActive(!contentsWeaponB);
                //contentsWeaponB = !contentsWeaponB;
                //contentsExpendablesB = false;
                //contentsOtherB = false;
                break;

            case CountentsType.Consumable:
                contentsList[1].SetActive(true);
                //contentsList[1].SetActive(!contentsExpendablesB);
                //contentsExpendablesB = !contentsExpendablesB;
                //contentsWeaponB = false;
                //contentsOtherB = false;
                break;

            case CountentsType.Other:
                contentsList[2].SetActive(true);
                //contentsList[2].SetActive(!contentsOtherB);
                //contentsOtherB = !contentsOtherB;
                //contentsExpendablesB = false;
                //contentsWeaponB = false;
                break;
        }
    }

    public List<int> changeIntText;
    public List<Sprite> changeSprite;
    private Sprite sprite;
    public Sprite nullSprite;

    public void UnEquipItemPos()
    {
        sprite = nullSprite;
        hasEquipBox.sprite = sprite;
        for (int i = 0; i < equipmenItemOnImageList.Count; i++)
        {
            equipmenItemOnImageList[i].enabled = false;
        }
    }

    public void EquipItemPos()
    {
        sprite = equipmenItemImageList[playerData.changeNum[0]].sprite;
        hasEquipBox.sprite = sprite;

        for (int i = 0; i < equipmenItemOnImageList.Count; i++)
        {
            equipmenItemOnImageList[i].enabled = false;
        }
        equipmenItemOnImageList[playerData.changeNum[0]].enabled = true;
        Debug.Log("인벤첸지");
    }

    public void DropItem()
    {
        switch (playerData.changeItemType)
        {
            case ItemData.ItemType.Equipment:
                itemOldECount = playerData.itemECount;
                equipmenItemImageList[playerData.changeNum[0]].sprite = nullSprite;
                equipmenItemTextList[playerData.changeNum[0]].text = "0";
                break;

            case ItemData.ItemType.Consumable:
                itemOldCCount = playerData.itemCCount;
                if (playerData.consumableItemIntText[playerData.changeCNum[0]] > 1)
                {
                    playerData.consumableItemIntText[playerData.changeCNum[0]]--;
                    consumableItemTextList[playerData.changeCNum[0]].text = "" + playerData.consumableItemIntText[playerData.changeCNum[0]];
                }
                else
                {
                    consumableItemImageList[playerData.changeCNum[0]].sprite = nullSprite;
                    consumableItemTextList[playerData.changeCNum[0]].text = "0";
                    playerData.consumableItemId[playerData.changeCNum[0]] = 0;
                    playerData.consumableItemIntText[playerData.changeCNum[0]] = 0;
                    playerData.consumableItemSprite[playerData.changeCNum[0]] = null;
                }
                break;

            case ItemData.ItemType.Other:
                itemOldOCount = playerData.itemOCount;
                otherItemImageList[playerData.changeONum[0]].sprite = nullSprite;
                otherItemTextList[playerData.changeONum[0]].text = "0";
                break;
        }
    }

    public void ChangeItemPos()
    {
        changeSprite[0] = equipmenItemImageList[playerData.changeNum[0]].sprite;
        changeIntText[0] = int.Parse("" + equipmenItemTextList[playerData.changeNum[0]].text);

        changeSprite[1] = equipmenItemImageList[playerData.changeNum[1]].sprite;
        changeIntText[1] = int.Parse("" + equipmenItemTextList[playerData.changeNum[1]].text);

        equipmenItemImageList[playerData.changeNum[0]].sprite = changeSprite[1];
        equipmenItemTextList[playerData.changeNum[0]].text = "" + changeIntText[1];
        equipmenItemImageList[playerData.changeNum[1]].sprite = changeSprite[0];
        equipmenItemTextList[playerData.changeNum[1]].text = "" + changeIntText[0];

        Debug.Log("인벤첸지");
    }

    public void ChangeItemConsumablePos()
    {
        changeSprite[0] = consumableItemImageList[playerData.changeCNum[0]].sprite;
        changeIntText[0] = int.Parse("" + consumableItemTextList[playerData.changeCNum[0]].text);

        changeSprite[1] = consumableItemImageList[playerData.changeCNum[1]].sprite;
        changeIntText[1] = int.Parse("" + consumableItemTextList[playerData.changeCNum[1]].text);

        consumableItemImageList[playerData.changeCNum[0]].sprite = changeSprite[1];
        consumableItemTextList[playerData.changeCNum[0]].text = "" + changeIntText[1];
        consumableItemImageList[playerData.changeCNum[1]].sprite = changeSprite[0];
        consumableItemTextList[playerData.changeCNum[1]].text = "" + changeIntText[0];

        Debug.Log("인벤첸지");
    }

    public void ChangeItemOtherPos()
    {
        changeSprite[0] = otherItemImageList[playerData.changeONum[0]].sprite;
        changeIntText[0] = int.Parse("" + otherItemTextList[playerData.changeONum[0]].text);

        changeSprite[1] = otherItemImageList[playerData.changeONum[1]].sprite;
        changeIntText[1] = int.Parse("" + otherItemTextList[playerData.changeONum[1]].text);

        otherItemImageList[playerData.changeONum[0]].sprite = changeSprite[1];
        otherItemTextList[playerData.changeONum[0]].text = "" + changeIntText[1];
        otherItemImageList[playerData.changeONum[1]].sprite = changeSprite[0];
        otherItemTextList[playerData.changeONum[1]].text = "" + changeIntText[0];
    }

    private void Update()
    {
        if (itemOldECount < playerData.itemECount)
        {
            equipmenItemImageList[playerData.inventoryAddNum].sprite = playerData.equipmentItemSprite[playerData.inventoryAddNum];

            int oldNum = int.Parse("" + equipmenItemTextList[playerData.inventoryAddNum].text);
            int addNum = playerData.equipmentItemIntText[playerData.inventoryAddNum];
            oldNum += addNum;
            equipmenItemTextList[playerData.inventoryAddNum].text = "" + oldNum;
            itemOldECount = playerData.itemECount;
        }

        if (itemOldCCount < playerData.itemCCount)
        {
            int oldNum = int.Parse("" + consumableItemTextList[playerData.inventoryAddNum].text); //0
            int addNum = playerData.consumableItemIntText[playerData.inventoryAddNum]; //1
            oldNum = addNum;

            for (int i = 0; i < playerData.consumableItemId.Count; i++)
            {
                if (playerData.consumableItemId[i] == 101)
                {
                    playerData.hpCount = playerData.consumableItemIntText[i];
                    //return;
                }
            }
            uiScript.portionText.text = $"{playerData.hpCount}";

            consumableItemTextList[playerData.inventoryAddNum].text = "" + oldNum;
            itemOldCCount = playerData.itemCCount;
            if (playerData.consumableOnlyAddNum)
                return;
            consumableItemImageList[playerData.inventoryAddNum].sprite = playerData.consumableItemSprite[playerData.inventoryAddNum];
        }

        if (itemOldOCount < playerData.itemOCount)
        {
            int oldNum = int.Parse("" + otherItemTextList[playerData.inventoryAddNum].text); //0
            int addNum = playerData.otherItemIntText[playerData.inventoryAddNum]; //1
            oldNum = addNum;

            otherItemTextList[playerData.inventoryAddNum].text = "" + oldNum;
            itemOldOCount = playerData.itemOCount;
            if (playerData.otherOnlyAddNum)
                return;
            otherItemImageList[playerData.inventoryAddNum].sprite = playerData.otherItemSprite[playerData.inventoryAddNum];
        }
    }

    public void PortionC()
    {
        for (int i = 0; i < playerData.consumableItemId.Count; i++)
        {
            if (playerData.consumableItemId[i] == 101)
            {
                if (playerData.hpCount >= 1)
                {
                    //uiScript.portionText.text = $"{playerData.hpCount}";
                    playerData.consumableItemIntText[i] = playerData.hpCount;
                    consumableItemTextList[i].text = $"{playerData.hpCount}";
                }
                else
                {
                    //uiScript.portionText.text =
                    consumableItemImageList[i].sprite = nullSprite;
                    consumableItemTextList[i].text = "0";
                    playerData.consumableItemIntText[i] = 0;
                    playerData.consumableItemId[i] = 0;
                    playerData.consumableItemSprite[i] = null;
                }
                return;
            }
        }
    }

    private void OnEnable()
    {
    }
}