using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SellBtn : MonoBehaviour
{
    public enum BtnType { Yes3, No3 };

    public PlayerData playerData;
    public int itemId;
    public int itemCount;
    public GameObject reQuest;
    public InventoryUI inventoryUI;

    // Start is called before the first frame update
    private void Start()
    {
        playerData = GameObject.Find("GameManager").GetComponent<PlayerData>();
        inventoryUI = GameObject.Find("UI").GetComponent<InventoryUI>();
        //    reQuest.transform.Find("GameObject/Yes").GetComponent<Button>().onClick.AddListener(
        //() => OnContents(BtnType.Yes3));
        //    reQuest.transform.Find("GameObject/No").GetComponent<Button>().onClick.AddListener(
        //() => OnContents(BtnType.No3));
    }

    //private void OnContents(BtnType btnType)
    //{
    //    switch (btnType)
    //    {
    //        case BtnType.Yes3:
    //            for (int i = 0; i < itemCount; i++)
    //                playerData.playerCoin += itemId / 5;
    //            playerData.itemOCount -= itemCount;
    //            Debug.Log("클릭");
    //            playerData.SellItem(itemId);

    //            itemId = 0;
    //            itemCount = 0;
    //            reQuest.SetActive(false);
    //            break;

    //        case BtnType.No3:
    //            reQuest.SetActive(false);
    //            break;
    //    }
    //}

    public void OnClickBtn()
    {
        if (itemId != 0)
        {
            playerData.sellID = itemId;
            playerData.sellCount = itemCount;
            reQuest.SetActive(true);
        }
    }

    // Update is called once per frame
    private void Update()
    {
    }
}