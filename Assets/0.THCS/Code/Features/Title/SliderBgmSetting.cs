

// 🔹 BGM 슬라이더
public class SliderBgmSetting : SliderAudioSetting
{
    protected override string PlayerPrefsKey => "bgm";
    protected override void ApplyVolume(float value) => AudioManager.Instance.BGMVolume = value;
}