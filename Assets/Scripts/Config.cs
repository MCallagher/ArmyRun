using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Config
{
    //! Soldier
    public static readonly float SOLDIER_SIDE_VELOCITY = 10;
    //! Soldier > MeleeSoldier
    public static readonly int SOLDIER_MELEE_HEALTH = 100;
    public static readonly int SOLDIER_MELEE_DAMAGE = 100;
    //! Gameplay
    public static readonly float GAME_WAVE_TIME_ENEMY = 2;
    public static readonly float GAME_WAVE_TIME_BONUS = 2;
    public static readonly Vector3 GAME_SPAWN_POSITION = new Vector3(0, 0, 50);
    //! World
    public static readonly float WORLD_SCROLL_VELOCITY = 10;
    public static readonly float WORLD_BOUND_Y_DOWN = -20;
    public static readonly float WORLD_BOUND_Z_BACK = -5;
    //! World > Road
    public static readonly float WORLD_ROAD_BOUND_X = 5;
    public static readonly float WORLD_ROAD_Y = 0;
    //! Generic
    public static readonly float EPS = 1e-3f;
}
