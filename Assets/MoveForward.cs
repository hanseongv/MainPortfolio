using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveForward : MonoBehaviour
{
    public float speed = 2;

    // Update is called once per frame
    private void Update()
    {
        transform.Translate(transform.forward * speed * Time.deltaTime);
    }
}