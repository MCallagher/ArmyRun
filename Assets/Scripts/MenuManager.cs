using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuManager : MonoBehaviour {

    void Start() {
        if(SceneManager.GetActiveScene().name == "Powerups Menu") {
            GameObject.Find("Unlock Melee Button").GetComponent<Button>().onClick.AddListener(() => Progress.instance.Unlock(Progress.UnlockCode.soldierMelee));
            GameObject.Find("Unlock Ranged Button").GetComponent<Button>().onClick.AddListener(() => Progress.instance.Unlock(Progress.UnlockCode.soldierRanged));
            GameObject.Find("Unlock Gunner Button").GetComponent<Button>().onClick.AddListener(() => Progress.instance.Unlock(Progress.UnlockCode.soldierGunner));
            GameObject.Find("Unlock Sniper Button").GetComponent<Button>().onClick.AddListener(() => Progress.instance.Unlock(Progress.UnlockCode.soldierSniper));
            GameObject.Find("Unlock Bumper Button").GetComponent<Button>().onClick.AddListener(() => Progress.instance.Unlock(Progress.UnlockCode.soldierBumper));
            GameObject.Find("Unlock Kamikaze Button").GetComponent<Button>().onClick.AddListener(() => Progress.instance.Unlock(Progress.UnlockCode.soldierKamikaze));
        }
    }

    void Update() {
        if(SceneManager.GetActiveScene().name == "Powerups Menu") {
            GameObject.Find("Diamonds Text").GetComponent<TextMeshProUGUI>().text = $"Diamonds: {Progress.instance.GetDiamonds()}";
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
}
