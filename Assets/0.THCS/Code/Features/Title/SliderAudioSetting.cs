using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public abstract class SliderAudioSetting : MonoBehaviour
{
    protected abstract string PlayerPrefsKey { get; }
    protected abstract void ApplyVolume(float value);

    void OnEnable() 
    {
        GetComponent<Slider>().value = PlayerPrefs.GetFloat(PlayerPrefsKey, 1.0f);
    }

    void Start()
    {
        GetComponent<Slider>().onValueChanged.AddListener(value =>
        {
            ApplyVolume(value);
            PlayerPrefs.SetFloat(PlayerPrefsKey, value);
        });
    }
}