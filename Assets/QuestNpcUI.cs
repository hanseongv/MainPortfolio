using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestNpcUI : MonoBehaviour
{
    public GameObject talkUI;
    public Text talkText;
    public PlayerData playerData;
    public List<string> talkString;
    public int talkInt;
    public GameObject questions;
    public GameObject cam;
    public Npc npc;
    public PlayerController playerController;

    public enum BtnType { Next, Yes, No };

    private void Awake()
    {
        cam = transform.Find("QuestNpcCM").gameObject;
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        npc = gameObject.GetComponentInParent<Npc>();
        playerData = GameObject.Find("GameManager").GetComponent<PlayerData>();
        talkText = transform.Find($"NpcTalk/Text").GetComponent<Text>();
        questions = transform.Find("Questions").gameObject;
        talkUI = transform.Find("NpcTalk").gameObject;
        transform.Find("NpcTalk/Button").GetComponent<Button>().onClick.AddListener(
() => OnContents(BtnType.Next));
        transform.Find("Questions/GameObject/Yes").GetComponent<Button>().onClick.AddListener(
() => OnContents(BtnType.Yes));
        transform.Find("Questions/GameObject/No").GetComponent<Button>().onClick.AddListener(
() => OnContents(BtnType.No));

        talkString.Add("이보게 자네 내 부탁 좀 들어주게나.");
        talkString.Add("이 길을 쭉 가면 외눈 슬라임이 있을 걸세");
        talkString.Add("5마리만 잡고 오게나");
        talkString.Add("왕으로써 명하노라");

        gameObject.SetActive(false);
    }

    private void OnContents(BtnType btnType)
    {
        switch (btnType)
        {
            case BtnType.Next:
                npc.animator.Play("Talk");
                if (talkInt >= talkString.Count - 1)
                {
                    questions.SetActive(false);
                    questions.SetActive(true);
                }
                else
                    talkInt++;
                break;

            case BtnType.Yes:
                playerController.talkToNpc = false;
                playerData.questB = true;
                questions.SetActive(false);
                gameObject.SetActive(false);

                break;

            case BtnType.No:
                playerController.talkToNpc = false;
                questions.SetActive(false);
                gameObject.SetActive(false);
                break;
        }
    }

    private void OnEnable()
    {
        talkInt = 0;
        questions.SetActive(false);
        talkUI.SetActive(false);
        cam.SetActive(false);
        if (playerData.questB)
        {
            npc.animator.Play("Talk");
        }
        else
        {
            talkUI.SetActive(true);
            cam.SetActive(true);
        }
    }

    // Update is called once per frame
    private void Update()
    {
        talkText.text = talkString[talkInt];
    }
}