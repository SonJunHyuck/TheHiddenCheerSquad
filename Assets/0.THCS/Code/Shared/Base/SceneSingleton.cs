using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance;
    private static readonly object lockObj = new object();
    private static bool isDestroyed = false;

    public static T Instance
    {
        get
        {
            if (isDestroyed)
            {
                Debug.LogWarning($"[SceneSingleton] Instance of {typeof(T)} is already destroyed.");
                return null;
            }

            lock (lockObj)
            {
                if (instance == null)
                {
                    instance = FindFirstObjectByType<T>();

                    if (instance == null)
                    {
                        GameObject singletonObj = new GameObject(typeof(T).Name);
                        instance = singletonObj.AddComponent<T>();
                    }
                }
                return instance;
            }
        }
    }

    protected virtual void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this as T;
        SceneManager.sceneUnloaded += OnSceneUnloaded; // ✅ 씬이 언로드되면 자동 제거
    }

    private void OnSceneUnloaded(Scene scene)
    {
        if (instance == this)
        {
            isDestroyed = true;
            instance = null;
        }
    }

    protected virtual void OnDestroy()
    {
        if (instance == this)
        {
            isDestroyed = true;
        }
    }
}