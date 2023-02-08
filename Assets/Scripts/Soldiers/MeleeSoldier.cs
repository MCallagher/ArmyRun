using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeSoldier : Soldier
{
    
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
    void OnCollisionEnter(Collision other) {
        if((!enemy && other.gameObject.CompareTag(Config.TAG_ENEMY)) || (enemy && other.gameObject.CompareTag(Config.TAG_PLAYER))) {
            Attack(other.gameObject.GetComponent<Soldier>());
        }
    }


    //! Soldier - Public
    public override void Initialize(int level, bool enemy) {
        maxHealth = level * Config.SOLDIER_MELEE_MAX_HEALTH;
        Initialize(level, enemy, maxHealth);
    }

    public override void Initialize(int level, bool enemy, int maxHealth) {
        strength = level * Config.SOLDIER_MELEE_STRENGTH;
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
