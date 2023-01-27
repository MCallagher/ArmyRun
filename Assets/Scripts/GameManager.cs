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


    //! Monobehaviour
    void Awake() {
        if (instance == null) {
            instance = this;
        }
        else {
            Destroy(this.gameObject);
        }
    }

    void Start() {
        InitializeGame();
        StartCoroutine(Waves());
    }

    void Update() {
        waveText.text = "Wave: " + Wave;
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
        AddSoldier<MeleeSoldier>(Config.GAME_INIT_PLAYER_MELEE, false);
    }

    private IEnumerator Waves() {
        while (!GameOver) {
            int waveValue = Mathf.Max((int)(Mathf.Pow((float)Wave, 1.1f)), 1);
            AddSoldierGroup<MeleeSoldier>(waveValue, true);
            AddSoldierGroup<RangedSoldier>(waveValue / 4, true);
            yield return new WaitForSeconds(Config.GAME_WAVE_TIME_ENEMY);
            GenerateBonusWall();
            yield return new WaitForSeconds(Config.GAME_WAVE_TIME_BONUS);
            Wave++;
        }
    }

    private void GenerateBonusWall() {
        GameObject newWall = PoolManager.instance.GetEntity<Wall>();
        newWall.transform.position = Config.GAME_SPAWN_POSITION_ENEMY;
        newWall.GetComponent<Wall>().InitializeWall();
    }

    private bool IsGameOver() {
        foreach (Soldier soldier in PoolManager.instance.GetActiveEntities<Soldier>()) {
            if (!soldier.Enemy) {
                return false;
            }
        }
        return true;
    }

    private Vector3 GetPlayerArmyCenter() {
        Vector3 center = Vector3.zero;
        int count = 0;
        foreach (GameObject soldiers in PoolManager.instance.GetActiveGameObject<Soldier>()) {
            if (!soldiers.GetComponent<Soldier>().Enemy) {
                center += soldiers.transform.position;
                count++;
            }
        }
        if (count == 0) {
            return Config.GAME_SPAWN_POSITION_PLAYER;
        }
        return center / count;
    }

    public void AddSoldierGroup<T>(int numOfSoldiers, bool enemy) where T : Soldier {
        int soldiersLeft = numOfSoldiers;
        int count = 1;
        while (soldiersLeft > 0) {
            while(soldiersLeft % (count * 10) > 0) {
                AddSoldier<T>(count, enemy);
                soldiersLeft -= count;
            }
            count *= 10;
        }
        if(!enemy) {
            AutoMerge<T>();
        }
    }

    private GameObject AddSoldier<T>(int count, bool enemy) where T : Soldier {
        // Get soldier
        GameObject soldierObject = PoolManager.instance.GetEntity<T>();
        Soldier soldier = soldierObject.GetComponent<Soldier>();
        // Compute position
        Vector3 center = enemy ? Config.GAME_SPAWN_POSITION_ENEMY : GetPlayerArmyCenter();
        float radius = enemy ? Config.WORLD_ROAD_BOUND_X : Config.WORLD_ROAD_BOUND_X / 10;
        float height = soldierObject.GetComponent<Renderer>().bounds.size.y / 2;
        Vector3 position = AdvancedRandom.PositionOnDisk(center, radius, height);
        // Setup soldier
        soldierObject.transform.position = position;
        soldierObject.tag = enemy ? Config.TAG_ENEMY : Config.TAG_PLAYER;
        soldier.InitializeSoldier(count, enemy);
        // Return soldier
        return soldierObject;
    }

    private void AutoMerge<T>() where T : Soldier {
        // Initialize dict to count soldiers
        Dictionary<int, List<T>> soldiersOfSize = new Dictionary<int, List<T>>();
        for(int size = 1; size <= Config.GAME_MERGE_LIMIT; size *= 10) {
            soldiersOfSize[size] = new List<T>();
        }
        // Analyse soldiers
        foreach (T soldier in PoolManager.instance.GetActiveEntities<T>()) {
            if(!soldier.Enemy && soldier.Count <= Config.GAME_MERGE_LIMIT) {
                soldiersOfSize[soldier.Count].Add(soldier);
            }
        }
        // Replace small 
        for(int size = 1; size <= Config.GAME_MERGE_LIMIT; size *= 10) {
            while(soldiersOfSize[size].Count >= 10) {
                for(int i = 0; i < 10; i++) {
                    T oldSoldier = soldiersOfSize[size][0];
                    soldiersOfSize[size].RemoveAt(0);
                    oldSoldier.gameObject.SetActive(false);
                }
                if(size * 10 <= Config.GAME_MERGE_LIMIT) {
                    GameObject newSoldier = AddSoldier<T>(size * 10, false);
                    soldiersOfSize[size * 10].Add(newSoldier.GetComponent<T>());
                }
            }
        }
    }
}
