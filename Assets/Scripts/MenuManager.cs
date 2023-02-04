using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {

    //! MenuManager - Public
    public void StartNewGame() {
        SceneManager.LoadScene("Playmode");
    }

    public void LoadMainMenu() {
        SceneManager.LoadScene("Main Menu");
    }

    public void LoadPowerupMenu() {
        throw new System.NotImplementedException();
    }

    public void LoadOptionsMenu() {
        throw new System.NotImplementedException();
    }

    public void QuitGame() {
        Application.Quit();
    }
}
