using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DropDownUI : BaseUI<DropDownUI>
{
    Dropdown dropdown;

    protected override void OnInit()
    {
        dropdown = transform.Find("Dropdown").GetComponent<Dropdown>();
        dropdown.AddListener(this, Selected);
        lastSelectedIndex = new SaveInt("lastSelectedIndex");
    }

    SaveInt lastSelectedIndex;
    protected override void OnShow()
    {
        dropdown.value = lastSelectedIndex.Value;
    }

    void Selected(int selectedIndex)
    {
        string selectedText = dropdown.options[selectedIndex].text;
        print($"선택된 인덱스 : {selectedIndex} , {selectedText}");

        lastSelectedIndex.Value = selectedIndex;
    }
}
