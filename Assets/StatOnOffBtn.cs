using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatOnOffBtn : MonoBehaviour
{
    public enum BtnType { Stat, Skill };

    public BtnType btnType;

    public enum OnOffType { On, Off };

    public OnOffType onOfftype;
    public GameObject characterStatObj;

    private void Start()
    {
        switch (btnType)
        {
            case BtnType.Stat:
                characterStatObj = GameObject.Find("UI/CharacterStats");
                break;

            case BtnType.Skill:
                characterStatObj = GameObject.Find("UI/CharacterSkill");
                break;
        }
    }

    public void OnClickBtn()
    {
        if (OnOffType.On == onOfftype)
            characterStatObj.SetActive(true);
        else
            characterStatObj.SetActive(false);
    }
}