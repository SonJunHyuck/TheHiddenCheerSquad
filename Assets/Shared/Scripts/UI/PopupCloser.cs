using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class PopupCloser : MonoBehaviour
{
    [SerializeField] private GameObject popup;

    void Start()
    {
        if (!TryGetComponent<Button>(out var button))
        {
            button = gameObject.AddComponent<Button>();
        }

        button.onClick.AddListener(() =>
        {
            popup.SetActive(false);
        });
    }
}
