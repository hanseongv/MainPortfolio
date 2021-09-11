﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DropBox : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler, IDropHandler
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

    public bool hasEquip;

    public void OnDrop(PointerEventData eventData)
    {
        if (hasEquip)
            return;
        Debug.Log("Drop");

        playerData.changeNum[1] = num;

        //if (playerData.equipmentItemId[playerData.changeNum[0]] != 0)
        //{
        playerData.EuqipItem();
        inventoryUI.EquipItemPos();
        hasEquip = true;
        //}
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (!hasEquip)
            return;
        Debug.Log("스타트");

        //playerData.changeNum[0] = num;
        //if (playerData.equipmentItemId[playerData.changeNum[0]] != 0)
        //{
        inventoryUI.dragItemObj.SetActive(true);

        dragImage = inventoryUI.dragItemObj.GetComponent<Image>();
        dragImage.sprite = playerData.hasEquipmentItemSprite;
        //}

        //playerData.SwapItem();
        //inventoryUI.startDragId = playerData.equipmentItemId[num];

        //inventoryUI.startDragText = playerData.equipmentItemIntText[num];

        //inventoryUI.startDragNum = num;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!hasEquip)
            return;
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