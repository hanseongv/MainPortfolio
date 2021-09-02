using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UISample
{
    public class RadioButtonUISample : MonoBehaviour
    {
        Toggle toggle3;
        Toggle toggle2;
        Toggle toggle1;

        void Awake()
        {
            toggle3 = transform.Find("ToggleGroup/Toggle3").GetComponent<Toggle>();
            toggle2 = transform.Find("ToggleGroup/Toggle2").GetComponent<Toggle>();
            toggle1 = transform.Find("ToggleGroup/Toggle1").GetComponent<Toggle>();

            //toggle1.onValueChanged.AddListener(OnValueChanged1);
            toggle1.onValueChanged.AddListener((bool bChecked) => { OnValueChanged(toggle1, bChecked); });
            toggle2.onValueChanged.AddListener((bool bChecked) => { OnValueChanged(toggle2, bChecked); });
            toggle3.onValueChanged.AddListener((bool bChecked) => { OnValueChanged(toggle3, bChecked); });
        }

        void OnValueChanged(Toggle toggle, bool bChecked)
        {
            if (bChecked)
            {
                string text = toggle.GetComponentInChildren<Text>().text;
                print($"{text} 변경됨:" + bChecked);
            }
        }

        //void OnValueChanged1(bool bChecked)
        //{
        //    print("OnValueChanged1:" + bChecked);
        //}
        //void OnValueChanged2(bool bChecked)
        //{
        //    print("OnValueChanged2:" + bChecked);
        //}
        //void OnValueChanged3(bool bChecked)
        //{
        //    print("OnValueChanged3:" + bChecked);
        //}
    }
}
