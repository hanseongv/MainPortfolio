using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    //이름, 아이디, 타입, 스프라이트

    public ItemData.Type type;
    public int id;
    public int count;

    public Sprite sprite;
    private BoxCollider boxCollider;

    //public enum Type { Equipment, Consumable };

    //public string name;

    //public int id;

    //public Type type;
    //public Sprite sprite;
    //private BoxCollider boxCollider;

    //private void Start()
    //{
    //    boxCollider = GetComponent<BoxCollider>();
    //}
}