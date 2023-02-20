using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmyManager : MonoBehaviour {

    //! Static variables
    public static ArmyManager instance;

    //! Variables
    [SerializeField] private int wave;

    //! Properties
    public int Wave {
        get {
            return wave;
        }
        private set {
            wave = value;
        }
    }


    //! Monobehaviour
    void Awake() {
        if (instance != null) {
            Destroy(this.gameObject);
            return;
        }
        instance = this;
    }

    public void InitializeWaves() {
        wave = 1;
        StartCoroutine(Waves());
    }

    public void InitializeArmy() {
        AddSoldierGroup<MeleeSoldier>(Config.GAME_INIT_PLAYER_MELEE, false);
    }


    //! ArmyManager - Public
    public void BonusExtraArmy<T>(int numOfSoldiers) where T : Soldier {
        AddSoldierGroup<T>(numOfSoldiers, false);
    }

    public void BonusHeal() {
        foreach (GameObject soldierObject in PoolManager.instance.GetActiveGameObject<Soldier>()) {
            Soldier soldier = soldierObject.GetComponent<Soldier>();
            if (!soldier.Enemy) {
                soldier.Heal();
            }
        }
    }

    //! ArmyManager - Private
    private IEnumerator Waves() {
        while (!GameManager.instance.GameOver) {
            int waveValue = Mathf.Max((int)(Mathf.Pow((float)Wave, 1.1f)), 1);
            AddSoldierGroup<MeleeSoldier>(2 * waveValue, true);
            //AddSoldierGroup<RangedSoldier>(waveValue / 4, true);
            yield return new WaitForSeconds(Config.WAVE_TIME_ENEMY);
            GenerateBonusWall();
            yield return new WaitForSeconds(Config.WAVE_TIME_BONUS);
            Wave++;
        }
    }

    private void GenerateBonusWall() {
        GameObject newWall = PoolManager.instance.GetEntity<Wall>();
        newWall.transform.position = Config.WORLD_SPAWN_POSITION_ENEMY;
        newWall.GetComponent<Wall>().InitializeWall();
    }


    public void AddSoldierGroup<T>(int numOfSoldiers, bool enemy) where T : Soldier {
        List<int> partition = GetSoldierPartitioning(numOfSoldiers, enemy);
        
        for(int level = 0; level < partition.Count; level++) {
            for(int i = 0; i < partition[level]; i++) {
                AddSoldier<T>(level, enemy);
            }
        }
        
        if(!enemy) {
            AutoMerge<T>();
        }
    }

    private List<int> GetSoldierPartitioning(int numOfSoldiers, bool enemy) {
        if(enemy) {
            return new List<int>(new int[]{numOfSoldiers});
        }
        int radix = Config.MERGE_COUNT_PER_LEVEL;
        List<int> soldiersPerLevel = new List<int>();
        while(numOfSoldiers > 0) {
            soldiersPerLevel.Add(numOfSoldiers % radix);
            numOfSoldiers /= radix;
        }
        return soldiersPerLevel;
    }

    private GameObject AddSoldier<T>(int level, bool enemy) where T : Soldier {
        // Get soldier
        GameObject soldierObject = PoolManager.instance.GetEntity<T>();
        Soldier soldier = soldierObject.GetComponent<Soldier>();

        // Compute position
        Vector3 center = enemy ? Config.WORLD_SPAWN_POSITION_ENEMY : Config.WORLD_SPAWN_POSITION_PLAYER;
        float radius = Config.WORLD_ROAD_BOUND_X / 10;
        float dimensionX = Config.WORLD_ROAD_BOUND_X * 9 / 10;
        float dimensionZ = Config.WORLD_ROAD_BOUND_X * 9 / 10;
        float height = soldierObject.GetComponent<Renderer>().bounds.size.y / 2;
        Vector3 position = Vector3.zero;
        if(enemy) {
            position = AdvancedRandom.PositionOnRect(center, dimensionX, dimensionZ, height);
        }
        else {
            position = AdvancedRandom.PositionOnDisk(center, radius, height);
        }

        // Setup soldier
        soldierObject.transform.position = position;
        soldierObject.tag = enemy ? Config.TAG_ENEMY : Config.TAG_PLAYER;
        soldier.Initialize(level, enemy);
        soldier.Spawn();

        // Return soldier
        return soldierObject;
    }

    private void AutoMerge<T>() where T : Soldier {
        // Initialize dict to count soldiers
        Dictionary<int, List<T>> soldiersOfLevel = new Dictionary<int, List<T>>();
        for(int level = 0; level < Config.MERGE_LEVEL_LIMIT; level++) {
            soldiersOfLevel[level] = new List<T>();
        }

        // Analyse soldiers
        foreach (T soldier in PoolManager.instance.GetActiveEntities<T>()) {
            if(!soldier.Enemy) {
                soldiersOfLevel[soldier.Level].Add(soldier);
            }
        }

        // Replace small
        for(int level = 0; level < Config.MERGE_LEVEL_LIMIT - 1; level++) {
            while(soldiersOfLevel[level].Count >= Config.MERGE_COUNT_PER_LEVEL) {
                for(int i = 0; i < Config.MERGE_COUNT_PER_LEVEL; i++) {
                    T oldSoldier = soldiersOfLevel[level][0];
                    soldiersOfLevel[level].RemoveAt(0);
                    oldSoldier.gameObject.SetActive(false);
                }
                GameObject newSoldier = AddSoldier<T>(level + 1, false);
                soldiersOfLevel[level + 1].Add(newSoldier.GetComponent<T>());
            }
        }
    }
}
