using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemData : MonoBehaviour
{
    public enum ItemType { Equipment, Consumable, Other, Coin };

    public ItemType type;
    public new string name;
    public int id;

    private void ItemList()
    {
    }
}