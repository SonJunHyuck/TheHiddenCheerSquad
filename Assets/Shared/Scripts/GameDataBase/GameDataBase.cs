using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public partial class GameDataBase : Singleton<GameDataBase>
{
    [Header("Loader")]
    private bool isDataLoaded = false;
    public bool IsDataLoaded => isDataLoaded;
    
    protected override void Awake()
    {
        base.Awake();
        
        LoadGameData();
    }

    private async void LoadGameData()
    {
        DebugWrapper.Log("Initializing GameDataBase...");
        isDataLoaded = false;
        
        // Label을 이용한 게임 데이터 로드
        string dataLabel = "InfoData";
        var handle = Addressables.LoadResourceLocationsAsync(dataLabel);
        await handle.Task;

        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            foreach (var location in handle.Result)
            {
                var assetHandle = Addressables.LoadAssetAsync<ScriptableObject>(location.PrimaryKey);
                await assetHandle.Task;

                if (assetHandle.Status == AsyncOperationStatus.Succeeded)
                {
                    AssignLoadedData(assetHandle.Result, location.PrimaryKey);
                }
                else
                {
                    DebugWrapper.LogError($"Failed to load asset: {location.PrimaryKey}");
                }
            }
        }
        else
        {
            DebugWrapper.LogError("Failed to load resource locations for label InfoData.");
        }

        isDataLoaded = true;
        DebugWrapper.Log("GameDataBase initialization completed.");
    }

    private void AssignLoadedData(ScriptableObject asset, string key)
    {
        if (asset is StageInfoScriptableObject stage) stageInfo = stage;
        else if (asset is SpawnRuleScriptableObject spawnRule) ruleInfo = spawnRule;
        else if (asset is AllyInfoScriptableObject allyInfo) allyUnitInfo = allyInfo;
        else if (asset is UnitGradeScriptableObject unitGrade) unitGradeInfo = unitGrade;
        else if (asset is EnemyInfoScriptableObject enemy) enemyInfo = enemy;
        else if (asset is LoadingMessageScriptableObject loading) loadingMessage = loading;
        else Debug.LogWarning($"Unknown asset type: {key}");
    }
}