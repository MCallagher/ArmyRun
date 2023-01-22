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

    /*
    public void Merge(List<MeleeSoldier> meleeSoldiers) {
        int totHealth = Health;
        int totCount = Count;
        foreach (MeleeSoldier meleeSoldier in meleeSoldiers) {
            totHealth += meleeSoldier.Health;
            totCount += meleeSoldier.Count;
            meleeSoldier.Die();
        }
        Count = totCount;
        Health = totHealth;
    }

    public override void Merge(List<Soldier> soldiers) {
        List<MeleeSoldier> meleeSoldiers = new List<MeleeSoldier>();
        foreach (Soldier soldier in soldiers) {
            if (soldier is MeleeSoldier) {
                meleeSoldiers.Add((MeleeSoldier) soldier);
            }
        }
        Merge(meleeSoldiers);
    }
    */

    //! Soldier - Protected
    protected override void RecomputeProperties() {
        strength = count * Config.SOLDIER_MELEE_STRENGTH;
        maxHealth = count * Config.SOLDIER_MELEE_CONSTITUTION;
        health = maxHealth;
    }
}
