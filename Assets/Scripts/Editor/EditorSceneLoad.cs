using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

public class EditorSceneLoad
{
    [MenuItem("Window/1. Title Scene Load")]
    private static void TitleSceneLoad()
    {
        LoadScene("Title");
    }


    [MenuItem("Window/2. Stage1 Scene Load")]
    private static void Stage1SceneLoad()
    {
        LoadScene("Stage1");
    }
    private static void LoadScene(string loadSceneName)
    {
        EditorSceneManager.OpenScene($"Assets/Scenes/{loadSceneName}.unity");
    }
}
