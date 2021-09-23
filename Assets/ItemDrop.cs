using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemDrop : MonoBehaviour, IDropHandler
{
    public InventoryUI inventoryUI;
    public PlayerData playerData;
    public GameObject dropPrefab;
    public Transform playerPos;
    public DropBox dropBox;

    private void Awake()
    {
        playerPos = GameObject.Find("Player").transform;
        playerData = GameObject.Find("GameManager").GetComponent<PlayerData>();
        inventoryUI = GameObject.Find("UI").GetComponent<InventoryUI>();

        dropBox = GameObject.Find("InventoryUI/EquipUI/WeaponBox/ItemImage").GetComponent<DropBox>();
    }

    private ItemData.ItemType itemType;

    public void OnDrop(PointerEventData eventData)
    {
        itemType = playerData.changeItemType;

        if (playerData.changeNum[0] == dropBox.lockBoxNum /*|| itemType != ItemData.ItemType.Equipment*/)
            return;

        int dropNum = 0;

        switch (itemType)
        {
            case ItemData.ItemType.Equipment:
                dropNum = playerData.equipmentItemId[playerData.changeNum[0]];
                break;

            case ItemData.ItemType.Consumable:
                dropNum = playerData.consumableItemId[playerData.changeCNum[0]];
                break;

            case ItemData.ItemType.Other:
                dropNum = playerData.otherItemId[playerData.changeONum[0]];
                break;
        }
        dropPrefab = Resources.Load<GameObject>($"{dropNum}");
        Instantiate(dropPrefab, playerPos.position, Quaternion.identity);

        playerData.DropItem();
        inventoryUI.DropItem();

        Debug.Log("z");
    }
}