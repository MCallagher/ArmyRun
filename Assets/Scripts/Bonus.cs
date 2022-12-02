using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Bonus {
    public BonusType type;
    public int value;
    public int cost;

    public Bonus() {
        // Get game information
        int coins = GameManager.instance.Coins;
        int totPlayerCount = 0;
        foreach (MeleeSoldier meleeSoldier in PoolMeleeSoldier.instance.GetActiveEntities()) {
            if (!meleeSoldier.Enemy) {
                totPlayerCount += meleeSoldier.Count;
            }
        }

        // Compute distribution
        List<float> distribution = new List<float>(Config.WALL_BONUS_DISTRIBUTION);
        if (coins < totPlayerCount * Config.BONUS_HEAL_COST) {
            distribution[0] += distribution[1];
            distribution[1] = 0f;
        }
        if (coins < totPlayerCount * Config.BONUS_MERGE_COST) {
            distribution[0] += distribution[2];
            distribution[2] = 0f;
        }
        type = (BonusType) AdvancedRandom.RangeWithWeight(distribution);

        Debug.Log(coins + " " + (totPlayerCount * Config.BONUS_HEAL_COST) + " " + (totPlayerCount * Config.BONUS_MERGE_COST) );//+ " | " + distribution[0] + " " + distribution[1] + " " + distribution[2]);

        if (type == BonusType.ExtraArmy) {
            int max = (int)(coins / Config.BONUS_MELEE_COST);
            value = Random.Range(1, max + 1);
            cost = (value - 1) * Config.BONUS_MELEE_COST;
        }
        else if (type == BonusType.Heal) {
            int max = (int)(coins / (totPlayerCount * Config.BONUS_HEAL_COST));
            value = 10 * Random.Range(1, Mathf.Max(11, max + 1));
            cost = (value / 10) * totPlayerCount * Config.BONUS_HEAL_COST;
        }
        else if (type == BonusType.Merge) {
            value = -1;
            cost = totPlayerCount * Config.BONUS_MERGE_COST;
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
        if (type == BonusType.ExtraArmy) {
            GameManager.instance.BonusExtraArmy(value);
        }
        else if (type == BonusType.Heal) {
            GameManager.instance.BonusHeal(value);
        }
        else if (type == BonusType.Merge) {
            GameManager.instance.BounsMerge();
        }
    }

    public override string ToString() {
        if (type == BonusType.ExtraArmy) {
            return "Add "+ value + " melee\n" + cost + " coins";
        }
        if (type == BonusType.Heal) {
            return "Heal "+ value + "%\n" + cost + " coins";
        }
        if (type == BonusType.Merge) {
            return "Merge army\n" + cost + " coins";
        }
        return "";
    }

    public enum BonusType {
        ExtraArmy = 0, 
        Heal = 1,
        Merge = 2 
    }
}