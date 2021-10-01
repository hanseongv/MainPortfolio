using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBullet : MonoBehaviour
{
    public Boss boss;
    public PlayerController playerController;
    private BoxCollider boxCollider;

    private void Start()
    {
        boss = GetComponentInParent<Boss>();
        boxCollider = GetComponent<BoxCollider>();
    }

    private void Update()
    {
    }

    public float force = 100;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerController = other.GetComponent<PlayerController>();
            playerController.GetHit(boss.monsterDamage, gameObject.transform);

            Rigidbody rigid = other.GetComponent<Rigidbody>();

            rigid.AddForce((transform.forward + Vector3.up) * boss.knockBackForce, ForceMode.Impulse);
            boxCollider.enabled = false;
        }
    }
}