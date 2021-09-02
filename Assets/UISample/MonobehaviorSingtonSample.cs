using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UISample
{
    /// <summary>/// 
    /// Monobehavior를 상속받으면 기본 함수들이 실행되어서 추가적인 실행비용이 있겠지만 무시할 수 있을 만큼 아주 작기 때문에 단점으로 보기 어렵습니다. 
    /// </summary>
    public class MonobehaviorSingtonSample : SingletonMonoBehaviour<MonobehaviorSingtonSample>
    {
        int myInt;
        public int MyPoint { get => myInt; set { print($"{myInt} -> {value}"); myInt = value; } }

        public List<int> someData;
        internal void SomeMethod()
        {
            print($"SomeMethod 실행, {someData[0]}");
        }
    }


    /// <summary>
    /// 인스펙터에서 확인 불가능하다는 점에서 특별한 이유가 없다면 유니티에서 C#기본 싱글턴 패턴을 쓸 필요가 없음.
    /// </summary>
    public class CSharpSingtonSample
    {
        public static CSharpSingtonSample m_instance;
        public static CSharpSingtonSample Instance
        {
            get
            {
                if (m_instance == null)
                    m_instance = new CSharpSingtonSample();
                return m_instance;
            }
        }
        int myInt;
        public int MyPoint { get => myInt; set { Debug.Log($"{myInt} -> {value}"); myInt = value; } }

        public List<int> someData = new List<int>() { 1, 2, 3 }; // 인스펙터에선 설정불가.
        public CSharpSingtonSample()
        {
            someData.Add(4);
            someData.AddRange(new int[] { 5, 6, 7 });
        }

        ~CSharpSingtonSample()
        {
            m_instance = null;
        }

        internal void SomeMethod()
        {
            Debug.Log($"SomeMethod 실행, {someData[0]}");
        }
    }
}