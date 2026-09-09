using UnityEngine;

public partial class GameDataBase
{
    [Header("Enemy")]
    [SerializeField] private EnemyInfoScriptableObject enemyInfo;

    public EnemyInfoScriptableObject.UnitInfo GetEnemyInfo(string key)
    {
        return enemyInfo.units.Find(unit => unit.unitKey == key);
    }

    public int GetEnemyGold(string key)
    {
        return enemyInfo.GetUnitInfo(key).gold;
    }
}