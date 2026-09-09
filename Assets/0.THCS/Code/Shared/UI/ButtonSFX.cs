using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ButtonSFX : MonoBehaviour
{
    private Button button;

    void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(PlayButtonSFX);
    }

    private void PlayButtonSFX()
    {
        AudioManager.Instance.PlaySFX(AudioManager.SfxUI.Button);
    }
}