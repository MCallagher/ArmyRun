using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeSoldier : Soldier {

    //! Static variables
    public static GameObject meleeWaypoint;

    //! Variables
    [SerializeField] protected int strength;

    //! Properties
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
        if(meleeWaypoint == null) {
            meleeWaypoint = GameObject.Find(Config.WAYPOINT_MELEE_NAME);
        }
        Waypoint = meleeWaypoint;
        SetupSoldier();
    }

    void OnCollisionEnter(Collision other) {
        if((!enemy && other.gameObject.CompareTag(Config.TAG_ENEMY)) || (enemy && other.gameObject.CompareTag(Config.TAG_PLAYER))) {
            Attack(other.gameObject.GetComponent<Soldier>());
        }
    }


    //! Soldier - Public
    public override void Initialize(int level, bool enemy) {
        maxHealth = Config.SOLDIER_MELEE_MAX_HEALTH[level];
        Initialize(level, enemy, maxHealth);
    }

    public override void Initialize(int level, bool enemy, int maxHealth) {
        strength = Config.SOLDIER_MELEE_STRENGTH[level];
        Initialize(level, enemy, maxHealth, strength);
    }

    public virtual void Initialize(int level, bool enemy, int maxHealth, int strength) {
        base.Initialize(level, enemy, maxHealth);
        Strength = strength;
    }

    public override void Attack(Soldier target) {
        AttackData attack = new AttackData(AttackType.Slash, Strength);
        target.Defend(attack);
    }
}
