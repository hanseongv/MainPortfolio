using UnityEngine;
using UnityEngine.UI;


public class MoveBackButton : MonoBehaviour
{
    private void Start() // Awake에서 실행되면 씬에 있는 UIStackManager 보다 먼저 실행되어서 UIStackManager인스턴스 생성함.
    {
        Button button = transform.AddOrGetComponent<Button>();
        button.AddListener(this, UIStackManager.Instance.MoveBack);
    }
}