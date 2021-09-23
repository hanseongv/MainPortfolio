using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemData : MonoBehaviour
{
    public enum ItemType { Equipment, Consumable, Other };

    public ItemType type;
    public string name;
    public int id;

    private void ItemList()
    {
    }
}