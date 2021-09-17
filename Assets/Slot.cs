using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler, IDropHandler, IPointerClickHandler
{
    public int num;
    public ItemData.Type type;
    public PlayerData playerData;
    public GameObject dragObj;
    public Image dragImage;
    public Image startDragImage;
    public InventoryUI inventoryUI;
    public Slot slot;

    public DropBox dropBox;
    public bool lockBox;

    private void Start()
    {
        dropBox = GameObject.Find("InventoryUI/EquipUI/WeaponBox/ItemImage").GetComponent<DropBox>();
    }

    public void OnDrop(PointerEventData eventData)
    {
        switch (type)
        {
            case ItemData.Type.Equipment:
                Debug.Log("Drop");

                playerData.changeNum[1] = num;

                if (playerData.equipmentItemId[playerData.changeNum[0]] != 0 && num != dropBox.lockBoxNum && playerData.changeNum[0] != dropBox.lockBoxNum)
                {
                    playerData.SwapItem();
                    inventoryUI.ChangeItemPos();
                }
                break;

            case ItemData.Type.Consumable:

                Debug.Log("Drop");

                playerData.changeCNum[1] = num;
                if (playerData.consumableItemId[playerData.changeCNum[0]] != 0)
                {
                    playerData.SwapItemConsumable();
                    inventoryUI.ChangeItemConsumablePos();
                }
                break;

            case ItemData.Type.Other:

                Debug.Log("Drop");

                playerData.changeONum[1] = num;
                if (playerData.otherItemId[playerData.changeONum[0]] != 0)
                {
                    playerData.SwapItemOther();
                    inventoryUI.ChangeItemOtherPos();
                }
                break;
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("스타트");
        switch (type)
        {
            case ItemData.Type.Equipment:
                playerData.changeNum[0] = num;

                if (playerData.equipmentItemId[playerData.changeNum[0]] != 0 && num != dropBox.lockBoxNum)
                {
                    inventoryUI.dragItemObj.SetActive(true);

                    dragImage = inventoryUI.dragItemObj.GetComponent<Image>();
                    dragImage.sprite = playerData.equipmentItemSprite[num];
                }
                break;

            case ItemData.Type.Consumable:
                playerData.changeCNum[0] = num;

                if (playerData.consumableItemId[playerData.changeCNum[0]] != 0 /*&& num != dropBox.lockBoxNum*/)
                {
                    inventoryUI.dragItemObj.SetActive(true);

                    dragImage = inventoryUI.dragItemObj.GetComponent<Image>();
                    dragImage.sprite = playerData.consumableItemSprite[num];
                }
                break;

            case ItemData.Type.Other:
                playerData.changeONum[0] = num;

                if (playerData.otherItemId[playerData.changeONum[0]] != 0 /*&& num != dropBox.lockBoxNum*/)
                {
                    inventoryUI.dragItemObj.SetActive(true);

                    dragImage = inventoryUI.dragItemObj.GetComponent<Image>();
                    dragImage.sprite = playerData.otherItemSprite[num];
                }
                break;
        }

        //playerData.SwapItem();
        //inventoryUI.startDragId = playerData.equipmentItemId[num];

        //inventoryUI.startDragText = playerData.equipmentItemIntText[num];

        //inventoryUI.startDragNum = num;
    }

    public void OnDrag(PointerEventData eventData)
    {
        switch (type)
        {
            case ItemData.Type.Equipment:
                if (num != dropBox.lockBoxNum)
                {
                    inventoryUI.dragItemObj.transform.position = eventData.position;
                }
                break;

            case ItemData.Type.Consumable:
                inventoryUI.dragItemObj.transform.position = eventData.position;
                break;

            case ItemData.Type.Other:
                inventoryUI.dragItemObj.transform.position = eventData.position;
                break;
        }
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

    public void OnPointerClick(PointerEventData eventData)
    {
        switch (type)
        {
            case ItemData.Type.Equipment:
                playerData.changeNum[0] = num;

                if (playerData.equipmentItemId[playerData.changeNum[0]] != 0 && num != dropBox.lockBoxNum && eventData.button == PointerEventData.InputButton.Right)
                {
                    Debug.Log(":2");
                    dropBox.lockBoxNum = playerData.changeNum[0];
                    playerData.EuqipItem();
                    inventoryUI.EquipItemPos();
                    dropBox.mountingEquipment = true;
                }
                break;

            case ItemData.Type.Consumable:

                break;

            case ItemData.Type.Other:

                break;
        }
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