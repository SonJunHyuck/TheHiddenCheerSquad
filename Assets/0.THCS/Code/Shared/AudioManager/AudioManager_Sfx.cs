using System.Collections.Generic;
using UnityEngine;

public partial class AudioManager
{
    [Header("SFX Settings")]
    protected AudioSource[] sfxPlayers;
    protected Queue<AudioSource> sfxQueue;
    public int channelCount = 10;

    [SerializeField] protected float sfxVolume = 1.0f;
    public float SFXVolume
    {
        get { return sfxVolume; }
        set
        {
            sfxVolume = value;
            foreach (var player in sfxPlayers)
            {
                player.volume = sfxVolume;
            }
            PlayerPrefs.SetFloat("sfx", value);
        }
    }

    protected virtual void InitSfxPlayer()
    {
        GameObject sfxPlayerObject = new GameObject("SfxPlayer");
        sfxPlayerObject.transform.SetParent(transform);
        sfxPlayers = new AudioSource[channelCount];

        for (int i = 0; i < sfxPlayers.Length; i++)
        {
            sfxPlayers[i] = sfxPlayerObject.AddComponent<AudioSource>();
            sfxPlayers[i].playOnAwake = false;
            sfxPlayers[i].volume = sfxVolume;
        }

        sfxQueue = new Queue<AudioSource>(sfxPlayers);
    }

    protected void PlaySFXInternal(AudioClip clip)
    {
        if (sfxQueue.Count > 0)
        {
            AudioSource player = sfxQueue.Dequeue();

            if (player.isPlaying)
            {
                player.Stop();
            }

            player.clip = clip;
            player.Play();

            sfxQueue.Enqueue(player);
        }
        else
        {
            Debug.LogWarning("No available AudioSource to play SFX.");
        }
    }
}