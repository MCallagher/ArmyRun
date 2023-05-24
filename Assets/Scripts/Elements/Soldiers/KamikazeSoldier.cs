using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// INHERITANCE
public class KamikazeSoldier : MeleeSoldier {

    //! Variables
    [SerializeField] protected float explosionRadius;

    //! Properties
    // ENCAPSULATION
    public float ExplosionRadius {
        get {
            return explosionRadius;
        }
        protected set {
            explosionRadius = Mathf.Max(0, value);
        }
    }

    //! Soldier - Public
    // POLYMORPHISM
    public override void Initialize(int level, bool enemy) {
        maxHealth = Config.SOLDIER_KAMIKAZE_MAX_HEALTH[level];
        Initialize(level, enemy, maxHealth);
    }

    // POLYMORPHISM
    public override void Initialize(int level, bool enemy, int maxHealth) {
        strength = Config.SOLDIER_KAMIKAZE_STRENGTH[level];
        explosionRadius = Config.SOLDIER_KAMIKAZE_EXPLOSION_RADIUS[level];
        Initialize(level, enemy, maxHealth, strength, explosionRadius);
    }

    // POLYMORPHISM
    public override void Initialize(int level, bool enemy, int maxHealth, int strength) {
        throw new System.NotImplementedException();
    }

    // POLYMORPHISM
    public virtual void Initialize(int level, bool enemy, int maxHealth, int strength, float pushForce) {
        base.Initialize(level, enemy, maxHealth, strength);
        ExplosionRadius = explosionRadius;
    }

    public override void Attack(Soldier target) {
        AttackData attack = new AttackData(AttackType.Explosion, Strength);
        foreach(GameObject soldierObject in PoolManager.instance.GetActiveGameObject<Soldier>()) {
            Soldier soldier = soldierObject.GetComponent<Soldier>();
            float distance = (soldier.transform.position - transform.position).magnitude;
            if(soldier.Enemy && distance <= ExplosionRadius) {
                soldier.Defend(attack);
            }
        }
        Die();
    }
}
