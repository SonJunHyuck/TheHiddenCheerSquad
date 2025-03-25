using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public partial class AudioManager
{
    [Header("BGM Settings")]
    public AudioClip bgmClip;
    private AudioSource bgmPlayer;
    [SerializeField] private float bgmVolume = 1.0f;
    public float BGMVolume
    {
        get { return bgmVolume; }
        set
        {
            bgmVolume = value;
            bgmPlayer.volume = value;
            PlayerPrefs.SetFloat("bgm", value);
        }
    }

    private Dictionary<BgmTrack, string> bgmKeys = new();

    public enum BgmTrack
    {
        Title,
        Ready,
        Forest
    }

    public enum BgmState
    {
        Play,
        Pause,
        Resume,
        Stop
    }

    private void InitBgmPlayer()
    {
        GameObject bgmPlayerObject = new GameObject("BgmPlayer");
        bgmPlayerObject.transform.SetParent(transform);
        bgmPlayer = bgmPlayerObject.AddComponent<AudioSource>();
        bgmPlayer.playOnAwake = false;
        bgmPlayer.loop = true;
        bgmPlayer.volume = bgmVolume;
        bgmPlayer.clip = bgmClip;
    }

    private void MappingBgmKeys()
    {
        bgmKeys[BgmTrack.Title] = "BgmTitle";
        bgmKeys[BgmTrack.Ready] = "BgmReady";
        bgmKeys[BgmTrack.Forest] = "BgmForest";
    }

    public void SetBGM(BgmTrack bgm)
    {
        if (bgmKeys.TryGetValue(bgm, out var key))
        {
            Addressables.LoadAssetAsync<AudioClip>(key).Completed += handle =>
            {
                if (handle.Status == AsyncOperationStatus.Succeeded)
                {
                    bgmClip = handle.Result;
                    bgmPlayer.clip = bgmClip;
                    bgmPlayer.time = 0;
                    bgmPlayer.Play();
                }
                else
                {
                    Debug.LogError($"Failed to load BGM: {key}");
                }
            };
        }
        else
        {
            Debug.LogWarning($"BGM not mapped for: {bgm}");
        }
    }

    public void ControlBGM(BgmState state)
    {
        switch (state)
        {
            case BgmState.Play:
                bgmPlayer.Play();
                break;
            case BgmState.Pause:
                bgmPlayer.Pause();
                break;
            case BgmState.Resume:
                bgmPlayer.UnPause();
                break;
            case BgmState.Stop:
                bgmPlayer.Stop();
                break;
        }
    }
}