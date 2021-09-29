using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinUI : MonoBehaviour
{
    public PlayerData playerData;
    public int oldPlayerCoin;
    public Text playerCoinUI;

    private void Awake()
    {
        playerData = GameObject.Find("GameManager").GetComponent<PlayerData>();
        playerCoinUI = GetComponentInChildren<Text>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (oldPlayerCoin != playerData.playerCoin)
        {
            oldPlayerCoin = playerData.playerCoin;
            playerCoinUI.text = $"{playerData.playerCoin}";
        }
    }
}