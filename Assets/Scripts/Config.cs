using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Config {

    //! Soldier > MeleeSoldier
    public static readonly int[] SOLDIER_MELEE_MAX_HEALTH = new int[]{100, 1500, 20000, 250000};
    public static readonly int[] SOLDIER_MELEE_STRENGTH = new int[]{100, 1500, 20000, 250000};
    public static readonly int SOLDIER_MELEE_UNLOCK_COST = 0;
    //! Soldier > RangedSoldier
    public static readonly int[] SOLDIER_RANGED_MAX_HEALTH = new int[]{80, 1000, 12000, 150000};
    public static readonly int[] SOLDIER_RANGED_SHOOTING_DAMAGE = new int[]{50, 600, 7000, 80000};
    public static readonly int[] SOLDIER_RANGED_SCAN_RANGE = new int[]{20, 22, 25, 30};
    public static readonly int[] SOLDIER_RANGED_SHOOTING_RATIO = new int[]{2, 3, 4, 5};
    public static readonly float SOLDIER_RANGED_SCAN_TIME = 0.1f;
    public static readonly int SOLDIER_RANGED_UNLOCK_COST = 1; //1000
    //! Soldier > RangedSoldier > GunnerSoldier
    public static readonly int[] SOLDIER_GUNNER_MAX_HEALTH = new int[]{80, 1000, 12000, 150000};
    public static readonly int[] SOLDIER_GUNNER_SHOOTING_DAMAGE = new int[]{30, 350, 4000, 50000};
    public static readonly int[] SOLDIER_GUNNER_SCAN_RANGE = new int[]{20, 22, 25, 30};
    public static readonly int[] SOLDIER_GUNNER_SHOOTING_RATIO = new int[]{4, 6, 8, 10};
    public static readonly float SOLDIER_GUNNER_SCAN_TIME = 0.1f;
    public static readonly int SOLDIER_GUNNER_UNLOCK_COST = 20; //20000
    //! Soldier > RangedSoldier > SniperSoldier
    public static readonly int[] SOLDIER_SNIPER_MAX_HEALTH = new int[]{80, 1000, 12000, 150000};
    public static readonly int[] SOLDIER_SNIPER_SHOOTING_DAMAGE = new int[]{150, 1800, 21000, 250000};
    public static readonly int[] SOLDIER_SNIPER_SCAN_RANGE = new int[]{30, 35, 40, 45};
    public static readonly int[] SOLDIER_SNIPER_SHOOTING_RATIO = new int[]{1, 1, 2, 2};
    public static readonly float SOLDIER_SNIPER_SCAN_TIME = 0.1f;
    public static readonly int SOLDIER_SNIPER_UNLOCK_COST = 30; //30000
    //! Costs
    public static Dictionary<Progress.UnlockCode, int> UNLOCK_COST; //TODO find best access control
    public static Dictionary<Progress.UnlockCode, string> UNLOCK_DESC; //TODO find best access control
    //! Bullet
    public static readonly float BULLET_VELOCITY = 30;
    public static readonly float SNIPER_BULLET_VELOCITY = 50;
    public static readonly float GUNNER_BULLET_VELOCITY = 20;
    public static readonly Vector3 BULLET_SCALE = Vector3.one * 0.25f;
    public static readonly Vector3 SNIPER_BULLET_SCALE = Vector3.one * 0.4f;
    public static readonly Vector3 GUNNER_BULLET_SCALE = Vector3.one * 0.15f;
    //! Bonus
    public static readonly float BONUS_THRESHOLD_DAMAGED = 0.75f;
    //! Gameplay
    public static readonly int GAME_INIT_PLAYER_MELEE = 10;
    public static readonly int GAME_ENEMY_LIMIT = 40;
    //! Merge
    public static readonly int MERGE_LEVEL_LIMIT = 4;
    public static readonly int MERGE_COUNT_PER_LEVEL = 10;
    //! Wave
    public static readonly float WAVE_TIME_ENEMY = 2f;
    public static readonly float WAVE_TIME_BONUS = 2f;

    //! Waypoint (tech)
    public static readonly string WAYPOINT_NAME = "Player Army Waypoint";
    public static readonly string WAYPOINT_MELEE_NAME = "Player Soldier Melee Waypoint";
    public static readonly string WAYPOINT_RANGED_NAME = "Player Soldier Ranged Waypoint";
    public static readonly float WAYPOINT_VELOCITY = 10;
    public static readonly float WAYPOINT_RECALL_VELOCITY = 2;
    //! Pool (tech)
    public static readonly int POOL_INIT_SIZE = 4;
    //! World (tech)
    public static readonly float WORLD_SCROLL_VELOCITY = 10;
    public static readonly float WORLD_BOUND_PLAYER_X = 10;
    public static readonly float WORLD_BOUND_PLAYER_Z = 4;
    public static readonly float WORLD_BOUND_X = 20;
    public static readonly float WORLD_BOUND_Y_DOWN = -20;
    public static readonly float WORLD_BOUND_Y_UP = 10;
    public static readonly float WORLD_BOUND_Z_BACK = -10;
    public static readonly float WORLD_BOUND_Z_FORWARD = 100;
    public static readonly float WORLD_ROAD_BOUND_X = 10;
    public static readonly float WORLD_ROAD_Y = 0;
    public static readonly Vector3 WORLD_SPAWN_POSITION_ENEMY = new Vector3(0, 0, 80);
    public static readonly Vector3 WORLD_SPAWN_POSITION_PLAYER = new Vector3(0, 0, 0);
    //! Generic (tech)
    public static readonly float EPS = 1e-3f;
    public static readonly float INF = 1e9f;
    //! Tags (tech)
    public static readonly string TAG_ENEMY = "Enemy";
    public static readonly string TAG_PLAYER = "Player";
    public static readonly string TAG_DEFAULT = "Untagged";
    //! Saves (tech)
    public static readonly string FILE_PROGRESS = "/progress.json";

    public static void Initialize() {
        // Unlock cost
        UNLOCK_COST = new Dictionary<Progress.UnlockCode, int>();
        UNLOCK_COST.Add(Progress.UnlockCode.soldierMelee, SOLDIER_MELEE_UNLOCK_COST);
        UNLOCK_COST.Add(Progress.UnlockCode.soldierRanged, SOLDIER_RANGED_UNLOCK_COST);
        UNLOCK_COST.Add(Progress.UnlockCode.soldierGunner, SOLDIER_GUNNER_UNLOCK_COST);
        UNLOCK_COST.Add(Progress.UnlockCode.soldierSniper, SOLDIER_SNIPER_UNLOCK_COST);
        // Unlock desc
        UNLOCK_DESC = new Dictionary<Progress.UnlockCode, string>();
        UNLOCK_DESC.Add(Progress.UnlockCode.soldierMelee, "Melee soldier");
        UNLOCK_DESC.Add(Progress.UnlockCode.soldierRanged, "Ranged soldier");
        UNLOCK_DESC.Add(Progress.UnlockCode.soldierGunner, "Gunner");
        UNLOCK_DESC.Add(Progress.UnlockCode.soldierSniper, "Sniper");
    }
}
