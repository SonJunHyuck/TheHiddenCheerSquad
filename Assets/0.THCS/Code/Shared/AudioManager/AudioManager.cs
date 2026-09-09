using UnityEngine;

public partial class AudioManager : Singleton<AudioManager>
{
    protected override void Awake()
    {
        base.Awake();

        // 🎵 BGM 초기화
        InitBgmPlayer();
        MappingBgmKeys();

        // 🔊 SFX 초기화
        InitSfxPlayer();
        MappingSfxUIKeys();
        MappingSfxGameKeys();
    }
}