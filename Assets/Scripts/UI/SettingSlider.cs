using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingSlider : MonoBehaviour {

    //! Variable
    [SerializeField] private Options.OptionCode code;

    //! References
    private Slider slider;


    //! MonoBehaviour
    void Awake() {
        slider = GetComponent<Slider>();
    }

    void Start() {
        LoadOption();
    }


    //! SettingSlider - Public
    public void SaveOption() {
        Options.instance.SaveOption(code, slider.value);
    }

    public void LoadOption() {
        slider.value = Options.instance.LoadOption(code);
    }
}
