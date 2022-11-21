using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    [SerializeField] private GameObject playerSoldierPrefab;
    [SerializeField] private GameObject enemySoldierPrefab;
    [SerializeField] private GameObject bonusWallPrefab;

    [SerializeField] private int wave = 1;
    public GameManager instance;

    void Awake() {
        if (instance == null) {
            instance = this;
        }
        else {
            Destroy(this.gameObject);
        }
    }

    void Start() {
        StartCoroutine(Waves());
    }

    void Update() {
        
    }

    private IEnumerator Waves() {
        while (true) {
            GenerateEnemyWave(wave);
            yield return new WaitForSeconds(Config.GAME_WAVE_TIME_ENEMY);
            GenerateBonusWall(wave);
            yield return new WaitForSeconds(Config.GAME_WAVE_TIME_BONUS);
            wave++;
        }
    }

    private void GenerateEnemyWave(int wave) {
        int numOfEnemies = wave;
        for (int i=0; i < numOfEnemies; i++) {
            float theta = Random.Range(0, 2 * Mathf.PI);
            float radius = Random.Range(0f, Config.WORLD_ROAD_BOUND_X);
            float height = enemySoldierPrefab.GetComponent<Renderer>().bounds.size.y;
            Vector3 offset = new Vector3(radius * Mathf.Sin(theta), height / 2, radius * Mathf.Cos(theta));
            Instantiate(enemySoldierPrefab, Config.GAME_SPAWN_POSITION + offset, enemySoldierPrefab.transform.rotation);
        }
    }

    private void GenerateBonusWall(int wave) {
        Instantiate(bonusWallPrefab, Config.GAME_SPAWN_POSITION, bonusWallPrefab.transform.rotation);
    }
}
