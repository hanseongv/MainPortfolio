using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    private int itemOldCount;
    public GameObject contentsWeapon;
    public GameObject contentsExpendables;
    public GameObject contentsOther;

    public List<GameObject> contentsList;

    public List<GameObject> weaponList;
    public List<GameObject> expendablesList;

    //public List<GameObject> otherList;
    public List<Image> onList;

    public PlayerData playerData;

    private enum CountentsType
    { Weapon, Expendables, Other };

    private CountentsType countentsType;
    private bool contentsWeaponB;
    private bool contentsExpendablesB;
    private bool contentsOtherB;
    private List<string> contentsName = new List<string> { "Weapon", "Expendables", "Other" };
    //private List<string> contentsBName = new List<string> { "contentsWeapon", "contentsExpendables", "contentsOther" };

    private void Start()
    {
        playerData = GameObject.Find("GameManager").GetComponent<PlayerData>();
        //contentsWeapon = GameObject.Find("Book/ContentsWeapon");
        //contentsExpendables = GameObject.Find("Book/ContentsExpendables");
        //contentsOther = GameObject.Find("Book/ContentsOther");

        contentsList.Add(GameObject.Find("Book/ContentsWeapon"));
        contentsList.Add(GameObject.Find("Book/ContentsExpendables"));
        contentsList.Add(GameObject.Find("Book/ContentsOther"));
        transform.Find("Book/Category/Weapon").GetComponent<Button>().onClick.AddListener(
() => OnContents(CountentsType.Weapon));
        transform.Find("Book/Category/Expendables").GetComponent<Button>().onClick.AddListener(
  () => OnContents(CountentsType.Expendables));
        transform.Find("Book/Category/Other").GetComponent<Button>().onClick.AddListener(
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
        //    contentsList[0].SetActive(contentsWeaponB);
        //contentsExpendables.SetActive(contentsExpendablesB);
        //contentsOther.SetActive(contentsOtherB);
        gameObject.SetActive(false);
    }

    private void OnContents(CountentsType type)
    {
        for (int i = 0; i < contentsList.Count; i++)
        {
            contentsList[i].SetActive(false);
        }

        switch (type)
        {
            case CountentsType.Weapon:
                contentsList[0].SetActive(true);
                //contentsList[0].SetActive(!contentsWeaponB);
                //contentsWeaponB = !contentsWeaponB;
                //contentsExpendablesB = false;
                //contentsOtherB = false;
                break;

            case CountentsType.Expendables:
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
        if (itemOldCount != playerData.itemCount)
        {
            itemOldCount = playerData.itemCount;
        }
    }

    private void OnEnable()
    {
    }
}