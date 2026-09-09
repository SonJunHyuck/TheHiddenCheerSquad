using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class StagePageButton : MonoBehaviour
{
    [SerializeField]private int increase;

    private void OnEnable() 
    {
        GetComponent<Button>().onClick.AddListener(OnClick);
    }

    private void OnDisable()
    {
        GetComponent<Button>().onClick.RemoveAllListeners();
    }

    // 버튼 클릭 이벤트
    public void OnClick()
    {
        Observer.Instance.RequestSwitchPage(increase);
    }
}