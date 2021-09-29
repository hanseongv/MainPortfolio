using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HitDamageScript : MonoBehaviour
{
    public GameObject target;
    public float hitBarTime;
    public bool hitBarTimeOn;
    public Image hitBarImage;

    //public GameObject target;
    private void Awake()
    {
        hitBarImage = GameObject.Find("HpBar/HpBarImage").GetComponent<Image>();
    }

    private void OnEnable()
    {
        hitBarTimeOn = true;
    }

    private void OnDisable()
    {
        hitBarTimeOn = false;
    }

    // Update is called once per frame
    private void Update()
    {
        transform.position = target.transform.position;
        if (hitBarTime < 10 && hitBarTimeOn)
            hitBarTime += Time.deltaTime;

        if (hitBarTime > 10)
            gameObject.SetActive(false);
    }
}