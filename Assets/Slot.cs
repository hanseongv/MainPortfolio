﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler, IDropHandler
{
    public int num;
    public ItemData.Type type;
    public PlayerData playerData;
    public GameObject dragObj;
    public Image dragImage;
    public Image startDragImage;
    public InventoryUI inventoryUI;
    private Slot slot1;
    private Slot slot2;
    public DropBox dropBox;

    private void Start()
    {
        dropBox = GameObject.Find("InventoryUI/EquipUI/WeaponBox/ItemImage").GetComponent<DropBox>();
    }

    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("Drop");
        // throw new System.NotImplementedException();

        if (dropBox.hasEquip)
        {
            playerData.equipmentItemId[num] = playerData.hasEquipmentItemId;
            playerData.equipmentItemIntText[num] = playerData.hasEquipmentItemIntText;
            playerData.equipmentItemSprite[num] = playerData.hasEquipmentItemSprite;

            //changeSprite[0] = equipmenItemImageList[playerData.changeNum[0]].sprite;
            //changeIntText[0] = int.Parse("" + equipmenItemTextList[playerData.changeNum[0]].text);

            //changeSprite[1] = equipmenItemImageList[playerData.changeNum[1]].sprite;
            //changeIntText[1] = int.Parse("" + equipmenItemTextList[playerData.changeNum[1]].text);

            inventoryUI.equipmenItemImageList[num].sprite = playerData.hasEquipmentItemSprite;
            inventoryUI.equipmenItemTextList[num].text = "" + playerData.hasEquipmentItemIntText;
            //텍스트 인트
            inventoryUI.hasEquipBox.sprite = inventoryUI.nullSprite;
            dropBox.hasEquip = false;
        }
        else
        {
            playerData.changeNum[1] = num;

            if (playerData.equipmentItemId[playerData.changeNum[0]] != 0)
            {
                playerData.SwapItem();
                inventoryUI.ChangeItemPos();
            }
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("스타트");

        playerData.changeNum[0] = num;
        if (playerData.equipmentItemId[playerData.changeNum[0]] != 0)
        {
            inventoryUI.dragItemObj.SetActive(true);

            dragImage = inventoryUI.dragItemObj.GetComponent<Image>();
            dragImage.sprite = playerData.equipmentItemSprite[num];
        }

        //playerData.SwapItem();
        //inventoryUI.startDragId = playerData.equipmentItemId[num];

        //inventoryUI.startDragText = playerData.equipmentItemIntText[num];

        //inventoryUI.startDragNum = num;
    }

    public void OnDrag(PointerEventData eventData)
    {
        inventoryUI.dragItemObj.transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("끝");
        inventoryUI.dragItemObj.SetActive(false);
    }

    private void Awake()
    {
        //dragObj = GameObject.Find("UI/InventoryUI/DragItemImage");
        //dragImage = dragObj.GetComponent<Image>();
        playerData = GameObject.Find("GameManager").GetComponent<PlayerData>();
        inventoryUI = GameObject.Find("UI").GetComponent<InventoryUI>();
    }

    //public void OnBeginDrag(PointerEventData eventData)
    //{
    //    Debug.Log("스타트");

    //    //inventoryUI.dragItemObj.SetActive(true); // 드래그아이템이미지 켜기
    //    //dragImage = inventoryUI.dragItemObj.GetComponent<Image>();
    //    //dragImage.sprite = playerData.equipmentItemSprite[num];

    //    //inventoryUI.startDragId = playerData.equipmentItemId[num];

    //    //inventoryUI.startDragText = playerData.equipmentItemText[num];

    //    //inventoryUI.startDragNum = num;
    //}

    //public void OnDrag(PointerEventData eventData)
    //{
    //    //inventoryUI.dragItemObj.transform.position = eventData.position;
    //}

    //public void OnEndDrag(PointerEventData eventData)
    //{
    //    Debug.Log("EndDrag");
    //    //inventoryUI.dragItemObj.SetActive(false);
    //}
}