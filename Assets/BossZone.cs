using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossZone : MonoBehaviour
{
    public Boss boss;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            boss.anim.SetBool("isOn", true);
            //Debug.Log("잠깸");
            boss.bossSleepOff = true;
        }
    }
}