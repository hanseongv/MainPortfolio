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

    public bool offNpc;

    public void onUI(GameObject player)
    {
        if (offNpc)
            return;
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

    public GameObject helpImage;
    public GameObject clearImage;
    public bool helpB = true;
    public bool clearB = false;

    // Update is called once per frame
    private void Update()
    {
        if (npcType == NpcType.Quest)
        {
            if (!helpB)
            {
                helpImage.SetActive(true);
            }
            else if (helpB)
            {
                helpImage.SetActive(false);
            }
            if (playerData.questBClear)
            {
                helpImage.SetActive(false);
                clearImage.SetActive(true);
            }
            else
            {
                clearImage.SetActive(false);
            }
            //if (clearB)
            //{
            //    clearImage.SetActive(true);
            //}
        }
    }
}