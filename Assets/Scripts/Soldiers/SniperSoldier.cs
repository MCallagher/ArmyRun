using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperSoldier : RangedSoldier {

    //! Soldier - public
    public override void Initialize(int level, bool enemy) {
        maxHealth = Config.SOLDIER_SNIPER_MAX_HEALTH[level];
        Initialize(level, enemy, maxHealth);
    }

    public override void Initialize(int level, bool enemy, int maxHealth) {
        scanRange = Config.SOLDIER_SNIPER_SCAN_RANGE[level];
        scanTime = Config.SOLDIER_SNIPER_SCAN_TIME;
        shootingRatio = Config.SOLDIER_SNIPER_SHOOTING_RATIO[level];
        shootingDamage = Config.SOLDIER_SNIPER_SHOOTING_DAMAGE[level];
        Initialize(level, enemy, scanRange, scanTime, shootingRatio, shootingDamage);
    }
}
