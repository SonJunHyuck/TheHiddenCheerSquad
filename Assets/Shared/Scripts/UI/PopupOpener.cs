using UnityEngine;
using UnityEngine.UI;

public class PopupOpener : MonoBehaviour
{
    [SerializeField] private GameObject popup; // 제어할 팝업
    [SerializeField] private GameObject closer;

    private void Awake()
    {
        if (popup == null)
        {
            Debug.LogError("Popup GameObject is not assigned!", this);
            return;
        }
    }

    private void Start()
    {
        if (!TryGetComponent<Button>(out var openButton))
        {
            openButton = gameObject.AddComponent<Button>();
        }
        openButton.onClick.AddListener(() => popup.SetActive(true));


        if (!closer.TryGetComponent<Button>(out var closeButton))
        {
            closeButton = gameObject.AddComponent<Button>();
        }
        closeButton.onClick.AddListener(() => popup.SetActive(false));
    }
}