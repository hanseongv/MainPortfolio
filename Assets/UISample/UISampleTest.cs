using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UISample
{
    public class UISampleTest: MonoBehaviour
    {
        public int callCount = 0;
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                ToastMessage.Instance.ShowToast("일반 메시지 : 처음부터 시작");
            }

            if (Input.GetKeyDown(KeyCode.T))
            {
                ToastMessage.Instance.ShowToast($"{++callCount}번째 메시지, 최소 보이는 시간 1초", 3, 1);
            }

            if (Input.GetKeyDown(KeyCode.Y))
            {
                ToastMessage.Instance.ShowToast($"{++callCount}번째 메시지, 매번 새로 생성", newInstance:true);
            }

            if (Input.GetKeyDown(KeyCode.Q))
            {
                QueryUI.Instance.Show("버튼 1개 내용", (string result) =>
                {
                    print(result + "를 눌렀다");
                }, "확인");
            }
            if (Input.GetKeyDown(KeyCode.W))
            {
                QueryUI.Instance.Show("버튼 2개 내용", (string result) =>
                {
                    print(result + "를 눌렀다");
                }, "확인", "취소");
            }
            if (Input.GetKeyDown(KeyCode.E))
            {
                QueryUI.Instance.Show("버튼 3개 내용", (string result) =>
                {
                    print(result + "를 눌렀다");
                }, "확인", "취소", "3번째 버튼");
            }
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                ChatUISample.Instance.Show();
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                ChatUISample.Instance.Close();
            }

            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                QuestUISample.Instance.Show();
            }
            if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                QuestUISample.Instance.Close();
            }
            if (Input.GetKeyDown(KeyCode.Alpha5))
            {
                HistoryUI.ShowPreviousMenu();
            }


            if (Input.GetKeyDown(KeyCode.Alpha6))
            {
                MonobehaviorSingtonSample.Instance.MyPoint++;
            }
            if (Input.GetKeyDown(KeyCode.Alpha7))
            {
                MonobehaviorSingtonSample.Instance.SomeMethod();
            }


            if (Input.GetKeyDown(KeyCode.Alpha8))
            {
                CSharpSingtonSample.Instance.MyPoint++;
            }
            if (Input.GetKeyDown(KeyCode.Alpha9))
            {
                CSharpSingtonSample.Instance.SomeMethod();
            }


            if (Input.GetKeyDown(KeyCode.Alpha9))
            {
                CSharpSingtonSample.Instance.SomeMethod();
            }

            if (Input.GetKeyDown(KeyCode.Tab))
            {
                QuestUISample.Instance.VisibleToggle();
            }
        }
    }
}