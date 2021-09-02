using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ToastMessage : SingletonMonoBehaviour<ToastMessage>
{
    public override int SortOrder => 1;
    public override string HierarchyPath => $"Canvas1/{nameof(ToastMessage)}";
    [SerializeField]
    Text text;


    [System.Serializable]
    public class ParamMessage
    {
        public string text;
        public float duration;
        public float minimumVisibleTime;

        public ParamMessage(string text, float duration, float minimumVisibleTime)
        {
            this.text = text;
            this.duration = duration;
            this.minimumVisibleTime = minimumVisibleTime;
        }
    }
    public List<ParamMessage> stackMessage = new List<ParamMessage>();
    public float forceVisibleEndTime = 0;
    public void ShowToast(string text, float duration = 2, float minimumVisibleTime = 0, bool newInstance = false)
    {
        base.Show(false); // 창이 닫힐때 강제로 UI를 보이게 하면 무한 루프 발생한다.

        if (CacheGameObject.activeInHierarchy == false)
        {
            Debug.Log($"{text} 메시지는 표시안함, 토스트 UI를 활성화 시키지 못했음, 게임 끌때 발생함(정상 과정)");
            return;
        }

        if (newInstance)
        {
            if (showToastCoHandle != null)
                StopCoroutine(showToastCoHandle);
            CreateToastMessageUI(text, duration);
        }
        else
        {
            if (forceVisibleEndTime > Time.time)
            {
                stackMessage.Add(new ParamMessage(text, duration, minimumVisibleTime));

                if (waitAllowNextMessageTimeCoHandle == null)
                    waitAllowNextMessageTimeCoHandle = StartCoroutine(WaitAllowNextMessageTimeCo());
                return;
            }

            if (minimumVisibleTime > 0)
                forceVisibleEndTime = Time.time + minimumVisibleTime;

            // 기존에 활성화 중인 메시지가 있다면 지금 코루틴이 끝난 다음에 연속해서 보여주자.
            if (showToastCoHandle != null)
                StopCoroutine(showToastCoHandle);

            ui.gameObject.SetActive(true);
            ui.transform.SetAsLastSibling();
            showToastCoHandle = StartCoroutine(ui.ShowToastCo(text, duration, Close));
        }
    }


    public ToastMessageUI ui;
    private void CreateToastMessageUI(string text, float duration)
    {
        ui.gameObject.SetActive(false);
        var newUI = Instantiate(ui, transform);
        newUI.gameObject.SetActive(true);
        StartCoroutine(newUI.ShowToastCo(text, duration, Close, true));
    }

    private IEnumerator WaitAllowNextMessageTimeCo()
    {
        while (stackMessage.Count > 0)
        {
            while (forceVisibleEndTime > Time.time)
                yield return new WaitForSeconds(forceVisibleEndTime - Time.time);

            var nextMessage = stackMessage[0];
            stackMessage.RemoveAt(0);
            ShowToast(nextMessage.text, nextMessage.duration, nextMessage.minimumVisibleTime);
        }

        waitAllowNextMessageTimeCoHandle = null;
    }

    Coroutine showToastCoHandle;
    Coroutine waitAllowNextMessageTimeCoHandle;

}
