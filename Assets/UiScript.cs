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
    public Image skill2CoolImage;
    public Image skill3CoolImage;
    public Text portionText;
    public GameObject characterStats;
    public Text characterStatsText;
    public Text characterStatText;
    public Text characterRandomPointText;
    public Text characterNameText;

    public GameObject characterSkillUI;
    public GameObject skill1OffImage;
    public Text skill1LockLevel;
    public GameObject skill2OffImage;
    public Text skill2LockLevel;
    public GameObject skill3OffImage;
    public Text skill3LockLevel;
    public Text skillPoint;
    public GameObject skill1PlusBtnImage;
    public GameObject skill2PlusBtnImage;
    public GameObject skill3PlusBtnImage;
    public GameObject skill1LockImage;
    public GameObject skill2LockImage;
    public GameObject skill3LockImage;

    //public Text characterStatText;
    public GameObject characterStatObj;

    //public Text playerCoinText;
    //public int oldPlayerCoin;

    private void SkillUI()
    {
        skillPoint.text = $"{playerData.skillPoints}";

        if (playerData.level >= 2)
            skill1LockLevel.color = Color.white;
        if (playerData.level >= 3)
            skill2LockLevel.color = Color.white;
        if (playerData.level >= 5)
            skill3LockLevel.color = Color.white;

        if (playerData.skillPoints >= 1)
        {
            skill1PlusBtnImage.SetActive(true);
            skill2PlusBtnImage.SetActive(true);
            skill3PlusBtnImage.SetActive(true);
        }
        else
        {
            skill1PlusBtnImage.SetActive(false);
            skill2PlusBtnImage.SetActive(false);
            skill3PlusBtnImage.SetActive(false);
        }
        if (playerData.skill1On)
        {
            skill1PlusBtnImage.SetActive(false);
            skill1OffImage.SetActive(false);
            skill3LockImage.SetActive(false);
        }
        if (playerData.skill2On)
        {
            skill2PlusBtnImage.SetActive(false);
            skill2OffImage.SetActive(false);
            skill2LockImage.SetActive(false);
        }
        if (playerData.skill3On)
        {
            skill3PlusBtnImage.SetActive(false);
            skill3OffImage.SetActive(false);
            skill1LockImage.SetActive(false);
        }
    }

    public GameObject optionUI;
    public GameObject reQuestions;

    private void Start()
    {
        optionUI = GameObject.Find("UI/OptionUI");
        reQuestions = GameObject.Find("UI/ReQuestions");
        skill1LockImage = GameObject.Find("UI/ControlUI/RightControl/Skill1/Image/SkillImage/SkillImageLock");
        skill2LockImage = GameObject.Find("UI/ControlUI/RightControl/Skill2/Image/SkillImage/SkillImageLock");
        skill3LockImage = GameObject.Find("UI/ControlUI/RightControl/Skill3/Image/SkillImage/SkillImageLock");
        characterSkillUI = GameObject.Find("UI/CharacterSkill");
        skillPoint = GameObject.Find("UI/CharacterSkill/SkillPointImage/SkillPoint").GetComponent<Text>();
        skill1OffImage = GameObject.Find("UI/CharacterSkill/SkillList/Skill1/SkillImage/SkillImageLock");
        skill2OffImage = GameObject.Find("UI/CharacterSkill/SkillList/Skill2/SkillImage/SkillImageLock");
        skill3OffImage = GameObject.Find("UI/CharacterSkill/SkillList/Skill3/SkillImage/SkillImageLock");
        skill1LockLevel = GameObject.Find("UI/CharacterSkill/SkillList/Skill1/LockLevel").GetComponent<Text>();
        skill2LockLevel = GameObject.Find("UI/CharacterSkill/SkillList/Skill2/LockLevel").GetComponent<Text>();
        skill3LockLevel = GameObject.Find("UI/CharacterSkill/SkillList/Skill3/LockLevel").GetComponent<Text>();
        skill1PlusBtnImage = GameObject.Find("UI/CharacterSkill/SkillList/Skill1/Skill1Plus/PlusBtnOn");
        skill2PlusBtnImage = GameObject.Find("UI/CharacterSkill/SkillList/Skill2/Skill1Plus/PlusBtnOn");
        skill3PlusBtnImage = GameObject.Find("UI/CharacterSkill/SkillList/Skill3/Skill1Plus/PlusBtnOn");

        playerData = GameObject.Find("GameManager").GetComponent<PlayerData>();
        expText = GameObject.Find("UI/ExpBarImageBg/ExpBarText").GetComponent<Text>();
        expBar = GameObject.Find("UI/ExpBarImageBg/ExpBarImage").GetComponent<Image>();
        hpBar = GameObject.Find("UI/CharacterStatus/Bar/HpBar").GetComponent<Image>();
        mpBar = GameObject.Find("UI/CharacterStatus/Bar/MpBar").GetComponent<Image>();
        hpHideBar = GameObject.Find("UI/CharacterStatus/Bar/HpBarHide").GetComponent<Image>();
        mpHideBar = GameObject.Find("UI/CharacterStatus/Bar/MpBarHide").GetComponent<Image>();
        skill1CoolImage = GameObject.Find("UI/ControlUI/RightControl/Skill1/Image/SkillCool").GetComponent<Image>();
        skill2CoolImage = GameObject.Find("UI/ControlUI/RightControl/Skill2/Image/SkillCool").GetComponent<Image>();
        skill3CoolImage = GameObject.Find("UI/ControlUI/RightControl/Skill3/Image/SkillCool").GetComponent<Image>();
        portionText = GameObject.Find("UI/ControlUI/RightControl/Portion/Text").GetComponent<Text>();
        levelText = GameObject.Find("UI/CharacterStatus/LevelImage/Level").GetComponent<Text>();
        nameText = GameObject.Find("UI/CharacterStatus/Name").GetComponent<Text>();
        explanation = GameObject.Find("UI/Explanation");
        characterStats = GameObject.Find("UI/CharacterStats").GetComponent<GameObject>();
        characterStatsText = GameObject.Find("UI/CharacterStats/Stats/StatsUp/StatText").GetComponent<Text>();
        characterRandomPointText = GameObject.Find("UI/CharacterStats/Stats/StatsDown/SP/RandomPointInt").GetComponent<Text>();
        //playerCoinText = GameObject.Find("UI/InventoryUI/EquipUI/Coin/Text").GetComponent<Text>();
        characterStatText = GameObject.Find("UI/CharacterStats/Stats/StatsUp/StatPointInt").GetComponent<Text>();
        characterNameText = GameObject.Find("UI/CharacterStats/PlayerName/Text").GetComponent<Text>();
        characterNameText.text = oldName;
        explanationTextUI = explanation.GetComponentInChildren<Text>();
        characterSkillUI.SetActive(false);
        characterStatObj = GameObject.Find("UI/CharacterStats");
        optionUI.SetActive(false);
        reQuestions.SetActive(false);
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
        SkillCoolImage();
        UIState();
        SkillUI();
        //Coin();
        //이름
    }

    //private void Coin()
    //{
    //    playerCoinText.text = $"{playerData.playerCoin}";
    //}

    private void UIState()
    {
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

    private void SkillCoolImage()
    {
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

        if (playerData.skill2B)
        {
            //skill1CoolObj.SetActive(true);
            skill2CoolImage.enabled = true;
            skill2CoolImage.fillAmount = playerData.skill2Time / playerData.skill2TimeMax;
        }
        else
        {
            skill2CoolImage.enabled = false;
            //skill1CoolObj.SetActive(false);
        }
        if (playerData.skill3B)
        {
            //skill1CoolObj.SetActive(true);
            skill3CoolImage.enabled = true;
            skill3CoolImage.fillAmount = playerData.skill3Time / playerData.skill3TimeMax;
        }
        else
        {
            skill3CoolImage.enabled = false;
            //skill1CoolObj.SetActive(false);
        }
    }

    private void Stat()
    {
        characterStatsText.text = $"{playerData.level}\n\n{playerData.curentHp}/{playerData.maxHp}\n\n{playerData.curentMp}/{playerData.maxMp}\n\n{playerData.playerStr}\n\n{playerData.playerInt}";
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