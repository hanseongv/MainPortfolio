using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSkill2Bullet : MonoBehaviour
{
    public int fireDamage = 300;

    public Transform target;
    public PlayerController playerController;
    public Boss boss;

    private void Start()
    {
        boss = GameObject.Find("Boss").GetComponent<Boss>();

        target = GameObject.Find("Player").GetComponent<Transform>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerController = other.GetComponent<PlayerController>();
            playerController.GetHit(fireDamage, gameObject.transform);
            playerController.GoStun(4);
            Rigidbody rigid = other.GetComponent<Rigidbody>();
            rigid.AddForce((transform.forward + Vector3.up) * boss.knockBackForce, ForceMode.Impulse);
            Destroy(gameObject);
        }
    }
}