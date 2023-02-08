using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Progress : MonoBehaviour {

    //! Static variables
    public static Progress instance;

    //! Variables
    private ProgressData data;


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
    }


    //! Progress - public
    public void Save() {
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + Config.FILE_PROGRESS, json);
    }

    public void Load() {
        string path = Application.persistentDataPath + Config.FILE_PROGRESS;
        if (File.Exists(path)) {
            string json = File.ReadAllText(path);
            data = JsonUtility.FromJson<ProgressData>(json);
        }
        data = new ProgressData();
    }


    //! ProgressData - Subclass
    [System.Serializable]
    public class ProgressData : MonoBehaviour {

        //! Variables
        private int soldierMeleeLevel;
        private int soldierRangeLevel;

        //! Properties
        public int SoldierMeleeLevel {
            get {
                return soldierMeleeLevel;
            }
            private set {
                soldierMeleeLevel = Mathf.Max(1, value);
            }
        }

        public int SoldierRangeLevel {
            get {
                return soldierRangeLevel;
            }
            private set {
                soldierRangeLevel = Mathf.Max(1, value);
            }
        }

        //! ProgressData - Protected
        public ProgressData() {
            soldierMeleeLevel = 1;
            soldierRangeLevel = 1;
        }
    }
}
