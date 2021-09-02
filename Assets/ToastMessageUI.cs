using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToastMessageUI : MonoBehaviour
{
    public Text text;

    [SerializeField]
    CanvasGroup canvasGroup;

    Animator animator;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    static ToastMessageUI lastCalledUI;
    public IEnumerator ShowToastCo(string text,
        float duration, Action closeCB, bool selfDestroy = false)
    {
        lastCalledUI = this;
        yield return ShowToastUICo(text, duration);

        if(lastCalledUI == this)
            closeCB?.Invoke();

        if(selfDestroy)
            Destroy(gameObject);
    }
    IEnumerator ShowToastUICo(string text, float duration)
    {
        animator.Play("ToastMessageUI", -1, 0);
        this.text.text = text;
        this.text.enabled = true;

        //Fade in
        yield return fadeInAndOut(canvasGroup, true, 0.5f);


        yield return new WaitForSeconds(duration);

        //Fade out
        yield return fadeInAndOut(canvasGroup, false, 0.5f);

        this.text.enabled = false;
    }

    IEnumerator fadeInAndOut(CanvasGroup canvasGroup, bool fadeIn, float duration)
    {
        float a, b;
        if (fadeIn)
        {
            a = 0f;
            b = 1f;
        }
        else
        {
            a = 1f;
            b = 0f;
        }

        float counter = 0f;

        while (counter < duration)
        {
            counter += Time.deltaTime;
            float alpha = Mathf.Lerp(a, b, counter / duration);

            canvasGroup.alpha = alpha;
            yield return null;
        }
    }
}
