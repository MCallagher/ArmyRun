using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeSoldier : Soldier
{
    //! Soldier - Public
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
    protected override void PlayerUpdate() {
        // No action
    }

    protected override void EnemyUpdate() {
        // No action
    }

    protected override void PlayerOnCollisionEnter(Collision other) {
        GenericOnCollisionEnter(other, "Enemy");
    }

    protected override void EnemyOnCollisionEnter(Collision other) {
        GenericOnCollisionEnter(other, "Player");
    }

    protected override void Attack(Soldier target) {
        target.Health -= strength;
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

    protected override List<Soldier> Scan(float radius) {
        throw new System.NotImplementedException();
    }

    protected override void Die() {
        if (Enemy && Health == 0) {
            GameManager.instance.Coins += Count * Config.SOLDIER_MELEE_COINS;
        }
        base.Die();
    }

    //! MeleeSoldier - Private
    private void GenericOnCollisionEnter(Collision other, string tagTarget) {
        if (other.gameObject.CompareTag(tagTarget)) {
            Attack(other.gameObject.GetComponent<Soldier>());
        }
    }
}
