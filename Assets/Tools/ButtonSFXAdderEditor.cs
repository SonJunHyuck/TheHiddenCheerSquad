using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

public class ButtonSFXAdderEditor : EditorWindow
{
    [MenuItem("Tools/Button SFX Adder")]
    public static void ShowWindow()
    {
        GetWindow<ButtonSFXAdderEditor>("Button SFX Adder");
    }

    private void OnGUI()
    {
        GUILayout.Label("Button SFX Adder", EditorStyles.boldLabel);
        GUILayout.Space(10);

        if (GUILayout.Button("Update All Buttons"))
        {
            AddButtonSFXToAllButtons();
        }
    }

    private static void AddButtonSFXToAllButtons()
    {
        Button[] buttons = FindObjectsByType<Button>(FindObjectsInactive.Include, FindObjectsSortMode.None); // 씬에 있는 모든 버튼 가져오기

        int addedCount = 0;

        foreach (Button button in buttons)
        {
            if (!button.gameObject.GetComponent<ButtonSFX>()) // ButtonSFX가 없는 버튼만 추가
            {
                Undo.AddComponent<ButtonSFX>(button.gameObject); // Undo 지원
                addedCount++;
            }
        }

        Debug.Log($"Button SFX 추가 완료: {addedCount}개 버튼 수정됨!");
    }
}