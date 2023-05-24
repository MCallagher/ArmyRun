using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// INHERITANCE
public class BumperSoldier : MeleeSoldier {

    //! Variables
    [SerializeField] protected float pushForce;

    //! Properties
    // ENCAPSULATION
    public float PushForce {
        get {
            return pushForce;
        }
        protected set {
            pushForce = Mathf.Max(0, value);
        }
    }

    //! Soldier - Public
    // POLYMORPHISM
    public override void Initialize(int level, bool enemy) {
        maxHealth = Config.SOLDIER_BUMPER_MAX_HEALTH[level];
        Initialize(level, enemy, maxHealth);
    }

    // POLYMORPHISM
    public override void Initialize(int level, bool enemy, int maxHealth) {
        strength = Config.SOLDIER_BUMPER_STRENGTH[level];
        pushForce = Config.SOLDIER_BUMPER_PUSH_FORCE[level];
        Initialize(level, enemy, maxHealth, strength, pushForce);
    }

    // POLYMORPHISM
    public override void Initialize(int level, bool enemy, int maxHealth, int strength) {
        throw new System.NotImplementedException();
    }

    // POLYMORPHISM
    public virtual void Initialize(int level, bool enemy, int maxHealth, int strength, float pushForce) {
        base.Initialize(level, enemy, maxHealth, strength);
        PushForce = pushForce;
    }

    public override void Attack(Soldier target) {
        AttackData attack = new AttackData(AttackType.Slash, Strength);
        target.Defend(attack);
        Vector3 direction = (target.transform.position - transform.position).normalized;
        target.Push(direction * pushForce);
    }
}
