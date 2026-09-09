using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class MissingScriptRemover : EditorWindow
{
    private List<GameObject> prefabsToProcess = new List<GameObject>();

    [MenuItem("Tools/Remove Missing Scripts from Prefabs")]
    public static void ShowWindow()
    {
        GetWindow<MissingScriptRemover>("Prefab Missing Script Remover");
    }

    private void OnGUI()
    {
        GUILayout.Label("Remove Missing Scripts from Prefabs", EditorStyles.boldLabel);
        GUILayout.Space(5);

        // Prefab 리스트 추가 UI
        EditorGUILayout.LabelField("Drag and drop Prefabs below", EditorStyles.miniLabel);

        if (GUILayout.Button("Clear List"))
        {
            prefabsToProcess.Clear();
        }

        GUILayout.Space(5);

        // Prefab 리스트 UI 표시
        for (int i = 0; i < prefabsToProcess.Count; i++)
        {
            EditorGUILayout.BeginHorizontal();
            prefabsToProcess[i] = (GameObject)EditorGUILayout.ObjectField(prefabsToProcess[i], typeof(GameObject), false);
            if (GUILayout.Button("X", GUILayout.Width(20)))
            {
                prefabsToProcess.RemoveAt(i);
            }
            EditorGUILayout.EndHorizontal();
        }

        GUILayout.Space(5);

        // Drag & Drop 영역
        Event evt = Event.current;
        Rect dropArea = GUILayoutUtility.GetRect(0, 50, GUILayout.ExpandWidth(true));
        GUI.Box(dropArea, "Drag & Drop Prefabs Here", EditorStyles.helpBox);

        if (evt.type == EventType.DragUpdated || evt.type == EventType.DragPerform)
        {
            if (dropArea.Contains(evt.mousePosition))
            {
                DragAndDrop.visualMode = DragAndDropVisualMode.Copy;

                if (evt.type == EventType.DragPerform)
                {
                    DragAndDrop.AcceptDrag();
                    foreach (Object draggedObject in DragAndDrop.objectReferences)
                    {
                        if (draggedObject is GameObject prefab && PrefabUtility.GetPrefabAssetType(prefab) != PrefabAssetType.NotAPrefab)
                        {
                            if (!prefabsToProcess.Contains(prefab))
                                prefabsToProcess.Add(prefab);
                        }
                    }
                }
                Event.current.Use();
            }
        }

        GUILayout.Space(10);

        if (GUILayout.Button("Remove Missing Scripts from Prefabs"))
        {
            RemoveMissingScriptsFromPrefabs();
        }
    }

    private void RemoveMissingScriptsFromPrefabs()
{
    int totalRemovedCount = 0;

    foreach (var prefab in prefabsToProcess)
    {
        if (prefab == null) continue;

        string prefabPath = AssetDatabase.GetAssetPath(prefab);
        if (string.IsNullOrEmpty(prefabPath)) continue;

        // 프리팹 콘텐츠 로드
        GameObject prefabInstance = PrefabUtility.LoadPrefabContents(prefabPath);
        int removedCount = 0;

        // 메인 프리팹 오브젝트에서 Missing Script 제거
        removedCount += GameObjectUtility.RemoveMonoBehavioursWithMissingScript(prefabInstance);

        // 자식 오브젝트에서도 Missing Script 제거
        foreach (Transform child in prefabInstance.GetComponentsInChildren<Transform>(true))
        {
            removedCount += GameObjectUtility.RemoveMonoBehavioursWithMissingScript(child.gameObject);
        }

        if (removedCount > 0)
        {
            totalRemovedCount += removedCount;
            PrefabUtility.SaveAsPrefabAsset(prefabInstance, prefabPath);
            Debug.Log($"✅ {prefab.name}: {removedCount} Missing Scripts Removed (Including Children)");
        }

        PrefabUtility.UnloadPrefabContents(prefabInstance);
    }

    if (totalRemovedCount > 0)
    {
        Debug.Log($"🔍 Total {totalRemovedCount} Missing Scripts Removed from Prefabs and Children!");
        AssetDatabase.SaveAssets();
    }
    else
    {
        Debug.Log("✅ No Missing Scripts Found in Prefabs or Children.");
    }
}
}