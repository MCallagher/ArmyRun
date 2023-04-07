using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour {

    //! Static variables
    public static GameManager instance;

    //! Variables
    [SerializeField] private int stones;
    [SerializeField] private bool gameOver;


    //! Properties
    public int Stones {
        get {
            return stones;
        }
        private set {
            stones = Mathf.Max(0, value);
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
    [SerializeField] private TextMeshProUGUI diamondText;
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private TextMeshProUGUI gameOverWaveText;
    [SerializeField] private TextMeshProUGUI gameOverDiamondText;


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
        diamondText.text = "Diamonds: " + GameManager.instance.Stones;
        if(!gameOver && IsGameOver()) {
            gameOver = true;
            gameOverScreen.SetActive(true);
            gameOverWaveText.text = "Wave: " + ArmyManager.instance.Wave;
            gameOverDiamondText.text = "Diamonds: " + GameManager.instance.Stones;
        }
    }


    //! GameManager - Public
    public void AddStones(int stones) {
        Stones += stones;
    }

    //! GameManager - Private
    private void InitializeGame() {
        GameOver = false;
        Stones = 0;
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
        // Game over
        Progress.instance.AddStones(Stones);
        Progress.instance.Save();
        return true;
    }
}
