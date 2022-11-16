using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject playerSoldierPrefab;
    [SerializeField] private GameObject enemySoldierPrefab;
    [SerializeField] private GameObject bonusWallPrefab;

    private static float enemyWaitTime = 4;
    private static float bonusWaitTime = 4;
    private static Vector3 enemySpawn = new Vector3(0, 1, 50);
    private static Vector3 wallSpawn = new Vector3(0, 0, 50);
    private static float radiusRange = 5;

    [SerializeField] private int wave = 1;

    void Start()
    {
        StartCoroutine(Waves());
    }

    void Update()
    {
        
    }

    private IEnumerator Waves() {
        while (true) {
            yield return new WaitForSeconds(enemyWaitTime);
            GenerateEnemyWave(wave);
            yield return new WaitForSeconds(bonusWaitTime);
            GenerateBonusWall(wave);
            wave++;
        }
    }

    private void GenerateEnemyWave(int wave) {
        int numOfEnemies = wave;
        for (int i=0; i < numOfEnemies; i++) {
            float theta = Random.Range(0, 2 * Mathf.PI);
            float radius = Random.Range(0f, radiusRange);
            Vector3 offset = new Vector3(radius * Mathf.Sin(theta), 0, radius * Mathf.Cos(theta));
            Instantiate(enemySoldierPrefab, enemySpawn + offset, enemySoldierPrefab.transform.rotation);
        }
    }

    private void GenerateBonusWall(int wave) {
        Instantiate(bonusWallPrefab, wallSpawn, bonusWallPrefab.transform.rotation);
    }
}
