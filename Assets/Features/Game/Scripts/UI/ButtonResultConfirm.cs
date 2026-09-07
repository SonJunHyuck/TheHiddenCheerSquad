using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ButtonResultConfirm : MonoBehaviour
{
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(() =>
        {
            Resources.UnloadUnusedAssets();

            LoadSceneManager.Instance.LoadScene("MainMenu");
            // SceneManager.LoadScene("ReadyScene", LoadSceneMode.Single);
        });
    }
}