using System;
using UnityEngine;


//#if UNITY_EDITOR
//using UnityEditor;
//[CustomPropertyDrawer(typeof(SaveInt))]
//public class SaveIntPropertyDrawer : PropertyDrawer
//{
//    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
//    {
//        EditorGUI.BeginProperty(position, label, property);

//        position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

//        var left = position; left.xMax -= 40;
//        var right = position; right.xMin = left.xMax + 2;

//        var value = property.FindPropertyRelative("value");
//        EditorGUI.PropertyField(left, value, GUIContent.none);

//        if (GUI.Button(right, "Save"))
//        {
//            var key = property.FindPropertyRelative("key").stringValue;
//            if (string.IsNullOrEmpty(key) == false)
//            {
//                PlayerPrefs.SetInt(key, value.intValue);
//                PlayerPrefs.Save();
//            }
//        }

//        EditorGUI.EndProperty();
//    }
//}
//#endif

[Serializable]
public class SaveInt 
{
    [SerializeField]
    string key;
    [SerializeField]
    int value;
    public SaveInt(string _key, int defaultValue = 0)
    {
        //key = Application.dataPath + GetType() + _key;
        key = GetType() + _key;

        if (PlayerPrefs.HasKey(key))
            value = PlayerPrefs.GetInt(key);
        else
            value = defaultValue;
    }

    public int Value
    {
        get
        {
            return value;
        }
        set
        {
            if (this.value != value)
            {
                PlayerPrefs.SetInt(key, value);
                PlayerPrefs.Save();
            }
            this.value = value;
        }
    }

    public override string ToString()
    {
        return Value.ToString();
    }

    public static implicit operator int(SaveInt saveInt)
    {
        return saveInt.value;
    }
}