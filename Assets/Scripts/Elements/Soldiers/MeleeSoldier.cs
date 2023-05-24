using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// INHERITANCE
public class MeleeSoldier : Soldier {

    //! Variables
    [SerializeField] protected int strength;

    //! Properties
    // ENCAPSULATION
    public int Strength {
        get {
            return strength;
        }
        protected set {
            strength = Mathf.Max(0, value);
        }
    }


    //! MonoBehaviour
    void Awake() {
        Waypoint = GameObject.Find(Config.WAYPOINT_MELEE_NAME);
        SetupSoldier();
    }

    void OnCollisionEnter(Collision other) {
        if((!enemy && other.gameObject.CompareTag(Config.TAG_ENEMY)) || (enemy && other.gameObject.CompareTag(Config.TAG_PLAYER))) {
            Attack(other.gameObject.GetComponent<Soldier>());
        }
    }


    //! Soldier - Public
    // POLYMORPHISM
    public override void Initialize(int level, bool enemy) {
        maxHealth = Config.SOLDIER_MELEE_MAX_HEALTH[level];
        Initialize(level, enemy, maxHealth);
    }

    // POLYMORPHISM
    public override void Initialize(int level, bool enemy, int maxHealth) {
        strength = Config.SOLDIER_MELEE_STRENGTH[level];
        Initialize(level, enemy, maxHealth, strength);
    }

    // POLYMORPHISM
    public virtual void Initialize(int level, bool enemy, int maxHealth, int strength) {
        base.Initialize(level, enemy, maxHealth);
        Strength = strength;
    }

    public override void Attack(Soldier target) {
        AttackData attack = new AttackData(AttackType.Slash, Strength);
        target.Defend(attack);
    }
}
