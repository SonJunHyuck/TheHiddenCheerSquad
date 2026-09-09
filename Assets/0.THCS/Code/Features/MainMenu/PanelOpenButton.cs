using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class PanelOpenButton : MonoBehaviour
{
    [SerializeField] private PanelController.PanelType targetPanelName; // 이동할 패널 이름

    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(OnClick);
    }

    private void OnClick()
    {
        Observer.Instance.RequestPanelSwitch(targetPanelName);
    }
}