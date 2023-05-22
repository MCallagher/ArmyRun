using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour {

    //! Static variables
    public static PoolManager instance;

    //! Variables
    private HashSet<System.Type> standardTypes = new HashSet<System.Type>(new System.Type[]{
        typeof(MeleeSoldier),
        typeof(RangedSoldier),
        typeof(GunnerSoldier),
        typeof(SniperSoldier),
        typeof(BumperSoldier),
        typeof(KamikazeSoldier),
        typeof(Wall),
        typeof(Bullet),
        typeof(Stone),
        typeof(Board),
        typeof(ExplosionParticles)
    });

    //! MonoBehaviour
    void Awake() {
        if (instance == null) {
            instance = this;
        }
        else {
            Destroy(gameObject);
        }
    }


    //! Pool - Public
    public GameObject GetEntity<T>() {
        // Generic types
        if (standardTypes.Contains(typeof(T))) {
            return Pool<T>.instance.GetEntity();
        }
        // Not valid type
        throw new System.Exception($"GameObject of Type {typeof(T)} cannot be pooled");
    }

    public List<GameObject> GetActiveGameObject<T>() {
        // Generic types
        if (standardTypes.Contains(typeof(T))) {
            return Pool<T>.instance.GetActiveGameObject();
        }
        // Soldier types
        if (typeof(T) == typeof(Soldier)) {
            List<GameObject> soldiers = new List<GameObject>();
            soldiers.AddRange(Pool<MeleeSoldier>.instance.GetActiveGameObject());
            soldiers.AddRange(Pool<RangedSoldier>.instance.GetActiveGameObject());
            soldiers.AddRange(Pool<GunnerSoldier>.instance.GetActiveGameObject());
            soldiers.AddRange(Pool<SniperSoldier>.instance.GetActiveGameObject());
            soldiers.AddRange(Pool<BumperSoldier>.instance.GetActiveGameObject());
            soldiers.AddRange(Pool<KamikazeSoldier>.instance.GetActiveGameObject());
            return soldiers;
        }
        // Not valid type
        throw new System.Exception($"GameObject of Type {typeof(T)} cannot be pooled");
    }

    public List<T> GetActiveEntities<T>() {
        // Generic types
        if (standardTypes.Contains(typeof(T))) {
            return Pool<T>.instance.GetActiveEntities();
        }
        // Not valid type
        throw new System.Exception($"GameObject of Type {typeof(T)} cannot be pooled");
    }
}
