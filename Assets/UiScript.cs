using System;
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
    private float oldMp = 50;
    public Image expBar;
    public Image hpBar;
    public Image mpBar;
    public Image hpHideBar;
    public Image mpHideBar;
    public GameObject explanation;
    public Text explanationTextUI;
    public GameObject skill1CoolObj;
    public Image skill1CoolImage;
    public Text portionText;
    public GameObject characterStats;
    public Text characterStatsText;
    public Text characterStatText;
    public Text characterRandomPointText;
    public Text characterNameText;

    //public Text characterStatText;
    public GameObject characterStatObj;

    private void Start()
    {
        playerData = GameObject.Find("GameManager").GetComponent<PlayerData>();
        expText = GameObject.Find("UI/ExpBarImageBg/ExpBarText").GetComponent<Text>();
        expBar = GameObject.Find("UI/ExpBarImageBg/ExpBarImage").GetComponent<Image>();
        hpBar = GameObject.Find("UI/CharacterStatus/Bar/HpBar").GetComponent<Image>();
        mpBar = GameObject.Find("UI/CharacterStatus/Bar/MpBar").GetComponent<Image>();
        hpHideBar = GameObject.Find("UI/CharacterStatus/Bar/HpBarHide").GetComponent<Image>();
        mpHideBar = GameObject.Find("UI/CharacterStatus/Bar/MpBarHide").GetComponent<Image>();
        skill1CoolImage = GameObject.Find("UI/ControlUI/RightControl/Skill1/Image/SkillCool").GetComponent<Image>();

        portionText = GameObject.Find("UI/ControlUI/RightControl/Portion/Text").GetComponent<Text>();
        levelText = GameObject.Find("UI/CharacterStatus/LevelImage/Level").GetComponent<Text>();
        nameText = GameObject.Find("UI/CharacterStatus/Name").GetComponent<Text>();
        explanation = GameObject.Find("UI/Explanation");
        characterStats = GameObject.Find("UI/CharacterStats").GetComponent<GameObject>();
        characterStatsText = GameObject.Find("UI/CharacterStats/Stats/StatsUp/StatText").GetComponent<Text>();
        characterRandomPointText = GameObject.Find("UI/CharacterStats/Stats/StatsDown/SP/RandomPointInt").GetComponent<Text>();

        characterStatText = GameObject.Find("UI/CharacterStats/Stats/StatsUp/StatPointInt").GetComponent<Text>();
        characterNameText = GameObject.Find("UI/CharacterStats/PlayerName/Text").GetComponent<Text>();
        characterNameText.text = oldName;
        explanationTextUI = explanation.GetComponentInChildren<Text>();

        characterStatObj = GameObject.Find("UI/CharacterStats");
        characterStatObj.SetActive(false);
        explanation.SetActive(false);
    }

    public IEnumerator HpCo()
    {
        float a = (float)oldHp / (float)playerData.maxHp;
        float b = (float)playerData.curentHp / (float)playerData.maxHp;
        oldHp = playerData.curentHp;

        for (; a >= b; a -= 0.01f)
        {
            yield return new WaitForSeconds(0.05f);
            hpHideBar.fillAmount = a;
        }
        hpHideBar.fillAmount = (float)playerData.curentHp / (float)playerData.maxHp;
    }

    private IEnumerator MpCo()
    {
        float a = (float)oldMp / (float)playerData.maxMp;
        float b = (float)playerData.curentMp / (float)playerData.maxMp;
        oldMp = playerData.curentMp;

        for (; a >= b; a -= 0.01f)
        {
            yield return new WaitForSeconds(0.05f);
            mpHideBar.fillAmount = a;
        }
        mpHideBar.fillAmount = (float)playerData.curentMp / (float)playerData.maxMp;
    }

    private void Update()
    {
        Stat();
        if (playerData.skill1B)
        {
            //skill1CoolObj.SetActive(true);
            skill1CoolImage.enabled = true;
            skill1CoolImage.fillAmount = playerData.skill1Time / playerData.skill1TimeMax;
        }
        else
        {
            skill1CoolImage.enabled = false;
            //skill1CoolObj.SetActive(false);
        }

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

            hpBar.fillAmount = (float)playerData.curentHp / (float)playerData.maxHp;
            StartCoroutine(HpCo());
        }
        // MP바
        if (oldMp != playerData.curentMp)
        {
            //Debug.Log("엠핍ㄴ동이요");
            mpBar.fillAmount = (float)playerData.curentMp / (float)playerData.maxMp; //mp바는 선 줄어들기
            StartCoroutine(MpCo());
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

    private void Stat()
    {
        characterStatsText.text = $"{playerData.level}\n\n{playerData.curentHp}/{playerData.curentMaxHp}\n\n{playerData.curentMp}/{playerData.curentMaxMp}\n\n{playerData.playerStr}\n\n{playerData.playerInt}";
        characterRandomPointText.text = "" + playerData.randomPoints;
        characterStatText.text = "" + playerData.statPoints;
    }

    //public Text explanationText;

    public void ExplanationUI()
    {
        explanation.SetActive(false);
        explanation.SetActive(true);
        //explanationTextUI ;
    }
}