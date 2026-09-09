using System.Collections.Generic;
using UnityEngine;

public class PoolManager : SceneSingleton<PoolManager>
{
    private Dictionary<string, ObjectPool> pools;
    public ObjectPool UnitPool
    {
        get { return pools["Unit"]; }
    }

    public ObjectPool ProjectilePool
    {
        get { return pools["Projectile"]; }
    }

    public ObjectPool EffectPool
    {
        get { return pools["Effect"]; }
    }

    public void InitPoolManager()
    {
        pools = new();
        
        CreatePool("Unit");
        CreatePool("Projectile");
        CreatePool("Effect");
    }

    private void CreatePool(string assetsLabel)
    {
        GameObject pool = new GameObject(assetsLabel);
        pool.transform.SetParent(this.transform); // GameManager 하위에 추가
        
        ObjectPool objectPool = pool.AddComponent<ObjectPool>();
        objectPool.InitObjectPool(assetsLabel);

        pools.Add(assetsLabel, objectPool);
    }
}