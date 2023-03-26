using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Bonus {

    //! Variables
    [SerializeField] private BonusType type;
    [SerializeField] private int value;


    //! Bonus - Public
    public Bonus(BonusType type) {
        // Get info
        int wave = ArmyManager.instance.Wave;

        this.type = type;
        if (type == BonusType.Heal) {
            value = -1;
        }
        else if (type == BonusType.AddMelee) {
            value = 1 + (wave / 2);
        }
        else if (type == BonusType.AddRanged) {
            value = 1 + (wave / 4);
        }
        else if (type == BonusType.AddGunner) {
            value = 1 + (wave / 6);
        }
        else if (type == BonusType.AddSniper) {
            value = 1 + (wave / 6);
        }
        else if (type == BonusType.AddBumper) {
            value = 1 + (wave / 3);
        }
        else if (type == BonusType.AddKamikaze) {
            value = 1 + (wave / 4);
        }
        else {
            throw new System.Exception("Bonus type (" + type + ") not found");
        }
    }

    public void ActivateBonus() {
        if (type == BonusType.AddMelee) {
            ArmyManager.instance.BonusExtraArmy<MeleeSoldier>(value);
        }
        else if (type == BonusType.AddRanged) {
            ArmyManager.instance.BonusExtraArmy<RangedSoldier>(value);
        }
        else if (type == BonusType.AddGunner) {
            ArmyManager.instance.BonusExtraArmy<GunnerSoldier>(value);
        }
        else if (type == BonusType.AddSniper) {
            ArmyManager.instance.BonusExtraArmy<SniperSoldier>(value);
        }
        else if (type == BonusType.AddBumper) {
            ArmyManager.instance.BonusExtraArmy<BumperSoldier>(value);
        }
        else if (type == BonusType.AddKamikaze) {
            ArmyManager.instance.BonusExtraArmy<KamikazeSoldier>(value);
        }
        else if (type == BonusType.Heal) {
            ArmyManager.instance.BonusHeal();
        }
    }

    public override string ToString() {
        if (type == BonusType.AddMelee) {
            return $"{value} melee";
        }
        if (type == BonusType.AddRanged) {
            return $"{value} ranged";
        }
        if (type == BonusType.AddGunner) {
            return $"{value} gunner";
        }
        if (type == BonusType.AddSniper) {
            return $"{value} sniper";
        }
        if (type == BonusType.AddBumper) {
            return $"{value} bumper";
        }
        if (type == BonusType.AddKamikaze) {
            return $"{value} kamikaze";
        }
        if (type == BonusType.Heal) {
            return $"Heal";
        }
        return "";
    }

    //! Bonus - Enum
    public enum BonusType {
        Heal = 0,
        AddMelee = 1,
        AddRanged = 2,
        AddGunner = 3,
        AddSniper = 4,
        AddBumper = 5,
        AddKamikaze = 6
    }
}