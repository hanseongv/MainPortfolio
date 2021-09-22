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

    public void OnDrop(PointerEventData eventData)
    {
        if (playerData.changeNum[0] == dropBox.lockBoxNum)
            return;
        int dropNum = playerData.equipmentItemId[playerData.changeNum[0]];

        dropPrefab = Resources.Load<GameObject>($"{dropNum}");
        Instantiate(dropPrefab, playerPos.position, Quaternion.identity);

        playerData.DropItem();
        inventoryUI.DropItem();

        Debug.Log("z");
    }
}