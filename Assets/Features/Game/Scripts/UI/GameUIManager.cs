using UnityEngine;

public class GameUIManager : MonoBehaviour
{   
    private void Start()
    {
        InitializeAllySpawnButtons();
    }

    #region Ally Spawn
    [Header("Ally Spawn Button")]
    [SerializeField] private GameObject buttonPrefab;   // AllySpawnButton 프리팹
    [SerializeField] private Transform buttonContainer; // 버튼들이 배치될 부모 Transform

    // 버튼 초기화를 시작합니다.
    public void InitializeAllySpawnButtons()
    {
        var keys = GameDataBase.Instance.GetAllUnitKey();  // View - Model 접근
        int num = 1;
        foreach(var key in keys)
        {
            GameObject button = Instantiate(buttonPrefab, buttonContainer);
            Sprite portrait = GameDataBase.Instance.GetUnitInfo(key).unitIcon;  // View - Model 접근
            int cost = GameDataBase.Instance.GetUnitInfo(key).cost;
            
            ButtonAllySpawn allyButton = button.GetComponent<ButtonAllySpawn>();
            allyButton.Initialize(key, cost, portrait, num++); // Key와 Sprite 전달
        }
    }
    #endregion
}