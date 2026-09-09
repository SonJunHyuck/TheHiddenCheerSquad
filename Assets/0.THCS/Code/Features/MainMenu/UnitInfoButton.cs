using UnityEngine;
using UnityEngine.UI;

// Button -> Observer
[RequireComponent(typeof(Button))]
public class UnitInfoButton : MonoBehaviour
{
    [SerializeField] private string unitKey;
    [SerializeField] private Image unitImage;

    private void OnEnable() 
    {
        GetComponent<Button>().onClick.AddListener(OnClick);
    }

    private void OnDisable()
    {
        GetComponent<Button>().onClick.RemoveAllListeners();
    }

    public void Initialize(string key, Sprite sprite)
    {
        unitKey = key;
        unitImage.sprite = sprite;
        unitImage.SetNativeSize();
    }

    // 버튼 클릭 이벤트
    public void OnClick()
    {
        Observer.Instance.RequestUnitInfoEvent(unitKey);
    }
}