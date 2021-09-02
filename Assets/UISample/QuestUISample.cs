using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UISample
{
    public class QuestUISample : BaseUI<QuestUISample>
    {
        new private void OnEnable()
        {
            base.OnEnable();
            ToastMessage.Instance.ShowToast("퀘스트 UI 열림");
        }

        new private void OnDisable()
        {
            base.OnDisable();
            if (ToastMessage.ApplicationQuit)
                return;
            ToastMessage.Instance.ShowToast("퀘스트 UI 닫힘");
        }

        internal void VisibleToggle()
        {
            gameObject.SetActive(!gameObject.activeSelf);
        }
    }
}