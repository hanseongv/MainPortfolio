using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explanation : MonoBehaviour
{
    // Start is called before the first frame update
    private IEnumerator CloseCo;

    private void Start()
    {
    }

    public float time;

    private void OnEnable()
    {
        //Invoke("Close", time);
        CloseCo = Close();
        StopCoroutine(CloseCo);
        StartCoroutine(CloseCo);
    }

    //IEnumerator swapAnimCoroutine;
    //swapAnimCoroutine = SwapTrail();
    //StopCoroutine(swapAnimCoroutine);
    //StartCoroutine(swapAnimCoroutine);
    public IEnumerator Close()
    {
        yield return new WaitForSeconds(time);
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    private void Update()
    {
    }
}