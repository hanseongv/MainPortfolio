using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public float rate = 0.5f;
    private PlayerData playerData;

    private void Start()
    {
        playerData = GameObject.Find("GameManager").GetComponent<PlayerData>();

        playerData.EquipWeapon(gameObject, rate);
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
    }

    // Update is called once per frame
    private void Update()
    {
    }
}