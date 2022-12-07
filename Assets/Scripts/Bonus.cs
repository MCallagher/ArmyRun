using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Bonus {
    [SerializeField] private BonusType type;
    [SerializeField] private int value;
    [SerializeField] private int cost;

    public Bonus(BonusType type) {
        // Get info
        int coins = GameManager.instance.Coins;
        int count = GetArmyCount();

        this.type = type;
        if (type == BonusType.Merge) {
            value = -1;
            cost = count * Config.BONUS_MERGE_COST;
        }
        else if (type == BonusType.Heal) {
            value = -1;
            cost = count * Config.BONUS_HEAL_COST;
        }
        else if (type == BonusType.AddMelee) {
            int max = (int)(coins / Config.BONUS_MELEE_COST);
            value = Random.Range(1, max + 1);
            cost = (value - 1) * Config.BONUS_MELEE_COST;
        }
        else if (type == BonusType.AddRanged) {
            int max = (int)(coins / Config.BONUS_RANGED_COST);
            value = Random.Range(1, max + 1);
            cost = (value - 1) * Config.BONUS_RANGED_COST;
        }
        else {
            throw new System.Exception("Bonus type (" + type + ") not found");
        }
    }

    public void ActivateBonus() {
        if (cost > GameManager.instance.Coins) {
            return;
        }
        GameManager.instance.Coins -= cost;
        if (type == BonusType.AddMelee) {
            GameManager.instance.BonusExtraArmy<MeleeSoldier>(value);
        }
        else if (type == BonusType.AddRanged) {
            GameManager.instance.BonusExtraArmy<RangedSoldier>(value);
        }
        else if (type == BonusType.Heal) {
            GameManager.instance.BonusHeal();
        }
        else if (type == BonusType.Merge) {
            GameManager.instance.BounsMerge();
        }
    }

    public override string ToString() {
        if (type == BonusType.AddMelee) {
            return $"{value} melee\n({cost} coins)";
        }
        if (type == BonusType.AddRanged) {
            return $"{value} ranged\n({cost} coins)";
        }
        if (type == BonusType.Heal) {
            return $"Heal\n({cost} coins)";
        }
        if (type == BonusType.Merge) {
            return $"Merge\n({cost} coins)";
        }
        return "";
    }

    //! Bonus - Private
    private int GetArmyCount() {
        int count = 0;
        List<Soldier> soldiers = new List<Soldier>();
        soldiers.AddRange(PoolManager.instance.GetActiveEntities<MeleeSoldier>());
        soldiers.AddRange(PoolManager.instance.GetActiveEntities<RangedSoldier>());
        foreach (Soldier soldier in soldiers) {
            if (!soldier.Enemy) {
                count += soldier.Count;
            }
        }
        return count;
    }

    public enum BonusType {
        Merge = 0,
        Heal = 1,
        AddMelee = 2,
        AddRanged = 3
    }
}