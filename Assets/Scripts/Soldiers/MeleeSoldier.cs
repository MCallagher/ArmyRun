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
    }


    //! MonoBehaviour
    void OnCollisionEnter(Collision other) {
        if((!enemy && other.gameObject.CompareTag(Config.TAG_ENEMY)) || (enemy && other.gameObject.CompareTag(Config.TAG_PLAYER))) {
            Attack(other.gameObject.GetComponent<Soldier>());
        }
    }


    //! Soldier - Public
    public override void Attack(Soldier target) {
        AttackData attack = new AttackData(AttackType.Slash, Strength);
        target.Defend(attack);
    }

    //! Soldier - Protected
    protected override void RecomputeProperties() {
        strength = count * Config.SOLDIER_MELEE_STRENGTH;
        maxHealth = count * Config.SOLDIER_MELEE_MAX_HEALTH;
        health = maxHealth;
    }
}
