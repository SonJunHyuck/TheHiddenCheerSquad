public static class PlayerDataManager
{
    #region Currency
        public static int GetCurrency() => CurrencyData.GetCurrency();
        public static void AddCurrency(int amount) => CurrencyData.AddCurrency(amount);
        public static bool SpendCurrency(int amount) => CurrencyData.SpendCurrency(amount);
    #endregion

    #region Stage
        public static int GetStageClearGrade(int stageId) => StageProgressData.GetStageClearGrade(stageId);
        public static void SaveStageClear(int stageId, int clearGrade) => StageProgressData.SaveStageClear(stageId, clearGrade);
        public static bool IsOpenStage(int stageId) => StageProgressData.IsOpenStage(stageId);
    #endregion

    #region UnitGrade
        public static int GetUnitGrade(string unitKey) => UnitUpgradeData.GetUnitGrade(unitKey);
        public static void UpgradeUnit(string unitKey) => UnitUpgradeData.UpgradeUnit(unitKey);
    #endregion

    
}