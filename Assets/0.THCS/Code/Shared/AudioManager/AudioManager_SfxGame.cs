using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public partial class AudioManager
{
    [Header("Game SFX Settings")]
    private Dictionary<SfxGame, string> sfxGameKeys = new();
    private Dictionary<SfxGame, AudioClip> sfxGameCache = new();

    public enum SfxGame
    {
        Explosion,
        Arrow,
        Nuke
    }

    private void MappingSfxGameKeys()
    {
        sfxGameKeys[SfxGame.Explosion] = "SfxExplosion";
        sfxGameKeys[SfxGame.Arrow] = "SfxArrowShoot";
        sfxGameKeys[SfxGame.Nuke] = "SfxNuke";
    }

    public void PlaySFX(SfxGame sfx)
    {
        if (sfxGameCache.TryGetValue(sfx, out var clip))
        {
            PlaySFXInternal(clip);
        }
        else if (sfxGameKeys.TryGetValue(sfx, out var key))
        {
            Addressables.LoadAssetAsync<AudioClip>(key).Completed += handle =>
            {
                if (handle.Status == AsyncOperationStatus.Succeeded)
                {
                    sfxGameCache[sfx] = handle.Result;
                    PlaySFXInternal(handle.Result);
                }
                else
                {
                    Debug.LogError($"Failed to load Game SFX: {key}");
                }
            };
        }
        else
        {
            Debug.LogWarning($"Game SFX not mapped for: {sfx}");
        }
    }
}