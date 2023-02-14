using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {

    void Start() {
        if(SceneManager.GetActiveScene().name == "Powerups Menu") {
            GameObject.Find("Unlock Melee Button").GetComponent<Button>().onClick.AddListener(() => UnlockPowerup(Progress.UnlockCode.soldierMelee));
            GameObject.Find("Unlock Ranged Button").GetComponent<Button>().onClick.AddListener(() => UnlockPowerup(Progress.UnlockCode.soldierRanged));
        }
    }

    //! MenuManager - Public
    public void StartNewGame() {
        SceneManager.LoadScene("Playmode");
    }

    public void LoadMainMenu() {
        SceneManager.LoadScene("Main Menu");
    }

    public void LoadPowerupMenu() {
        SceneManager.LoadScene("Powerups Menu");
    }

    public void LoadOptionsMenu() {
        throw new System.NotImplementedException();
    }

    public void QuitGame() {
        Application.Quit();
    }

    public void UnlockPowerup(Progress.UnlockCode code) {
        Debug.Log(code);
    }
}
