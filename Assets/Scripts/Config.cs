using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Config {

    //! Soldier > MeleeSoldier
    public static readonly int SOLDIER_MELEE_MAX_HEALTH = 100;
    public static readonly int SOLDIER_MELEE_STRENGTH = 100;
    //! Soldier > RangedSoldier
    public static readonly int SOLDIER_RANGED_MAX_HEALTH = 100;
    public static readonly int SOLDIER_RANGED_SHOOTING_DAMAGE = 50;
    public static readonly int SOLDIER_RANGED_SCAN_RANGE = 20;
    public static readonly int SOLDIER_RANGED_SHOOTING_RATIO = 2;
    public static readonly float SOLDIER_RANGED_SCAN_TIME = 0.1f;
    //! Bullet
    public static readonly float BULLET_VELOCITY = 30;
    //! Bonus
    public static readonly float BONUS_THRESHOLD_DAMAGED = 0.75f;
    //! Gameplay
    public static readonly int GAME_INIT_PLAYER_MELEE = 10;
    //! Merge
    public static readonly int MERGE_LEVEL_LIMIT = 5;
    public static readonly int MERGE_COUNT_PER_LEVEL = 10;
    //! Wave
    public static readonly float WAVE_TIME_ENEMY = 1.5f;
    public static readonly float WAVE_TIME_BONUS = 1.5f;

    //! Waypoint (tech)
    public static readonly string WAYPOINT_NAME = "Player Army Waypoint";
    public static readonly float WAYPOINT_VELOCITY = 10;
    public static readonly float WAYPOINT_RECALL_VELOCITY = 2;
    //! Pool (tech)
    public static readonly int POOL_INIT_SIZE = 4;
    //! World (tech)
    public static readonly float WORLD_SCROLL_VELOCITY = 15;
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
    //! Generic (tech)
    public static readonly float EPS = 1e-3f;
    public static readonly float INF = 1e9f;
    //! Tags (tech)
    public static readonly string TAG_ENEMY = "Enemy";
    public static readonly string TAG_PLAYER = "Player";
    public static readonly string TAG_DEFAULT = "Untagged";
    //! Saves (tech)
    public static readonly string FILE_PROGRESS = "/progress.json";
}
