// 🔹 SFX 슬라이더
public class SliderSfxSetting : SliderAudioSetting
{
    protected override string PlayerPrefsKey => "sfx";
    protected override void ApplyVolume(float value) => AudioManager.Instance.SFXVolume = value;
}