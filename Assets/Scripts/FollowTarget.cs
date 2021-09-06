using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    public Transform target;

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 pos = transform.position;
        float y = pos.y;
        pos = target.position;
        pos.y = y;
        transform.position = pos;
    }
}
