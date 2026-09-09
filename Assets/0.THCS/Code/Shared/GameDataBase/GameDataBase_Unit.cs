using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public partial class GameDataBase
{
    [Header("Ally")]
    [SerializeField] private AllyInfoScriptableObject allyUnitInfo;
    [SerializeField] private UnitGradeScriptableObject unitGradeInfo;

    public int MaxUnitGrade => 20;

    public AllyInfoScriptableObject.UnitInfo GetUnitInfo(string key)
    {
        return allyUnitInfo.GetUnitInfo(key);
    }

    public UnitGradeScriptableObject.GradeInfo GetGradeInfo(string key, int grade)
    {
        return unitGradeInfo.GetGradeInfo(key, grade);
    }

    public UnitGradeScriptableObject.GradeInfo GetMaxGradeInfo()
    {
        return GetGradeInfo("99_Max", 99);
    }

    public List<string> GetAllUnitKey()
    {
        return allyUnitInfo.units.Select(unit => unit.unitKey).ToList();
    }
}