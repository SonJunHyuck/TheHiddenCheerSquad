using System.Collections.Generic;
using UnityEngine;

public partial class GameDataBase
{
    [Header("Stage")]
    [SerializeField] private StageInfoScriptableObject stageInfo;
    [SerializeField] private SpawnRuleScriptableObject ruleInfo;
    public int currentStageId;

    public int GetStageInfoListCount()
    {
        return stageInfo.stageInfoList.Count;
    }

    public StageInfoScriptableObject.StageInfo GetStageInfo(int idx)
    {
        return stageInfo.GetStageInfo(idx);
    }

    public StageInfoScriptableObject.StageInfo GetCurrentStageInfo()
    {
        return GetStageInfo(currentStageId);
    }

    // 해당 스테이지에 어떤 몬스터 등장 정보를 반환합니다. (몬스터 정보x)
    public List<SpawnRuleScriptableObject.SpawnRule> GetSpawnRule(int stageId)
    {
        return ruleInfo.spawnRules.FindAll(rule => rule.stageId == stageId);
    }
}