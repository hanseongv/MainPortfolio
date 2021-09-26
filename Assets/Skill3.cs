using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill3 : MonoBehaviour
{
    public PlayerController playerController;
    public GameObject player;
    private bool on;

    private void Start()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        player = GameObject.Find("Player");
        gameObject.SetActive(false);
        on = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (on)
        {
            if (other.CompareTag("Monster"))
            {
                //playerController.skill3Pos = other.transform.forward * -1;
                player.transform.position = other.transform.position;
            }
            gameObject.SetActive(false);
        }
    }
}