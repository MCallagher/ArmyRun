using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Options : MonoBehaviour {

    //! Static variables
    public static Options instance;

    //! Variables
    [SerializeField] private OptionsData data;

    //! Properties
    public float SoundsVolume {
        get {
            return data.soundsVolume;
        }
        set {
            data.soundsVolume = Mathf.Clamp(value, 0, 1);
        }
    }
    
    public float MusicVolume {
        get {
            return data.musicVolume;
        }
        set {
            data.musicVolume = Mathf.Clamp(value, 0, 1);
        }
    }

    public float EffectsVolume {
        get {
            return data.effectsVolume;
        }
        set {
            data.effectsVolume = Mathf.Clamp(value, 0, 1);
        }
    }


    //! MonoBehaviours
    void Awake() {
        if(instance != null) {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Start() {
        Load();
        EventManager.TriggerUpdatedOptions();
    }


    //! Progress - public
    public void SaveOption(OptionCode code, float value) {
        switch (code) {
            case OptionCode.soundsVolume:
                SoundsVolume = value;
                break;
            case OptionCode.musicVolume:
                MusicVolume = value;
                break;
            case OptionCode.effectsVolume:
                EffectsVolume = value;
                break;
        }
    }

    public float LoadOption(OptionCode code) {
        switch (code) {
            case OptionCode.soundsVolume:
                return SoundsVolume;
            case OptionCode.musicVolume:
                return MusicVolume;
            case OptionCode.effectsVolume:
                return EffectsVolume;
        }
        throw new Exception("Invalid option code");
    }

    public void Save() {
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + Config.FILE_OPTIONS, json);
    }

    public void Load() {
        string path = Application.persistentDataPath + Config.FILE_OPTIONS;
        data = null;
        if (File.Exists(path)) {
            string json = File.ReadAllText(path);
            data = JsonUtility.FromJson<OptionsData>(json);
        }
        else {
            Reset();
        }
    }

    public void Reset() {
        data = new OptionsData();
        Save();
    }


    //! OptionsData - Subclass
    [System.Serializable]
    public class OptionsData {

        //! Variables
        public float soundsVolume;
        public float musicVolume;
        public float effectsVolume;

        //! ProgressData - Public
        public OptionsData() {
            soundsVolume = 0.8f;
            musicVolume = 0.8f;
            effectsVolume = 0.8f;
        }
    }

    public enum OptionCode {
        soundsVolume = 0,
        musicVolume = 1,
        effectsVolume = 2
    }
}
