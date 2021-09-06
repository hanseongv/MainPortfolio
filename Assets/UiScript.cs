using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiScript : MonoBehaviour
{
    private PlayerData playerData;
    private Text expText;
    private Text levelText;
    private Text nameText;
    private int oldLevel = -1;
    private float oldExp = -1;
    private float oldHp = -1;
    private string oldName = "용사한성";
    private float oldMp = -1;
    public Image expBar;
    public Image hpBar;
    public Image mpBar;

    private void Start()
    {
        playerData = GameObject.Find("GameManager").GetComponent<PlayerData>();
        expText = GameObject.Find("UI/ExpBarImageBg/ExpBarText").GetComponent<Text>();
        expBar = GameObject.Find("UI/ExpBarImageBg/ExpBarImage").GetComponent<Image>();
        hpBar = GameObject.Find("UI/CharacterStatus/Bar/HpBar").GetComponent<Image>();
        mpBar = GameObject.Find("UI/CharacterStatus/Bar/MpBar").GetComponent<Image>();
        levelText = GameObject.Find("UI/CharacterStatus/LevelImage/Level").GetComponent<Text>();
        nameText = GameObject.Find("UI/CharacterStatus/Name").GetComponent<Text>();
    }

    private void Update()
    {
        //이름
        if (oldName != playerData.playerName)
        {
            //Debug.Log("에치피변동ㅇ요");
            oldName = playerData.playerName;
            nameText.text = $"{playerData.playerName}";
        }
        //레벨
        if (oldLevel != playerData.level)
        {
            //Debug.Log("에치피변동ㅇ요");
            oldLevel = playerData.level;
            levelText.text = $"{playerData.level}";
        }
        //HP바
        if (oldHp != playerData.curentHp)
        {
            //Debug.Log("에치피변동ㅇ요");
            oldHp = playerData.curentHp;

            hpBar.fillAmount = (float)playerData.curentHp / (float)playerData.maxHp;
        }
        // MP바
        if (oldMp != playerData.curentMp)
        {
            //Debug.Log("엠핍ㄴ동이요");
            oldMp = playerData.curentMp;

            mpBar.fillAmount = (float)playerData.curentMp / (float)playerData.maxMp;
        }
        //경험치 바
        if (oldExp != playerData.curentExp)
        {
            //Debug.Log($"경험치 변동 {oldExp}=>{playerData.curentExp}");
            oldExp = playerData.curentExp;

            float expPersent = ((float)playerData.curentExp / (float)playerData.maxExp[playerData.level - 1]) * 100;

            expText.text = $"{playerData.curentExp}/{playerData.maxExp[playerData.level - 1]} [{expPersent.ToString("N1")}%]";

            expBar.fillAmount = expPersent * 0.01f;
        }
    }
}