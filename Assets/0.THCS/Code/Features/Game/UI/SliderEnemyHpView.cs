using UnityEngine;

public class SliderEnemyHpView : SliderBaseHpView
{
    public override void OnEnable()
    {
        UnitBossEnemyHealth enemyHealth = GameObject.FindFirstObjectByType<UnitBossEnemyHealth>();
        if(enemyHealth != null)
        {
            enemyHealth.onSetHp += ResponseSetHp;
            enemyHealth.SetHpView();
        }
    }

    public override void OnDisable()
    {
        UnitBossEnemyHealth enemyHealth = GameObject.FindFirstObjectByType<UnitBossEnemyHealth>();
        if(enemyHealth != null)
        {
            enemyHealth.onSetHp -= ResponseSetHp;
        }
    }
}