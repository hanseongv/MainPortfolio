using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopUI : MonoBehaviour
{
    public enum BtnType { Sword, Portion, Yes, No, Yes2, No2, Close, SwapBtn, Yes3, No3/*, SellBtn, Yes3, No3*/ };

    private Item item;
    public PlayerData playerData;

    public GameObject reQuest;

    public List<int> itemId;
    public List<ItemData.ItemType> itemType;
    public List<Sprite> itemSprite;
    public int itemCount;
    public bool sellOn;
    public List<int> itemPriceList;

    public Text swapText;

    //public Text inputCount;
    public InputField inputCount2;

    public int buyItemId;
    public ItemData.ItemType buyItemType;
    public Sprite buyItemSprite;
    public int buyItemCount;
    public int buyItemPrice;
    public GameObject reQuest2;
    public GameObject reQuest3;
    public GameObject explanation;
    public List<Image> sellItemImage;

    public List<Text> sellItemText;
    public Sprite nullImage;

    public GameObject shop;
    public GameObject shopSell;
    public PlayerController playerController;
    public List<SellBtn> sellBtns;

    private void Awake()
    {
        itemId[0] = 7;
        itemType[0] = ItemData.ItemType.Equipment;
        itemPriceList[0] = 300;
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        itemId[1] = 101;
        itemType[1] = ItemData.ItemType.Consumable;
        itemPriceList[1] = 10;
        shop = transform.Find("Shop").gameObject;
        shopSell = transform.Find("ShopSell").gameObject;
        for (int i = 0; i < sellItemImage.Count; i++)
        {
            sellItemImage[i] = transform.Find($"ShopSell/ItemList/1 ({i})/Image/Image").GetComponent<Image>();
            //sellItemImage[i].raycastTarget = false;
            sellItemText[i] = transform.Find($"ShopSell/ItemList/1 ({i})/Image/Image2/Text").GetComponent<Text>();
            sellItemImage[i].sprite = nullImage;
            sellBtns[i] = transform.Find($"ShopSell/ItemList/1 ({i})").GetComponent<SellBtn>();
            //          transform.Find($"ShopSell/ItemList/1 ({i})").GetComponent<Button>().onClick.AddListener(
            //() => OnContents(BtnType.SellBtn));
        }

        //sellItemImage=transform.Find("ShopSell/ItemList")
        transform.Find("SwapBtn").GetComponent<Button>().onClick.AddListener(
   () => OnContents(BtnType.SwapBtn));
        swapText = transform.Find($"SwapBtn/Text").GetComponent<Text>();

        playerData = GameObject.Find("GameManager").GetComponent<PlayerData>();
        explanation = transform.Find("Explanation").gameObject;
        explanation.SetActive(false);
        reQuest = transform.Find("ReQuestions").gameObject;
        reQuest2 = transform.Find("ReQuestions2").gameObject;
        //inputCount = transform.Find("ReQuestions2/GameObject/InputField/Text").GetComponent<Text>();
        inputCount2 = reQuest2.GetComponentInChildren<InputField>();
        reQuest3 = transform.Find("ReQuestions3").gameObject;
        reQuest.SetActive(false);
        reQuest2.SetActive(false);
        reQuest3.SetActive(false);
        transform.Find("Shop/ItemList/1").GetComponent<Button>().onClick.AddListener(
    () => OnContents(BtnType.Sword));
        transform.Find("Close").GetComponent<Button>().onClick.AddListener(
    () => OnContents(BtnType.Close));
        transform.Find("Shop/ItemList/2").GetComponent<Button>().onClick.AddListener(
  () => OnContents(BtnType.Portion));
        transform.Find("ReQuestions/GameObject/Yes").GetComponent<Button>().onClick.AddListener(
 () => OnContents(BtnType.Yes));
        transform.Find("ReQuestions/GameObject/No").GetComponent<Button>().onClick.AddListener(
() => OnContents(BtnType.No));

        transform.Find("ReQuestions2/GameObject/Yes").GetComponent<Button>().onClick.AddListener(
() => OnContents(BtnType.Yes2));
        transform.Find("ReQuestions2/GameObject/No").GetComponent<Button>().onClick.AddListener(
() => OnContents(BtnType.No2));
        transform.Find("ReQuestions3/GameObject/Yes").GetComponent<Button>().onClick.AddListener(
() => OnContents(BtnType.Yes3));
        transform.Find("ReQuestions3/GameObject/No").GetComponent<Button>().onClick.AddListener(
() => OnContents(BtnType.No3));
    }

    private void OnEnable()
    {
        sellOn = false;
        shop.SetActive(true);
        shopSell.SetActive(false);
        //for (int i = 0; i < sellItemImage.Count; i++)
        //{
        //    sellItemImage[i] = transform.Find($"ShopSell/ItemList/1 ({i})/Image/Image").GetComponent<Image>();
        //    //sellItemImage[i].raycastTarget = false;
        //    sellItemText[i] = transform.Find($"ShopSell/ItemList/1 ({i})/Image/Image2/Text").GetComponent<Text>();
        //    sellItemImage[i].sprite = nullImage;
        //    sellItemText[i].text = "0";
        //    //sellBtns[i] = null;
        //}

        //for (int i = 0; i < playerData.otherItemId.Count; i++)
        //{
        //    if (playerData.otherItemId[i] != 0)
        //    {
        //        for (int j = 0; j < sellItemImage.Count; j++)
        //        {
        //            if (sellItemImage[j].sprite == nullImage)
        //            {
        //                sellBtns[j].itemId = playerData.otherItemId[i];
        //                sellBtns[j].itemCount = playerData.otherItemIntText[i];
        //                //sellItemImage[i].raycastTarget = true;
        //                sellItemImage[j].sprite = playerData.otherItemSprite[i];
        //                sellItemText[j].text = $"{playerData.otherItemIntText[i]}";
        //                j = sellItemImage.Count;
        //            }
        //        }
        //    }
        //}
    }

    private void OnContents(BtnType btnType)
    {
        switch (btnType)
        {
            case BtnType.Sword:
                if (playerData.playerCoin >= 300)
                {
                    buyItemId = itemId[0];
                    buyItemSprite = itemSprite[0];
                    buyItemType = itemType[0];
                    buyItemCount = 1;
                    buyItemPrice = itemPriceList[0];
                    reQuest.SetActive(false);
                    reQuest.SetActive(true);
                }
                else
                    explanation.SetActive(true);
                break;

            case BtnType.Portion:
                buyItemId = itemId[1];
                buyItemSprite = itemSprite[1];
                buyItemType = itemType[1];
                //buyItemPrice = itemPriceList[1];
                reQuest2.SetActive(false);
                reQuest2.SetActive(true);
                break;

            case BtnType.Yes:

                playerData.playerCoin -= buyItemPrice;
                playerData.GetItem(buyItemId, buyItemType, buyItemSprite, buyItemCount);
                reQuest.SetActive(false);
                break;

            case BtnType.No:
                reQuest.SetActive(false);
                break;

            case BtnType.Yes2:

                //buyItemCount = int.Parse(inputCount.text);
                buyItemPrice = itemPriceList[1] * int.Parse(inputCount2.text);
                if (playerData.playerCoin >= buyItemPrice)
                {
                    buyItemCount = int.Parse(inputCount2.text);
                    playerData.playerCoin -= buyItemPrice;
                    playerData.GetItem(buyItemId, buyItemType, buyItemSprite, buyItemCount);

                    reQuest2.SetActive(false);
                }
                else
                {
                    explanation.SetActive(true);
                }
                break;

            case BtnType.No2:
                reQuest2.SetActive(false);
                break;

            case BtnType.Close:
                gameObject.SetActive(false);
                playerController.talkToNpc = false;

                break;

            case BtnType.SwapBtn:

                sellOn = !sellOn;
                shop.SetActive(!sellOn);
                shopSell.SetActive(sellOn);
                break;

            case BtnType.Yes3:
                for (int i = 0; i < playerData.sellCount; i++)
                    playerData.playerCoin += playerData.sellID / 5;
                playerData.itemOCount -= playerData.sellCount;
                Debug.Log("클릭");
                playerData.SellItem(playerData.sellID);

                playerData.sellID = 0;
                playerData.sellCount = 0;
                reQuest3.SetActive(false);
                break;

            case BtnType.No3:
                reQuest3.SetActive(false);
                break;
                //case BtnType.SellBtn:

                //    reQuest3.SetActive(true);
                //    break;

                //case BtnType.Yes3:

                //    reQuest3.SetActive(false);
                //    break;

                //case BtnType.No3:
                //    reQuest3.SetActive(false);
                //    break;
        }
    }

    private void Update()
    {
        if (sellOn)
        {
            swapText.text = "구매하기";
        }
        else
            swapText.text = "판매하기";

        for (int i = 0; i < sellItemImage.Count; i++)
        {
            sellItemImage[i] = transform.Find($"ShopSell/ItemList/1 ({i})/Image/Image").GetComponent<Image>();
            //sellItemImage[i].raycastTarget = false;
            sellItemText[i] = transform.Find($"ShopSell/ItemList/1 ({i})/Image/Image2/Text").GetComponent<Text>();
            sellItemImage[i].sprite = nullImage;
            sellItemText[i].text = "0";
            //sellBtns[i] = null;
        }

        for (int i = 0; i < playerData.otherItemId.Count; i++)
        {
            if (playerData.otherItemId[i] != 0)
            {
                for (int j = 0; j < sellItemImage.Count; j++)
                {
                    if (sellItemImage[j].sprite == nullImage)
                    {
                        sellBtns[j].itemId = playerData.otherItemId[i];
                        sellBtns[j].itemCount = playerData.otherItemIntText[i];
                        //sellItemImage[i].raycastTarget = true;
                        sellItemImage[j].sprite = playerData.otherItemSprite[i];
                        sellItemText[j].text = $"{playerData.otherItemIntText[i]}";
                        j = sellItemImage.Count;
                    }
                }
            }
        }
    }
}