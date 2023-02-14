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

    public void Unlock(UnlockCode code) {
        if(code == UnlockCode.soldierMelee) {
            data.unlocked[UnlockCode.soldierMelee] = true;
        }
        else if(code == UnlockCode.soldierRanged) {
            data.unlocked[UnlockCode.soldierRanged] = true;
        }
        Save();
    }

    public int GetDiamonds() {
        return data.diamonds;
    }

    public void AddDiamonds(int collectedDiamonds) {
        data.diamonds += collectedDiamonds;
    }

    public bool IsUnlocked(UnlockCode code) {
        return data.unlocked[code];
    }


    //! ProgressData - Subclass
    [System.Serializable]
    public class ProgressData : MonoBehaviour {

        //! Variables
        public Dictionary<UnlockCode, bool> unlocked;
        public int diamonds;

        //! ProgressData - Public
        public ProgressData() {
            unlocked = new Dictionary<UnlockCode, bool>();
            unlocked.Add(UnlockCode.soldierMelee, true);
            unlocked.Add(UnlockCode.soldierRanged, false);
            diamonds = 0;
        }
    }


    //! UnlockCode - Enum
    public enum UnlockCode {
        soldierMelee = 0,
        soldierRanged = 1
    }
}
