using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    void Start()
    {
        AudioManager.Instance.SetBGM(AudioManager.BgmTrack.Ready);   
    }
}