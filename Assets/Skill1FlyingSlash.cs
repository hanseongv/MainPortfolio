using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill1FlyingSlash : MonoBehaviour
{
    public GameObject hitEffect;
    public GameObject hitEffect2;
    //public GameObject OnEffect;

    private void Start()
    {
    }

    private IEnumerator OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Monster"))
        {
            int i;

            for (i = 0; i < 30; i++)
            {
                Instantiate(hitEffect, other.transform.position, other.transform.rotation);
                Instantiate(hitEffect2, other.transform.position, other.transform.rotation);
                //yield return null;
                yield return new WaitForSeconds(0.009f);
            }
            Debug.Log(other + "횟수 : " + i);
        }
    }
}