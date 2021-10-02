using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossBar : MonoBehaviour
{
    public Boss boss;

    //public float hp;
    //public float mp;
    public Image hpBar;

    public Image mpBar;

    private void Start()
    {
        boss = GameObject.Find("Boss").GetComponent<Boss>();
    }

    private void Update()
    {
        //hp = boss.monsterHp / boss.monsterMaxHp;
        //mp = boss.monsterMp / boss.monsterMaxMp;
        hpBar.fillAmount = (float)boss.monsterHp / (float)boss.monsterMaxHp;
        mpBar.fillAmount = (float)boss.monsterMp / (float)boss.monsterMaxMp;
    }
}