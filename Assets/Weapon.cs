using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public float rate = 0.5f;
    public int weaponDamage = 1;
    private PlayerData playerData;
    public BoxCollider boxCollider;
    private MonsterNormal monsterNormal;
    private Golem golem;
    private StoneSkill stoneSkill;

    private void Awake()
    {
        playerData = GameObject.Find("GameManager").GetComponent<PlayerData>();

        playerData.EquipWeapon(gameObject, rate);
        gameObject.SetActive(false);
        boxCollider = GetComponent<BoxCollider>();
        boxCollider.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Monster"))
        {
            boxCollider.enabled = false;
            //if (other.GetComponent<MonsterNormal>())
            //{
            //    Debug.Log("몬스터");
            //}
            //if (other.GetComponent<Golem>())
            //{
            //    Debug.Log("골렘");
            //}
            monsterNormal = other.GetComponent<MonsterNormal>();
            int damage = weaponDamage + playerData.playerPhyDamage;
            monsterNormal.GetHit(weaponDamage + playerData.playerPhyDamage, transform);
            Debug.Log($"무기 공격력 {weaponDamage}, 플레이어 데미지{playerData.playerPhyDamage}");
        }
        if (other.CompareTag("Golem"))
        {
            boxCollider.enabled = false;
            //if (other.GetComponent<MonsterNormal>())
            //{
            //    Debug.Log("몬스터");
            //}
            //if (other.GetComponent<Golem>())
            //{
            //    Debug.Log("골렘");
            //}
            golem = other.GetComponent<Golem>();
            int damage = weaponDamage + playerData.playerPhyDamage;
            golem.GetHit(weaponDamage + playerData.playerPhyDamage, transform);
            Debug.Log($"무기 공격력 {weaponDamage}, 플레이어 데미지{playerData.playerPhyDamage}");
        }
        if (other.CompareTag("Stone"))
        {
            stoneSkill = other.GetComponent<StoneSkill>();
            Instantiate(stoneSkill.destroyStone, other.transform.position, Quaternion.identity);
            Destroy(other.gameObject);
            Debug.Log("바위 부심");
        }
    }

    // Update is called once per frame
    private void Update()
    {
    }
}