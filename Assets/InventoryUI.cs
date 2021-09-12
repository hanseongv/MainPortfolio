using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class InventoryUI : MonoBehaviour
{
    private int itemOldECount;
    private int itemOldCCount;
    private int itemOldOCount;

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

    private void Start()
    {
        //itemText.text = "0";
        inventory = GameObject.Find("InventoryUI");
        playerData = GameObject.Find("GameManager").GetComponent<PlayerData>();
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
            //equipmenItemTextList.Add(GameObject.Find("InventoryUI/Book/ContentsEquipment/Inventory/Viewport/Content5x4/Item (" + i + ")/Value").GetComponent<Text>());
            //equipmenItemOnImageList.Add(GameObject.Find("InventoryUI/Book/ContentsEquipment/Inventory/Viewport/Content5x4/Item (" + i + ")/OnImage").GetComponent<Image>());
            //playerData.equipmentItemIntText.Add(0);
            //playerData.equipmentItemId.Add(0);
            //playerData.equipmentItemSprite.Add(null);
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

        for (int i = 0; i < equipmenItemList.Count; i++)
        {
            consumableItemImageList.Add(GameObject.Find("InventoryUI/Book/ContentsConsumable/Inventory/Viewport/Content5x4/Item (" + i + ")/ItemImage").GetComponent<Image>());
            consumableItemTextList.Add(GameObject.Find("InventoryUI/Book/ContentsConsumable/Inventory/Viewport/Content5x4/Item (" + i + ")/Value").GetComponent<Text>());
            consumableItemOnImageList.Add(GameObject.Find("InventoryUI/Book/ContentsConsumable/Inventory/Viewport/Content5x4/Item (" + i + ")/OnImage").GetComponent<Image>());
            playerData.consumableItemIntText.Add(0);
            playerData.consumableItemId.Add(0);
            playerData.consumableItemSprite.Add(null);
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
        //changeSprite[0] = equipmenItemImageList[playerData.changeNum[0]].sprite;
        //changeIntText[0] = int.Parse("" + equipmenItemTextList[playerData.changeNum[0]].text);

        //changeSprite[1] = equipmenItemImageList[playerData.changeNum[1]].sprite;
        //changeIntText[1] = int.Parse("" + equipmenItemTextList[playerData.changeNum[1]].text);

        //equipmenItemImageList[playerData.changeNum[1]] = equipmenItemImageList[playerData.changeNum[0]];
        //equipmenItemTextList[playerData.changeNum[1]] = equipmenItemTextList[playerData.changeNum[0]];

        sprite = equipmenItemImageList[playerData.changeNum[0]].sprite;
        hasEquipBox.sprite = sprite;

        //equipmenItemImageList[playerData.changeNum[1]] = equipmenItemImageList[playerData.changeNum[0]];
        //equipmenItemTextList[playerData.changeNum[1]] = equipmenItemTextList[playerData.changeNum[0]];

        //equipmenItemImageList[playerData.changeNum[0]].sprite = nullSprite;
        //equipmenItemTextList[playerData.changeNum[0]].text = null;
        for (int i = 0; i < equipmenItemOnImageList.Count; i++)
        {
            equipmenItemOnImageList[i].enabled = false;
        }
        equipmenItemOnImageList[playerData.changeNum[0]].enabled = true;
        Debug.Log("인벤첸지");
    }

    public void ChangeItemPos()
    {
        changeSprite[0] = equipmenItemImageList[playerData.changeNum[0]].sprite;
        changeIntText[0] = int.Parse("" + equipmenItemTextList[playerData.changeNum[0]].text);

        changeSprite[1] = equipmenItemImageList[playerData.changeNum[1]].sprite;
        changeIntText[1] = int.Parse("" + equipmenItemTextList[playerData.changeNum[1]].text);

        //equipmenItemImageList[playerData.changeNum[1]] = equipmenItemImageList[playerData.changeNum[0]];
        //equipmenItemTextList[playerData.changeNum[1]] = equipmenItemTextList[playerData.changeNum[0]];

        equipmenItemImageList[playerData.changeNum[0]].sprite = changeSprite[1];
        equipmenItemTextList[playerData.changeNum[0]].text = "" + changeIntText[1];
        equipmenItemImageList[playerData.changeNum[1]].sprite = changeSprite[0];
        equipmenItemTextList[playerData.changeNum[1]].text = "" + changeIntText[0];

        Debug.Log("인벤첸지");
    }

    private void Update()
    {
        if (itemOldECount < playerData.itemECount)
        {
            //for (int i = 0; i < playerData.itemId.Count; i++)
            //{
            //    if (playerData.itemId[i] == 0)
            //    {
            equipmenItemImageList[playerData.inventoryAddNum].sprite = playerData.equipmentItemSprite[playerData.inventoryAddNum];

            int oldNum = int.Parse("" + equipmenItemTextList[playerData.inventoryAddNum].text);
            int addNum = playerData.equipmentItemIntText[playerData.inventoryAddNum];
            oldNum += addNum;
            equipmenItemTextList[playerData.inventoryAddNum].text = "" + oldNum;
            itemOldECount = playerData.itemECount;
            //i = playerData.itemId.Count;
            //    }
            //}
        }

        if (itemOldCCount < playerData.itemCCount)
        {
            int oldNum = int.Parse("" + consumableItemTextList[playerData.inventoryAddNum].text);
            int addNum = playerData.consumableItemIntText[playerData.inventoryAddNum];
            oldNum += addNum;

            consumableItemTextList[playerData.inventoryAddNum].text = "" + oldNum;
            itemOldCCount = playerData.itemCCount;
            if (playerData.consumableOnlyAddNum)
                return;
            consumableItemImageList[playerData.inventoryAddNum].sprite = playerData.consumableItemSprite[playerData.inventoryAddNum];
        }
        //else if (itemOldCCount > playerData.itemCCount)
        //{
        //    int oldNum = int.Parse("" + consumableItemTextList[playerData.inventoryAddNum].text);
        //    int addNum = playerData.consumableItemText[playerData.inventoryAddNum];
        //    oldNum -= addNum;

        //    consumableItemTextList[playerData.inventoryAddNum].text = "" + oldNum;
        //    itemOldCCount = playerData.itemCCount;
        //    if (playerData.consumableOnlyAddNum)
        //        return;
        //    consumableItemImageList[playerData.inventoryAddNum].sprite = playerData.consumableItemSprite[playerData.inventoryAddNum];
        //}
        if (itemOldOCount < playerData.itemOCount)
        {
            itemOldOCount = playerData.itemOCount;
        }
    }

    private void OnEnable()
    {
    }
}