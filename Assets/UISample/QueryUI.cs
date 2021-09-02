using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UISample
{
    public class QueryUI : BaseUI<QueryUI>
    {
        Text contentText;
        GameObject baseButton;
        Action<string> callbackFn;

        protected override void OnInit()
        {
            contentText = transform.Find("ContentText").GetComponent<Text>();

            baseButton = transform.Find("ButtonParent/Button").gameObject;
            baseButton.SetActive(false);
        }

        public void Show(string title, Action<string> _callbackFn, params string[] buttonTexts)
        {
            base.Show();
            callbackFn = _callbackFn;
            contentText.text = title;
            DestroyChildObject();

            foreach (var item in buttonTexts)
            {
                var newButtonGo = Instantiate(baseButton, baseButton.transform.parent);
                newButtonGo.SetActive(true);
                ChildObject.Add(newButtonGo);
                newButtonGo.GetComponentInChildren<Text>().text = item;

                Button btn = newButtonGo.GetComponent<Button>();
                btn.AddListener(this, delegate { ButtonClicked(item); });
            }
        }

        void ButtonClicked(string clickedText)
        {
            Close();
            callbackFn(clickedText);
        }
    }
}