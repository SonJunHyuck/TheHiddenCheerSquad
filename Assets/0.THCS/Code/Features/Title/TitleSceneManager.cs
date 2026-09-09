using System.Collections.Generic;
using UnityEngine;

public class TitleSceneManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> characters;

    private void Start()
    {
        foreach(GameObject character in characters)
        {
            character.GetComponentInChildren<Animator>().SetBool("IsMoving", true);
        }

        AudioManager.Instance.SFXVolume = PlayerPrefs.GetFloat("sfx", 1.0f);
        AudioManager.Instance.BGMVolume = PlayerPrefs.GetFloat("bgm", 1.0f);
        AudioManager.Instance.SetBGM(AudioManager.BgmTrack.Title);
    }
}
