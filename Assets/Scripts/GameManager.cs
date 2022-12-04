using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour {

    //! Static variables
    public static GameManager instance;

    //! Variables
    [SerializeField] private int wave;
    [SerializeField] private int coins;

    //! Properties
    public int Coins {
        get {
            return coins;
        }
        set {
            if (value >= 0) {
                coins = value;
            }
        }
    }

    //! References
    [SerializeField] private GameObject meleeSoldierPrefab;
    [SerializeField] private TextMeshProUGUI coinsText;


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
        GeneratePlayerArmy(Config.GAME_INIT_PLAYER_MELEE);
        StartCoroutine(Waves());
    }

    void Update() {
        coinsText.text = "Coins: " + Coins;
    }

    //! Game manager - Public
    public void BonusExtraArmy(int numOfMeleeSoldiers) {
        GeneratePlayerArmy(numOfMeleeSoldiers);
    }

    public void BonusHeal(int healingPrc) {
        foreach (MeleeSoldier obj in PoolMeleeSoldier.instance.GetActiveEntities()) {
            Soldier soldier = obj.GetComponent<Soldier>();
            if (!soldier.Enemy) {
                soldier.Health += (int)(soldier.Constitution * healingPrc / 100);
            }
        }
    }

    public void BounsMerge() {
        MeleeSoldier chosenMeleeSoldier = null;
        List<MeleeSoldier> mergeMeleeSoldiers = new List<MeleeSoldier>();
        foreach (MeleeSoldier meleeSoldier in PoolMeleeSoldier.instance.GetActiveEntities()) {
            if (!meleeSoldier.Enemy) {
                if (chosenMeleeSoldier == null) {
                    chosenMeleeSoldier = meleeSoldier;
                }
                else {
                    mergeMeleeSoldiers.Add(meleeSoldier);
                }
            }
        }
        if (chosenMeleeSoldier != null) {
            chosenMeleeSoldier.Merge(mergeMeleeSoldiers);
        }
        //
        RangedSoldier chosenRangedSoldier = null;
        List<RangedSoldier> mergeRangedSoldiers = new List<RangedSoldier>();
        foreach (RangedSoldier rangedSoldier in PoolRangedSoldier.instance.GetActiveEntities()) {
            if (!rangedSoldier.Enemy) {
                if (chosenRangedSoldier == null) {
                    chosenRangedSoldier = rangedSoldier;
                }
                else {
                    mergeRangedSoldiers.Add(rangedSoldier);
                }
            }
        }
        if (chosenRangedSoldier != null) {
            chosenRangedSoldier.Merge(mergeRangedSoldiers);
        }
    }

    //! GameManager - Private
    private void InitializeGame() {
        wave = 1;
        coins = 0;
    }

    private Vector3 GetArmyCenter() {
        Vector3 center = Vector3.zero;
        int count = 0;
        foreach (GameObject meleeSoldier in PoolMeleeSoldier.instance.GetActiveGameObject()) {
            if (!meleeSoldier.GetComponent<Soldier>().Enemy) {
                center += meleeSoldier.transform.position;
                count++;
            }
        }
        if (count == 0) {
            return Config.GAME_SPAWN_POSITION_PLAYER;
        }
        return center / count;
    } 

    private void GeneratePlayerArmy(int numOfMeleeSoldiers) {
        GameObject newSoldier = PoolRangedSoldier.instance.GetEntity(); //DEBUG
        //GameObject newSoldier = PoolMeleeSoldier.instance.GetEntity(); //DEBUG
        float height = meleeSoldierPrefab.GetComponent<Renderer>().bounds.size.y;
        Vector3 center = GetArmyCenter();
        center.y = 0;
        newSoldier.transform.position = AdvancedRandom.PositionOnDisk(center, Config.WORLD_ROAD_BOUND_X / 10, height / 2);
        newSoldier.tag = Config.TAG_PLAYER;
        newSoldier.GetComponent<Soldier>().InitializeSoldier(numOfMeleeSoldiers, false);
    }

    private IEnumerator Waves() {
        while (true) {
            GenerateEnemyWave();
            yield return new WaitForSeconds(Config.GAME_WAVE_TIME_ENEMY);
            GenerateBonusWall();
            yield return new WaitForSeconds(Config.GAME_WAVE_TIME_BONUS);
            wave++;
        }
    }

    private void GenerateEnemyWave() {
        int enemiesLeft = wave;
        while (enemiesLeft > 0) {
            GameObject newSoldier = PoolMeleeSoldier.instance.GetEntity();
            float height = meleeSoldierPrefab.GetComponent<Renderer>().bounds.size.y;
            newSoldier.transform.position = AdvancedRandom.PositionOnDisk(Config.GAME_SPAWN_POSITION_ENEMY, Config.WORLD_ROAD_BOUND_X, height / 2);
            newSoldier.tag = Config.TAG_ENEMY;
            int addedEnemies = Random.Range(1, enemiesLeft + 1);
            newSoldier.GetComponent<Soldier>().InitializeSoldier(addedEnemies, true);
            enemiesLeft -= addedEnemies;
        }
    }

    private void GenerateBonusWall() {
        GameObject newWall = PoolWall.instance.GetEntity();
        newWall.transform.position = Config.GAME_SPAWN_POSITION_ENEMY;
        newWall.GetComponent<Wall>().InitializeWall();
    }
}
