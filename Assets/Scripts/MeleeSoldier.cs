using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeSoldier : Soldier
{
    //! Soldier - Public
    public override void ResetSoldier() {
        health = enemy ? Config.SOLDIER_MELEE_HEALTH : Config.SOLDIER_MELEE_HEALTH * 100;
        attackDamage = Config.SOLDIER_MELEE_DAMAGE;
    }

    //! Soldier - Protected
    protected override void PlayerOnCollisionEnter(Collision other) {
        GenericOnCollisionEnter(other, "Enemy");
    }

    protected override void EnemyOnCollisionEnter(Collision other) {
        GenericOnCollisionEnter(other, "Player");
    }

    //! MeleeSoldier - Private
    private void GenericOnCollisionEnter(Collision other, string tagTarget) {
        if (other.gameObject.CompareTag(tagTarget)) {
            Attack(other.gameObject.GetComponent<Soldier>());
        }
    }
}
