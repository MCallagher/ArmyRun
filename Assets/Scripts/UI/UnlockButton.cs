using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UnlockButton : MonoBehaviour {

    //! Variable
    [SerializeField] private Progress.UnlockCode code;

    //! References
    private Button button;
    private Image image;
    private TextMeshProUGUI text;


    //! MonoBehaviour
    void Awake() {
        button = GetComponent<Button>();
        image = GetComponent<Image>();
        text = GetComponentInChildren<TextMeshProUGUI>();
    }

    void Update() {
        bool unlocked = Progress.instance.IsUnlocked(code);
        if(unlocked) {
            button.enabled = false;
            image.color = new Color(0.50f, 0.92f, 0.41f);
            text.text = $"{Config.UNLOCK_DESC[(int)code]}";
        }
        else {
            if(Progress.instance.GetStones() >= Config.UNLOCK_COST[(int)code]) {
                button.enabled = true;
                image.color = new Color(1, 1, 1);
            }
            else {
                button.enabled = false;
                image.color = new Color(0.92f, 0.50f, 0.50f);
            }
            text.text = $"{Config.UNLOCK_DESC[(int)code]} - {Config.UNLOCK_COST[(int)code]}";
        }
    }
}
