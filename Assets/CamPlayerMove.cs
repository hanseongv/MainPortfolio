using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamPlayerMove : MonoBehaviour
{
    public float hAxis2;
    public float vAxis2;

    // Update is called once per frame
    private void Update()
    {
        hAxis2 = Input.GetAxisRaw("Horizontal");
        vAxis2 = Input.GetAxisRaw("Vertical");
    }
}