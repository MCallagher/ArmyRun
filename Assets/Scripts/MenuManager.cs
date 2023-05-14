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
            GameObject.Find("Stones Text").GetComponent<TextMeshProUGUI>().text = $"Stones: {Progress.instance.GetStones()}";
        }
    }

    //! MenuManager - Public - Actions
    public void SaveOptions() {
        GameObject.Find("Sounds Slider").GetComponent<SettingSlider>().SaveOption();
        GameObject.Find("Music Slider").GetComponent<SettingSlider>().SaveOption();
        GameObject.Find("Effects Slider").GetComponent<SettingSlider>().SaveOption();
        Options.instance.Save();
        EventManager.TriggerUpdatedOptions();
        LoadMainMenu();
    }

    public void QuitGame() {
        Application.Quit();
    }

    //! MenuManager - Public - Scenes
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
        SceneManager.LoadScene("Options Menu");
    }
}
