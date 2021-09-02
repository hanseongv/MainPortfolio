using UnityEngine;
using System;


#if UNITY_EDITOR
using UnityEditor;

[CustomPropertyDrawer(typeof(SaveString))]
public class SaveStringPropertyDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);

        position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

        var left = position; left.xMax -= 40;
        var right = position; right.xMin = left.xMax + 2;

        var value = property.FindPropertyRelative("value");
        EditorGUI.PropertyField(left, value, GUIContent.none);

        if (GUI.Button(right, "Save"))
        {
            var key = property.FindPropertyRelative("key").stringValue;
            if (string.IsNullOrEmpty(key) == false)
            {
                PlayerPrefs.SetString(key, value.stringValue);
                PlayerPrefs.Save();
            }
        }

        EditorGUI.EndProperty();
    }
}
#endif

[Serializable]
public class SaveString
{
    [SerializeField] string key;

    [SerializeField]
    string value;
    public SaveString(string _key)
    {
        //key = Application.dataPath + GetType() + _key;
        key = GetType() + _key;

        if (PlayerPrefs.HasKey(key))
            value = PlayerPrefs.GetString(key);
        else
            value = "";
    }

    public string Value
    {
        get
        {
            return value;
        }
        set
        {
            if (this.value != value)
            {
                PlayerPrefs.SetString(key, value);
                PlayerPrefs.Save();
            }
            this.value = value;
        }
    }

    public override string ToString()
    {
        return Value;
    }

    public static implicit operator string(SaveString saveString)
    {
        return saveString.value;
    }
}