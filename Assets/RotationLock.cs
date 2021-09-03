using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationLock : MonoBehaviour
{
    public Vector3 rota;
    public bool local;

    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
        if (local)
            transform.Rotate(rota);//로컬좌표
        else
            transform.rotation = Quaternion.Euler(rota);//월드좌표
    }
}