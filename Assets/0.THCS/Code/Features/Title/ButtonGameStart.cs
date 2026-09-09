using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ButtonGameStart : MonoBehaviour
{
    void Start()
    {
        GetComponent<Button>().onClick.AddListener( () => 
        {
            if(GameDataBase.Instance.IsDataLoaded)
            {
                // SceneManager.LoadScene("ReadyScene");
                LoadSceneManager.Instance.LoadScene("MainMenu");
            }
            else
            {
                DebugWrapper.LogWarning("DataManager is not Ready yet");
            }
        } );
    }
}
