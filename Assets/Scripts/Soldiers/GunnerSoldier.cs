using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunnerSoldier : RangedSoldier {

    //! Soldier - public
    public override void Initialize(int level, bool enemy) {
        maxHealth = Config.SOLDIER_GUNNER_MAX_HEALTH[level];
        Initialize(level, enemy, maxHealth);
    }

    public override void Initialize(int level, bool enemy, int maxHealth) {
        scanRange = Config.SOLDIER_GUNNER_SCAN_RANGE[level];
        scanTime = Config.SOLDIER_GUNNER_SCAN_TIME;
        shootingRatio = Config.SOLDIER_GUNNER_SHOOTING_RATIO[level];
        shootingDamage = Config.SOLDIER_GUNNER_SHOOTING_DAMAGE[level];
        Initialize(level, enemy, scanRange, scanTime, shootingRatio, shootingDamage);
    }
}
