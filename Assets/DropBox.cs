using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DropBox : MonoBehaviour, IDropHandler, IPointerClickHandler
{
    public int num;
    public ItemData.Type type;
    public PlayerData playerData;
    public GameObject dragObj;
    public Image dragImage;
    public Image startDragImage;
    public InventoryUI inventoryUI;
    public Slot slot;
    public int lockBoxNum = -1;
    public bool mountingEquipment;

    private void Awake()
    {
        //dragObj = GameObject.Find("UI/InventoryUI/DragItemImage");
        //dragImage = dragObj.GetComponent<Image>();
        playerData = GameObject.Find("GameManager").GetComponent<PlayerData>();
        inventoryUI = GameObject.Find("UI").GetComponent<InventoryUI>();
    }

    public void OnDrop(PointerEventData eventData)
    {
        //if (hasEquip)
        //    return;
        Debug.Log("Drop");
        //PlayerController playerController;
        //playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        //if (playerController.isFireReady)
        //{
        //playerData.changeNum[1] = num;

        //if (playerData.equipmentItemId[playerData.changeNum[0]] != 0)
        //{
        //playerData.equipWeapon.SetActive(false);
        lockBoxNum = playerData.changeNum[0];
        playerData.EuqipItem();
        inventoryUI.EquipItemPos();
        mountingEquipment = true;
        //}
        //hasEquip = true;
        //}
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            if (mountingEquipment)
            {
                //Output to console the clicked GameObject's name and the following message. You can replace this with your own actions for when clicking the GameObject.
                Debug.Log("우측 클릭");
                lockBoxNum = -1;
                playerData.UnEquip();
                inventoryUI.UnEquipItemPos();
            }
        }
    }

    //public void OnBeginDrag(PointerEventData eventData)
    //{
    //    if (!hasEquip)
    //        return;
    //    Debug.Log("스타트");

    //    //playerData.changeNum[0] = num;
    //    //if (playerData.equipmentItemId[playerData.changeNum[0]] != 0)
    //    //{
    //    inventoryUI.dragItemObj.SetActive(true);

    //    dragImage = inventoryUI.dragItemObj.GetComponent<Image>();
    //    dragImage.sprite = playerData.hasEquipmentItemSprite;
    //    //}

    //    //playerData.SwapItem();
    //    //inventoryUI.startDragId = playerData.equipmentItemId[num];

    //    //inventoryUI.startDragText = playerData.equipmentItemIntText[num];

    //    //inventoryUI.startDragNum = num;
    //}

    //public void OnDrag(PointerEventData eventData)
    //{
    //    if (!hasEquip)
    //        return;
    //    inventoryUI.dragItemObj.transform.position = eventData.position;
    //}

    //public void OnEndDrag(PointerEventData eventData)
    //{
    //    Debug.Log("끝");
    //    inventoryUI.dragItemObj.SetActive(false);
    //}

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