using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnlockButton : MonoBehaviour {

    //! Variable
    [SerializeField] private Progress.UnlockCode code;

    //! References
    private Button button;
    private Image image;


    //! MonoBehaviour
    void Awake() {
        button = GetComponent<Button>();
        image = GetComponent<Image>();
    }

    void Start() {
        bool unlocked = Progress.instance.IsUnlocked(code);
        button.enabled = !unlocked;
        image.color = unlocked ? new Color(0.50f, 0.92f, 0.41f) : new Color(1, 1, 1);
    }
}
