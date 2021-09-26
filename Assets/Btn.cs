using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Btn : MonoBehaviour
{
    private Animator animator;
    public PlayerData playerData;

    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
        playerData = GameObject.Find("GameManager").GetComponent<PlayerData>();
    }

    public void OnClickBtn()
    {
        if (playerData.randomPoints >= 1)
        {
            animator.Play("Btn");
            playerData.randomPoints--;
            playerData.statPoints += Random.Range(1, 7);
        }
    }
}