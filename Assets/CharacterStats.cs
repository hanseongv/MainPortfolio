using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    private PlayerController playerController;

    private void Awake()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    private void OnEnable()
    {
        playerController.characterStatsUIB = true;
    }

    private void OnDisable()
    {
        playerController.characterStatsUIB = false;
    }

    // Update is called once per frame
    private void Update()
    {
    }
}