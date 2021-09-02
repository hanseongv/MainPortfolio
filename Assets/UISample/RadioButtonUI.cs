using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UISample
{
    public class RadioButtonUI : BaseUI<RadioButtonUI>
    {
        Toggle toggle3;
        Toggle toggle2;
        Toggle toggle1;
        void Start()
        {
            toggle3 = transform.Find("ToggleGroup/Toggle3").GetComponent<Toggle>();
            toggle2 = transform.Find("ToggleGroup/Toggle2").GetComponent<Toggle>();
            toggle1 = transform.Find("ToggleGroup/Toggle1").GetComponent<Toggle>();

            toggle1.AddListener(this, (bool result) => { ToggleChanged(toggle1, result); });
            toggle2.AddListener(this, (bool result) => { ToggleChanged(toggle2, result); });
            toggle3.AddListener(this, (bool result) => { ToggleChanged(toggle3, result); });
        }

        void ToggleChanged(Toggle toggle, bool changed)
        {
            if (changed)
            {
                string toggleText = toggle.GetComponentInChildren<Text>().text;
                print(toggleText + " 좋아해!");
            }
        }
    }
}