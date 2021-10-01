using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill3 : MonoBehaviour
{
    public PlayerController playerController;
    public GameObject player;
    private bool on;
    public GameObject effect;

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
            if (other.CompareTag("Monster") || other.CompareTag("Golem"))
            {
                //playerController.skill3Pos = other.transform.forward * -1;
                //player.transform.position = new Vector3(other.transform.position.x, player.transform.position.y, other.transform.position.z);
                Instantiate(effect, player.transform.position, Quaternion.identity);
                player.transform.position = other.transform.position;
            }
            gameObject.SetActive(false);
        }
    }
}