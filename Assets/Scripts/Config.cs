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
    public static readonly int SOLDIER_MELEE_COINS = 10;
    //! Bonus
    public static readonly List<float> WALL_BONUS_DISTRIBUTION = new List<float>(new float[]{0.7f, 0.2f, 0.1f});
    public static readonly int BONUS_MELEE_COST = 5;
    public static readonly int BONUS_HEAL_COST = 1;
    public static readonly int BONUS_MERGE_COST = 2;
    //! Pool
    public static readonly int POOL_INIT_SIZE = 4;
    //! Gameplay
    public static readonly int GAME_INIT_PLAYER_MELEE = 10;
    public static readonly float GAME_WAVE_TIME_ENEMY = 2;
    public static readonly float GAME_WAVE_TIME_BONUS = 2;
    public static readonly Vector3 GAME_SPAWN_POSITION_ENEMY = new Vector3(0, 0, 50);
    public static readonly Vector3 GAME_SPAWN_POSITION_PLAYER = new Vector3(0, 0, 0);
    //! World
    public static readonly float WORLD_SCROLL_VELOCITY = 15;
    public static readonly float WORLD_BOUND_Y_DOWN = -20;
    public static readonly float WORLD_BOUND_Z_BACK = -5;
    //! World > Road
    public static readonly float WORLD_ROAD_BOUND_X = 5;
    public static readonly float WORLD_ROAD_Y = 0;
    //! Generic
    public static readonly float EPS = 1e-3f;
    public static readonly string TAG_ENEMY = "Enemy";
    public static readonly string TAG_PLAYER = "Player";
    public static readonly string TAG_DEFAULT = "Untagged";
}
