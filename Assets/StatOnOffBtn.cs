using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatOnOffBtn : MonoBehaviour
{
    public enum OnOffType { On, Off };

    public OnOffType type;
    public GameObject characterStatObj;

    private void Start()
    {
        characterStatObj = GameObject.Find("UI/CharacterStats");
    }

    public void OnClickBtn()
    {
        if (OnOffType.On == type)
            characterStatObj.SetActive(true);
        else
            characterStatObj.SetActive(false);
    }
}