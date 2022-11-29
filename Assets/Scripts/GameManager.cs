using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    //! Static variables
    public static GameManager instance;

    //! Variables
    [SerializeField] private int wave = 1;

    //! References
    [SerializeField] private GameObject meleeSoldierPrefab;
    [SerializeField] private GameObject bonusWallPrefab;
    [SerializeField] private GameObject soldierHierarchy;


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
        GeneratePlayerArmy(Config.GAME_INIT_PLAYER_MELEE);
        StartCoroutine(Waves());
    }

    void Update() {
        
    }

    public void BonusExtraArmy(int numOfMeleeSoldiers) {
        GeneratePlayerArmy(numOfMeleeSoldiers);
    }

    public void BonusHeal(int healingPrc) {
        foreach (GameObject obj in Pool.instance.GetActiveMeleeSoldiers()) {
            Soldier soldier = obj.GetComponent<Soldier>();
            if (!soldier.Enemy) {
                soldier.Health += (int)(soldier.Constitution * healingPrc / 100);
            }
        }
    }

    public void BounsMerge() {
        MeleeSoldier chosenMeleeSoldier = null;
        List<MeleeSoldier> mergeMeleeSoldiers = new List<MeleeSoldier>();
        foreach (GameObject obj in Pool.instance.GetActiveMeleeSoldiers()) {
            MeleeSoldier meleeSoldier = obj.GetComponent<MeleeSoldier>();
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
    }

    //! GameManager - Private
    private void GeneratePlayerArmy(int numOfMeleeSoldiers) {
        GameObject newSoldier = Pool.instance.GetMeleeSoldier();
        newSoldier.transform.position = RandomPosition(Config.GAME_SPAWN_POSITION_PLAYER, Config.WORLD_ROAD_BOUND_X / 2, meleeSoldierPrefab);
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
        //for (int i = 0; i < numOfEnemies; i++) {
        //    GameObject newSoldier = Pool.instance.GetMeleeSoldier();
        //    newSoldier.transform.position = RandomPosition(Config.GAME_SPAWN_POSITION_ENEMY, Config.WORLD_ROAD_BOUND_X, meleeSoldierPrefab);
        //    newSoldier.tag = Config.TAG_ENEMY;
        //    newSoldier.GetComponent<Soldier>().InitializeSoldier(1, true);
        //}
        int enemiesLeft = wave;
        while (enemiesLeft > 0) {
            GameObject newSoldier = Pool.instance.GetMeleeSoldier();
            newSoldier.transform.position = RandomPosition(Config.GAME_SPAWN_POSITION_ENEMY, Config.WORLD_ROAD_BOUND_X, meleeSoldierPrefab);
            newSoldier.tag = Config.TAG_ENEMY;
            int addedEnemies = Random.Range(1, enemiesLeft + 1);
            newSoldier.GetComponent<Soldier>().InitializeSoldier(addedEnemies, true);
            enemiesLeft -= addedEnemies;
        }
    }

    private void GenerateBonusWall() {
        GameObject newWall = Pool.instance.GetWall();
        newWall.transform.position = Config.GAME_SPAWN_POSITION_ENEMY;
        newWall.GetComponent<Wall>().InitializeWall();
    }

    private Vector3 RandomPosition(Vector3 center, float radius, GameObject soldierPrefab) {
        float theta = Random.Range(0, 2 * Mathf.PI);
        float rho = Random.Range(0f, radius);
        float height = soldierPrefab.GetComponent<Renderer>().bounds.size.y;
        Vector3 offset = new Vector3(rho * Mathf.Sin(theta), height / 2, rho * Mathf.Cos(theta));
        return center + offset;
    }
}
