using UnityEngine;

public partial class GameDataBase
{
    [Header("Loading")]
    [SerializeField] private LoadingMessageScriptableObject loadingMessage ;

    public string GetRandomLoadingMessage()
    {
        DebugWrapper.Log(loadingMessage.GetRandomMessage());
        return loadingMessage.GetRandomMessage();
    }
}