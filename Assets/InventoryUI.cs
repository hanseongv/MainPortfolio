using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    public List<GameObject> equipmenItemtList;
    public List<Image> equipmenItemImageList;
    public List<Text> equipmenItemTextList;
    public List<Image> equipmenItemOnImageList;
    public List<GameObject> consumableItemList;
    public List<GameObject> otherItemList;

    //public List<GameObject> otherList;

    public PlayerData playerData;

    private enum CountentsType
    { Equipment, Consumable, Other };

    public Sprite spriteNull;
    private CountentsType countentsType;
    private bool contentsEquipmentB;
    private bool contentsConsumableB;
    private bool contentsOtherB;
    private List<string> contentsName = new List<string> { "Equipment", "Consumable", "Other" };
    //private List<string> contentsBName = new List<string> { "contentsWeapon", "contentsExpendables", "contentsOther" };

    private void Start()
    {
        inventory = GameObject.Find("InventoryUI");
        playerData = GameObject.Find("GameManager").GetComponent<PlayerData>();
        //contentsWeapon = GameObject.Find("Book/ContentsWeapon");
        //contentsExpendables = GameObject.Find("Book/ContentsExpendables");
        //contentsOther = GameObject.Find("Book/ContentsOther");

        contentsList.Add(GameObject.Find("InventoryUI/Book/ContentsEquipment"));
        contentsList.Add(GameObject.Find("InventoryUI/Book/ContentsConsumable"));
        contentsList.Add(GameObject.Find("InventoryUI/Book/ContentsOther"));
        transform.Find("InventoryUI/Book/Category/Equipment").GetComponent<Button>().onClick.AddListener(
() => OnContents(CountentsType.Equipment));
        transform.Find("InventoryUI/Book/Category/Consumable").GetComponent<Button>().onClick.AddListener(
  () => OnContents(CountentsType.Consumable));
        transform.Find("InventoryUI/Book/Category/Other").GetComponent<Button>().onClick.AddListener(
  () => OnContents(CountentsType.Other));
        //        for (int i = 0; i < contentsList.Count; i++)
        //        {
        //            transform.Find("Book/Category/" + contentsName[i]).GetComponent<Button>().onClick.AddListener(
        //() => OnContents(CountentsType.);
        //        }
        contentsList[0].SetActive(true);
        for (int i = 1; i < contentsList.Count; i++)
        {
            contentsList[i].SetActive(false);
        }

        for (int i = 0; i < equipmenItemtList.Count; i++)
        {
            equipmenItemImageList.Add(GameObject.Find("InventoryUI/Book/ContentsEquipment/Inventory/Viewport/Content5x4/Item (" + i + ")/ItemImage").GetComponent<Image>());
            equipmenItemTextList.Add(GameObject.Find("InventoryUI/Book/ContentsEquipment/Inventory/Viewport/Content5x4/Item (" + i + ")/Value").GetComponent<Text>());
            equipmenItemOnImageList.Add(GameObject.Find("InventoryUI/Book/ContentsEquipment/Inventory/Viewport/Content5x4/Item (" + i + ")/OnImage").GetComponent<Image>());
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

    private void Update()
    {
        if (itemOldECount != playerData.itemECount)
        {
            equipmenItemImageList[itemOldECount].sprite = playerData.itemSprite[itemOldECount];
            itemOldECount = playerData.itemECount;
        }

        if (itemOldCCount != playerData.itemCCount)
        {
            itemOldCCount = playerData.itemCCount;
        }
        if (itemOldOCount != playerData.itemOCount)
        {
            itemOldOCount = playerData.itemOCount;
        }
    }

    private void OnEnable()
    {
    }
}