using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "LoadingMessageScriptableObject", menuName = "ScriptableObjects/LoadingMessageScriptableObject", order = 1)]
public class LoadingMessageScriptableObject : ScriptableObject
{
    [TextArea] // 다중 라인 입력 지원
    public List<string> messages = new(); // 로딩 메시지 배열

    public string GetRandomMessage()
    {
        if(messages.Count == 0)
        {
            return "You Can Spawn Unit";
        }

        int randIdx = Random.Range(0, messages.Count);
        return messages[randIdx];
    }
}