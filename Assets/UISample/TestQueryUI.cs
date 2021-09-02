using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UISample
{
    public class TestQueryUI : MonoBehaviour
    {

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                QueryUISample.Instace.Show("내용", PrintResult, "확인");
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                QueryUISample.Instace.Show("내용", PrintResult, "확인", "취소");
            }
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                QueryUISample.Instace.Show("내용", PrintResult, "확인", "취소", "다른버튼");
            }
        }

        void PrintResult(string clickedText)
        {
            print(clickedText);
        }
    }
}
