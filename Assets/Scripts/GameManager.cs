using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour {

    //! Static variables
    public static GameManager instance;

    //! Variables
    [SerializeField] private int wave;
    [SerializeField] private bool gameOver;


    //! Properties
    public int Wave {
        get {
            return wave;
        }
        private set {
            wave = value;
        }
    }

    public bool GameOver {
        get {
            return gameOver;
        }
        private set {
            gameOver = value;
        }
    }

    //! References
    [SerializeField] private TextMeshProUGUI waveText;
    [SerializeField] private GameObject gameOverScreen;


    //! Monobehaviour
    void Awake() {
        if (instance != null) {
            Destroy(this.gameObject);
            return;
        }
        instance = this;
    }

    void Start() {
        InitializeGame();
        StartCoroutine(Waves());
    }

    void Update() {
        waveText.text = "Wave: " + Wave;
        if(IsGameOver()) {
            gameOver = true;
            gameOverScreen.SetActive(true);
        }
    }


    //! Game manager - Public
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

    //! GameManager - Private
    private void InitializeGame() {
        Wave = 1;
        GameOver = false;
        AddSoldierGroup<MeleeSoldier>(Config.GAME_INIT_PLAYER_MELEE, false);
    }

    private IEnumerator Waves() {
        while (!GameOver) {
            int waveValue = Mathf.Max((int)(Mathf.Pow((float)Wave, 1.1f)), 1);
            AddSoldierGroup<MeleeSoldier>(waveValue, true);
            AddSoldierGroup<RangedSoldier>(waveValue / 4, true);
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

    private bool IsGameOver() {
        foreach (GameObject soldierObject in PoolManager.instance.GetActiveGameObject<Soldier>()) {
            Soldier soldier = soldierObject.GetComponent<Soldier>();
            if (!soldier.Enemy) {
                return false;
            }
        }
        return true;
    }

    public void AddSoldierGroup<T>(int numOfSoldiers, bool enemy) where T : Soldier {
        int soldiersLeft = numOfSoldiers;
        int level = 0;
        int levelEquivalent = 1;
        while (soldiersLeft > 0) {
            while(soldiersLeft % (levelEquivalent * Config.MERGE_COUNT_PER_LEVEL) > 0) {
                AddSoldier<T>(level, enemy);
                soldiersLeft -= levelEquivalent;
            }
            level++;
            levelEquivalent *= Config.MERGE_COUNT_PER_LEVEL;
        }
        if(!enemy) {
            AutoMerge<T>();
        }
    }

    private GameObject AddSoldier<T>(int level, bool enemy) where T : Soldier {
        // Get soldier
        GameObject soldierObject = PoolManager.instance.GetEntity<T>();
        Soldier soldier = soldierObject.GetComponent<Soldier>();

        // Compute position
        Vector3 center = enemy ? Config.WORLD_SPAWN_POSITION_ENEMY : Soldier.Waypoint.transform.position;
        float radius = enemy ? Config.WORLD_ROAD_BOUND_X : Config.WORLD_ROAD_BOUND_X / 10;
        float height = soldierObject.GetComponent<Renderer>().bounds.size.y / 2;
        Vector3 position = AdvancedRandom.PositionOnDisk(center, radius, height);

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
