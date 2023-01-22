using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour {

    //! Static variables
    public static GameManager instance;

    //! Variables
    [SerializeField] private int wave;

    //! Properties
    public int Wave {
        get {
            return wave;
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
        int soldiersLeft = numOfSoldiers;
        while (soldiersLeft > 0) {
            int added = Random.Range(1, soldiersLeft + 1);
            AddSoldier<T>(added, false);
            soldiersLeft -= added;
        }
    }

    public void BonusHeal() {
        foreach (GameObject soldierObject in PoolManager.instance.GetActiveGameObject<Soldier>()) {
            Soldier soldier = soldierObject.GetComponent<Soldier>();
            if (!soldier.Enemy) {
                soldier.Health = soldier.Constitution;
            }
        }
    }


    //! GameManager - Private
    private void InitializeGame() {
        wave = 1;
        AddSoldier<MeleeSoldier>(Config.GAME_INIT_PLAYER_MELEE, false);
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

    private IEnumerator Waves() {
        while (true) {
            int waveValue = Mathf.Max((int)(Mathf.Pow((float)wave, 1.1f)), 1);
            GenerateEnemyWave<MeleeSoldier>(waveValue);
            GenerateEnemyWave<RangedSoldier>(waveValue / 4);
            yield return new WaitForSeconds(Config.GAME_WAVE_TIME_ENEMY);
            GenerateBonusWall();
            yield return new WaitForSeconds(Config.GAME_WAVE_TIME_BONUS);
            wave++;
        }
    }

    private void GenerateEnemyWave<T>(int numOfSoldiers) where T : Soldier{
        int soldiersLeft = numOfSoldiers;
        while (soldiersLeft > 0) {
            int added = Random.Range(1, soldiersLeft + 1);
            AddSoldier<T>(added, true);
            soldiersLeft -= added;
        }
    }

    private void GenerateBonusWall() {
        GameObject newWall = PoolManager.instance.GetEntity<Wall>();
        newWall.transform.position = Config.GAME_SPAWN_POSITION_ENEMY;
        newWall.GetComponent<Wall>().InitializeWall();
    }

    private void MergeSoldier<T>() where T : Soldier{
        Soldier chosenSoldier = null;
        List<Soldier> mergeSoldiers = new List<Soldier>();
        foreach (Soldier Soldier in PoolManager.instance.GetActiveEntities<T>()) {
            if (!Soldier.Enemy) {
                if (chosenSoldier == null) {
                    chosenSoldier = Soldier;
                }
                else {
                    mergeSoldiers.Add(Soldier);
                }
            }
        }
        if (chosenSoldier != null) {
            chosenSoldier.Merge(mergeSoldiers);
        }
    }

    private void AddSoldier<T>(int count, bool enemy) where T : Soldier {
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
    }
}
