using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill1FlyingSlash : MonoBehaviour
{
    public GameObject hitEffect;
    public GameObject hitEffect2;

    public BoxCollider boxCollider;
    private MonsterNormal monsterNormal;
    private PlayerData playerData;

    private void Awake()
    {
        playerData = GameObject.Find("GameManager").GetComponent<PlayerData>();
    }

    private IEnumerator OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Monster"))
        {
            monsterNormal = other.GetComponent<MonsterNormal>();
            int i;

            for (i = 0; i < 30; i++)
            {
                Instantiate(hitEffect, other.transform.position, other.transform.rotation);
                Instantiate(hitEffect2, other.transform.position, other.transform.rotation);
                monsterNormal.GetHit(playerData.skill1Level * 2, transform);
                //yield return null;
                yield return new WaitForSeconds(0.009f);
            }
            Debug.Log(other + "횟수 : " + i);
        }
    }

    //  if (other.CompareTag("Monster"))
    //{
    //    boxCollider.enabled = false;
    //    monsterNormal = other.GetComponent<MonsterNormal>();
}