using System;
using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

static public class EditorButtonHelperExtend
{
#if UNITY_EDITOR
    [MenuItem("Util/Delete EditorButtonHelper Script")]
    private static void DeleteEditorButtonHelper()
    {
        foreach (var item in Selection.objects)
        {
            GameObject go = (GameObject)item;
            if (go == null)
                continue;
            var tempScripts = go.GetComponentsInChildren<EditorButtonHelper>();
            if (tempScripts.Length == 0)
                continue;

            foreach (var subItem in tempScripts)
            {
                UnityEngine.Object.DestroyImmediate(subItem);
            }

            EditorUtility.SetDirty(go);
            AssetDatabase.SaveAssets();
        }
    }
#endif

    public static void AddListener(this InputField value, Component component, UnityAction<string> unityAction)
    {
        value.onEndEdit.AddListener(unityAction);

#if UNITY_EDITOR
        EditorButtonHelper editorButtonHelper = value.gameObject.AddComponent<EditorButtonHelper>();
        editorButtonHelper.Init(component, unityAction.Method.ToString());
#endif
    }

    public static void AddListener(this Button value, Component component, UnityAction unityAction)
    {
        value.onClick.AddListener(unityAction);

#if UNITY_EDITOR
        EditorButtonHelper editorButtonHelper = value.gameObject.AddComponent<EditorButtonHelper>();
        editorButtonHelper.Init(component, unityAction.Method.ToString());
#endif
    }

    public static void AddListener(this Toggle value, Component component, UnityAction<bool> unityAction)
    {
        value.onValueChanged.AddListener(unityAction);

#if UNITY_EDITOR
        EditorButtonHelper editorButtonHelper = value.gameObject.AddComponent<EditorButtonHelper>();
        editorButtonHelper.Init(component, unityAction.Method.ToString());
#endif
    }
    public static void AddListener(this Dropdown value, Component component, UnityAction<int> unityAction)
    {
        value.onValueChanged.AddListener(unityAction);

#if UNITY_EDITOR
        EditorButtonHelper editorButtonHelper = value.gameObject.AddComponent<EditorButtonHelper>();
        editorButtonHelper.Init(component, unityAction.Method.ToString());
#endif
    }
}

public class EditorButtonHelper : MonoBehaviour
{
    public Component from;
    public string component;
    public string Method;
#if UNITY_EDITOR
    internal void Init(Component component, string methodName)
    {
        InitValue(component);
        this.Method = methodName;
    }


    private void InitValue(Component component)
    {
        this.from = component;
        var names = component.GetType().ToString().Split('.');
        this.component = names[names.Length - 1];
    }
#endif
}
