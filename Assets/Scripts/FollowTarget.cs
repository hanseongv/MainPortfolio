using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    public Transform target;

    public enum Type { Null, player };

    public Type type;

    private void Start()
    {
        if (type == Type.player)
        {
            target = GameObject.Find("Player").GetComponent<Transform>();
        }
    }

    private void LateUpdate()
    {
        Vector3 pos = transform.position;
        float y = pos.y;
        pos = target.position;
        pos.y = y;
        transform.position = pos;
    }
}