using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Npc : MonoBehaviour
{
    public enum NpcType { Shop, Quest };

    public NpcType npcType;

    public GameObject npcUI;
    public Transform playerPos;
    public Animator animator;
    public PlayerData playerData;
    public PlayerController playerController;

    private void Start()
    {
        //if (npcType == NpcType.Shop)
        //{
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        playerData = GameObject.Find("GameManager").GetComponent<PlayerData>();
        npcUI = transform.GetChild(0).gameObject;
        npcUI.SetActive(false);
        //}
        animator = GetComponentInChildren<Animator>();
        playerPos = transform.GetChild(1).transform;
    }

    public void onUI(GameObject player)
    {
        if (npcType == NpcType.Quest && playerData.questB)
        {
            Debug.Log("온유0");
            playerController.talkToNpc = false;
            animator.Play("Talk");
        }
        else
        {
            playerController.talkToNpc = true;
            player.transform.position = playerPos.position;
            player.transform.rotation = playerPos.rotation;
            npcUI.SetActive(true);
        }
    }

    // Update is called once per frame
    private void Update()
    {
    }
}