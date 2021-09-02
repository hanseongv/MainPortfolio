using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SupportBackButton : MonoBehaviour, IHistoryUI
{
    protected void OnEnable()
    {
        UIStackManager.PushUiStack(transform);
    }

    protected void OnDisable()
    {
        UIStackManager.PopUiStack(gameObject.GetInstanceID());

        HistoryUI.AddHistory(this);
    }

    public void ShowRestoreUI()
    {
        gameObject.SetActive(true);
        transform.SetAsLastSibling();
    }
}
