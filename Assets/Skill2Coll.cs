using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill2Coll : MonoBehaviour
{
    //public Boss boss;
    public PlayerController playerController;

    private BoxCollider boxCollider;

    private void Start()
    {
        //boss = GetComponentInParent<Boss>();
        boxCollider = GetComponent<BoxCollider>();
    }

    private void Update()
    {
    }

    public int damage = 300;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("스킬2콜");
            playerController = other.GetComponent<PlayerController>();
            playerController.GetHit(damage, gameObject.transform);

            Rigidbody rigid = other.GetComponent<Rigidbody>();
            playerController.GoStun(4);
            rigid.AddForce((transform.forward + Vector3.up) * 2, ForceMode.Impulse);
            Destroy(gameObject);
        }
    }
}