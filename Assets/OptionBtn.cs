using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionBtn : MonoBehaviour
{
    public GameObject optionUI;
    public GameObject reQuestions;

    public enum BtnType { On, Countinue, Exit, Yes, No }

    public BtnType type;

    // Start is called before the first frame update
    private void Start()
    {
        optionUI = GameObject.Find("UI/OptionUI");
        reQuestions = GameObject.Find("UI/ReQuestions");
        //optionUI.SetActive(false);
        //reQuestions.SetActive(false);
    }

    public void OnClickPlusBtn()
    {
        switch (type)
        {
            case BtnType.On:
                optionUI.SetActive(true);
                Time.timeScale = 0;
                break;

            case BtnType.Countinue:
                optionUI.SetActive(false);
                Time.timeScale = 1;
                break;

            case BtnType.Exit:
                reQuestions.SetActive(true);

                break;

            case BtnType.Yes:
#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit(); // 어플리케이션 종료
#endif
                break;

            case BtnType.No:
                reQuestions.SetActive(false);

                break;
        }
    }
}