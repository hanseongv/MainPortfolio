using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill1FlyingSlash : MonoBehaviour
{
    public GameObject hitEffect;

    private void OnTriggerEnter(Collider other)
    {
        Instantiate(hitEffect, other.transform.position, other.transform.rotation);
    }
}