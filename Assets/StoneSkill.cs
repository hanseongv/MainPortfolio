using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneSkill : MonoBehaviour
{
    public Golem golem;
    public PlayerController playerController;
    private BoxCollider boxCollider;
    public GameObject destroyStone;

    private void Start()
    {
        golem = GetComponentInParent<Golem>();
        boxCollider = GetComponent<BoxCollider>();
    }

    public float speed = 1;

    // Update is called once per frame
    private void Update()
    {
        float translateMove = speed * Time.deltaTime;

        transform.Translate(Vector3.forward * translateMove);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerController = other.GetComponent<PlayerController>();
            playerController.GetHit(golem.monsterSkillDamage, gameObject.transform);
            Debug.Log("플레이어한테 온트리거엔터 발생");

            Rigidbody rigid = other.GetComponent<Rigidbody>();

            rigid.AddForce((transform.forward + Vector3.up) * golem.knockBackForce, ForceMode.Impulse);

            Destroy(gameObject);
        }
        //if (other.CompareTag("Weapon"))
        //{
        //    Destroy(gameObject);
        //}
    }
}