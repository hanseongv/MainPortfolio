using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillPlus : MonoBehaviour
{
    public enum SkillNum { 일, 이, 삼 };

    public SkillNum num;

    public PlayerData playerData;
    //public InventoryUI inventoryUI;

    private void Awake()
    {
        //inventoryUI = GameObject.Find("UI/InventoryUI").GetComponent<InventoryUI>();
        playerData = GameObject.Find("GameManager").GetComponent<PlayerData>();
    }

    public void OnClickPlusBtn()
    {
        if (playerData.skillPoints >= 1)
        {
            if (SkillNum.일 == num && playerData.level > 1)

            {
                playerData.skill1On = true;
                playerData.skillPoints--;
            }
            else if (SkillNum.이 == num && playerData.level > 2)
            {
                playerData.skill2On = true;
                playerData.skillPoints--;
            }
            else if (SkillNum.삼 == num && playerData.level > 4)
            {
                playerData.skill3On = true;
                playerData.skillPoints--;
            }
        }
    }
}