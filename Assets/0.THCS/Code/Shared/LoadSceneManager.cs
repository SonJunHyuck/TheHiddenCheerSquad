using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadSceneManager : Singleton<LoadSceneManager>
{   
    private string nextSceneName; // 로딩 후 이동할 씬 이름

    protected override void Awake()
    {
        base.Awake();
    }

    // 씬 전환을 요청하는 함수 (메인 게임 코드에서 이 함수 호출)
    public void LoadScene(string sceneName)
    {
        nextSceneName = sceneName;
        
        StartCoroutine(LoadAsync());  // 타겟 씬으로 이동
    }

    // 실제 씬을 비동기로 로드하는 코루틴
    private IEnumerator LoadAsync()
    {
        yield return SceneManager.LoadSceneAsync("Loading"); // 먼저 로딩 씬으로 이동

        // 설명 작성
        TextMeshProUGUI textExplain = FindFirstObjectByType<TextMeshProUGUI>();
        textExplain.text = GameDataBase.Instance.GetRandomLoadingMessage();

        yield return new WaitForSeconds(0.5f);  // 의도적으로 기다리기

        // 씬 로드 시작 (Progress Bar)
        AsyncOperation operation = SceneManager.LoadSceneAsync(nextSceneName);
        operation.allowSceneActivation = false;
        Slider progressBar = FindFirstObjectByType<Slider>();

        while (!operation.isDone)
        {
            progressBar.value = Mathf.Clamp01(operation.progress / 0.9f);

            if (operation.progress >= 0.9f)
            {
                yield return new WaitForSeconds(0.5f);
                operation.allowSceneActivation = true;
            }

            yield return null;
        }
    }
}