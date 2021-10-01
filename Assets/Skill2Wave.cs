using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill2Wave : MonoBehaviour
{
    private Transform originVec3;
    private Vector3 addVec3;
    public GameObject Skill2Hit;
    public MonsterNormal monsterNormal;
    public Golem golem;
    public int hitDamage = 20;
    public PlayerData playerData;
    private Boss boss;

    private void OnEnable()
    {
        StartCoroutine(Skill2WaveCo());
    }

    private void Start()
    {
        originVec3 = transform.parent;

        gameObject.SetActive(false);
        playerData = GameObject.Find("GameManager").GetComponent<PlayerData>();
    }

    private IEnumerator Skill2WaveCo()
    {
        Renderer renderer = gameObject.GetComponent<Renderer>();
        Color c = renderer.material.color;
        for (float i = 0; i <= 20; i += 0.1f)
        {
            c.a = (20 - i) * 0.05f;
            renderer.material.color = c;
            this.transform.localScale = new Vector3(i, i * 0.4f, i);

            yield return new WaitForSeconds(0.00001f); //0.01
        }
        this.transform.localScale = new Vector3(0, 0, 0);
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Monster"))
        {
            Skill2Hit = Resources.Load<GameObject>("Skill2Hit");
            Instantiate(Skill2Hit, other.transform.position, Quaternion.identity);
            monsterNormal = other.GetComponent<MonsterNormal>();
            monsterNormal.GetHit(hitDamage + (playerData.playerPhyDamage / 5), other.transform);
        }
        if (other.CompareTag("Golem"))
        {
            Skill2Hit = Resources.Load<GameObject>("Skill2Hit");
            Instantiate(Skill2Hit, other.transform.position, Quaternion.identity);
            golem = other.GetComponent<Golem>();
            golem.GetHit(hitDamage + (playerData.playerPhyDamage / 5), other.transform);
        }
        if (other.CompareTag("Boss"))
        {
            Skill2Hit = Resources.Load<GameObject>("Skill2Hit");
            Instantiate(Skill2Hit, other.transform.position, Quaternion.identity);
            boss = other.GetComponent<Boss>();
            boss.GetHit(hitDamage + (playerData.playerPhyDamage / 5), other.transform);
        }
    }
}