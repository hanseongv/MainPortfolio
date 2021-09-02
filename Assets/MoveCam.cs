using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCam : MonoBehaviour
{
    public Vector3 camVec = new Vector3(0, 10, -4);
    public PlayerData playerData;
    public GameObject target;
    public float camSpeed = 4;

    private void Start()
    {
        playerData = GameObject.Find("GameManager").GetComponent<PlayerData>();
        target = playerData.player;
    }

    // Update is called once per frame
    private void Update()
    {
        transform.position = Vector3.Lerp
            (transform.position, target.transform.position + camVec,

            camSpeed * Time.deltaTime);
    }
}