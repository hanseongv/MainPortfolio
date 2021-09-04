using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour
{
    public float destroyTime = 1;
    //private float time;

    private void Start()
    {
        Invoke("DestroyBox", destroyTime);
        //Debug.Log(time);
    }

    private void DestroyBox()
    {
        Destroy(gameObject);
        //Debug.Log(time);
    }

    private void Update()
    {
        //time += Time.deltaTime;
    }
}