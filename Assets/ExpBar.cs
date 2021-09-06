using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExpBar : MonoBehaviour
{
    //private PlayerData playerData;
    //private Text expText;
    //private float oldExp = -1;
    //private Image expBarImage;

    //private void Start()
    //{
    //    playerData = GameObject.Find("GameManager").GetComponent<PlayerData>();
    //    expText = GameObject.Find("UI/ExpBarImageBg/ExpBarText").GetComponent<Text>();
    //    expBarImage = GameObject.Find("UI/ExpBarImageBg/ExpBarImage").GetComponent<Image>();
    //}

    //// Update is called once per frame
    //private void Update()
    //{
    //    if (oldExp != playerData.curentExp)
    //    {
    //        Debug.Log($"경험치 변동 {oldExp}=>{playerData.curentExp}");
    //        oldExp = playerData.curentExp;

    //        float expPersent = ((float)playerData.curentExp / (float)playerData.maxExp[playerData.level - 1]) * 100;

    //        expText.text = $"{playerData.curentExp}/{playerData.maxExp[playerData.level - 1]} [{expPersent.ToString("N1")}%]";

    //        expBarImage.fillAmount = expPersent * 0.01f;
    //        Debug.Log(expPersent * 0.01f);
    //    }
    //}
}