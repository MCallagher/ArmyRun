using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeSoldier : Soldier
{
    //! Soldier - Public
    public override void InitializeSoldier(int count, bool enemy) {
        baseReward = Config.SOLDIER_MELEE_COINS;
        base.InitializeSoldier(count, enemy);
    }

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


    //! Soldier - Protected
    protected override void PlayerOnCollisionEnter(Collision other) {
        AttackOnCollisionEnter(other, "Enemy");
    }

    protected override void EnemyOnCollisionEnter(Collision other) {
        AttackOnCollisionEnter(other, "Player");
    }

    protected override void Attack(Soldier target) {
        target.Health -= Strength;
    }

    protected override void RecomputeProperties() {
        float currHealthPrc = constitution != 0 ? health / constitution : 0;
        strength = count * Config.SOLDIER_MELEE_STRENGTH;
        constitution = count * Config.SOLDIER_MELEE_CONSTITUTION;
        health = constitution;
        if (gameObject.activeInHierarchy) {
            health = (int)(currHealthPrc * health);
        }
    }


    //! MeleeSoldier - Private
    private void AttackOnCollisionEnter(Collision other, string tagTarget) {
        if (other.gameObject.CompareTag(tagTarget)) {
            Attack(other.gameObject.GetComponent<Soldier>());
        }
    }
}
