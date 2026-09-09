using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public partial class AudioManager
{
    [Header("UI SFX Settings")]
    private Dictionary<SfxUI, string> sfxUIKeys = new();
    private Dictionary<SfxUI, AudioClip> sfxUICache = new();

    public enum SfxUI
    {
        Button,
        Error,
        Success
    }

    private void MappingSfxUIKeys()
    {
        sfxUIKeys[SfxUI.Button] = "SfxButton";
        sfxUIKeys[SfxUI.Success] = "SfxSuccess";
        sfxUIKeys[SfxUI.Error] = "SfxError";
    }

    public void PlaySFX(SfxUI sfx)
    {
        if (sfxUICache.TryGetValue(sfx, out AudioClip clip))
        {
            PlaySFXInternal(clip);
        }
        else if (sfxUIKeys.TryGetValue(sfx, out string key))
        {
            Addressables.LoadAssetAsync<AudioClip>(key).Completed += handle =>
            {
                if (handle.Status == AsyncOperationStatus.Succeeded)
                {
                    sfxUICache[sfx] = handle.Result;
                    PlaySFXInternal(handle.Result);
                }
                else
                {
                    Debug.LogError($"Failed to load UI SFX: {key}");
                }
            };
        }
        else
        {
            Debug.LogWarning($"UI SFX not mapped for: {sfx}");
        }
    }
}