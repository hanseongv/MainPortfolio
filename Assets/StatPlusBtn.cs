using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatPlusBtn : MonoBehaviour
{
    public enum StatType { Str, Int };

    public StatType type;

    public PlayerData playerData;
    public Image Str;
    public Image Int;

    private void Start()
    {
        playerData = GameObject.Find("GameManager").GetComponent<PlayerData>();
        Str = GameObject.Find("UI/CharacterStats/Stats/StatsUp/Stat/StrPlusBtn/PlusBtnOn").GetComponent<Image>();
        Int = GameObject.Find("UI/CharacterStats/Stats/StatsUp/Stat/IntPlusBtn/PlusBtnOn").GetComponent<Image>();
        oldCount = playerData.statPoints;
        if (playerData.statPoints >= 1)
        {
            Str.enabled = true;
            Int.enabled = true;
        }
        else
        {
            Str.enabled = false;
            Int.enabled = false;
        }
    }

    private int oldCount;

    private void Update()
    {
        if (oldCount != playerData.statPoints)
        {
            oldCount = playerData.statPoints;
            if (playerData.statPoints >= 1)
            {
                Str.enabled = true;
                Int.enabled = true;
            }
            else
            {
                Str.enabled = false;
                Int.enabled = false;
            }
        }
    }

    public void OnClickPlusBtn()
    {
        Debug.Log("플러스버튼클릭온");
        if (playerData.statPoints >= 1)
        {
            if (StatType.Int == type)
                playerData.playerInt++;
            else
                playerData.playerStr++;

            playerData.statPoints--;

            Debug.Log("플러스 버튼 클릭");
        }
    }
}