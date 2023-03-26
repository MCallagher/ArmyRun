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
    [SerializeField] private TextMeshProUGUI diamondText;
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
    }

    void Update() {
        waveText.text = "Wave: " + ArmyManager.instance.Wave;
        diamondText.text = "Diamonds: " + Progress.instance.GetStones();
        if(!gameOver && IsGameOver()) {
            gameOver = true;
            gameOverScreen.SetActive(true);
        }
    }


    //! GameManager - Private
    private void InitializeGame() {
        GameOver = false;
        ArmyManager.instance.InitializeArmy();
        ArmyManager.instance.InitializeWaves();
    }

    private bool IsGameOver() {
        foreach (GameObject soldierObject in PoolManager.instance.GetActiveGameObject<Soldier>()) {
            Soldier soldier = soldierObject.GetComponent<Soldier>();
            if (!soldier.Enemy) {
                return false;
            }
        }
        Progress.instance.Save();
        return true;
    }
}
