using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Config
{
    //! Soldier
    public static readonly float SOLDIER_SIDE_VELOCITY = 10;
    public static readonly float SOLDIER_LEVEL_INCREASE = 1.07f;
    //! Soldier > MeleeSoldier
    public static readonly int SOLDIER_MELEE_CONSTITUTION = 100;
    public static readonly int SOLDIER_MELEE_STRENGTH = 100;
    //! Soldier > RangedSoldier
    public static readonly int SOLDIER_RANGED_CONSTITUTION = 100;
    public static readonly int SOLDIER_RANGED_STRENGTH = 50;
    public static readonly int SOLDIER_RANGED_RANGE = 20;
    public static readonly int SOLDIER_RANGED_RATIO = 2;
    public static readonly float SOLDIER_RANGED_REFRESH_TIME = 0.1f;
    //! Bullet
    public static readonly float BULLET_VELOCITY = 30;
    //! Bonus
    public static readonly float BONUS_THRESHOLD_DAMAGED = 0.75f;
    public static readonly int BONUS_THRESHOLD_CROWDED = 4;
    //! Pool
    public static readonly int POOL_INIT_SIZE = 4;
    //! Gameplay
    public static readonly int GAME_INIT_PLAYER_MELEE = 10;
    public static readonly float GAME_WAVE_TIME_ENEMY = 1.5f;
    public static readonly float GAME_WAVE_TIME_BONUS = 1.5f;
    public static readonly Vector3 GAME_SPAWN_POSITION_ENEMY = new Vector3(0, 0, 80);
    public static readonly Vector3 GAME_SPAWN_POSITION_PLAYER = new Vector3(0, 0, 0);
    //! World
    public static readonly float WORLD_SCROLL_VELOCITY = 15;
    public static readonly float WORLD_BOUND_PLAYER_Z = 4;
    public static readonly float WORLD_BOUND_X = 20;
    public static readonly float WORLD_BOUND_Y_DOWN = -20;
    public static readonly float WORLD_BOUND_Y_UP = 10;
    public static readonly float WORLD_BOUND_Z_BACK = -10;
    public static readonly float WORLD_BOUND_Z_FORWARD = 100;
    //! World > Road
    public static readonly float WORLD_ROAD_BOUND_X = 10;
    public static readonly float WORLD_ROAD_Y = 0;
    //! Generic
    public static readonly float EPS = 1e-3f;
    public static readonly float INF = 1e9f;
    public static readonly string TAG_ENEMY = "Enemy";
    public static readonly string TAG_PLAYER = "Player";
    public static readonly string TAG_DEFAULT = "Untagged";
}
