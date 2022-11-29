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
    //! Pool
    public static readonly int SOLDIER_MELEE_POOL = 10;
    public static readonly int WALL_POOL = 2;
    //! Gameplay
    public static readonly int GAME_INIT_PLAYER_MELEE = 10;
    public static readonly float GAME_WAVE_TIME_ENEMY = 2;
    public static readonly float GAME_WAVE_TIME_BONUS = 2;
    public static readonly Vector3 GAME_SPAWN_POSITION_ENEMY = new Vector3(0, 0, 50);
    public static readonly Vector3 GAME_SPAWN_POSITION_PLAYER = new Vector3(0, 0, 0);
    //! World
    public static readonly float WORLD_SCROLL_VELOCITY = 10;
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
