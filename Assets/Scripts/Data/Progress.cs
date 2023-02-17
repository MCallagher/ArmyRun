using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Progress : MonoBehaviour {

    //! Static variables
    public static Progress instance;

    //! Variables
    [SerializeField] private ProgressData data;


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
    public bool Unlock(UnlockCode code) {
        if(code == UnlockCode.soldierMelee && data.unlocked[(int)UnlockCode.soldierMelee] == 0 && data.diamonds >= Config.SOLDIER_MELEE_UNLOCK_COST) {
            data.unlocked[(int)UnlockCode.soldierMelee] = 1;
            Save();
            return true;
        }
        if(code == UnlockCode.soldierRanged && data.unlocked[(int)UnlockCode.soldierRanged] == 0 && data.diamonds >= Config.SOLDIER_RANGED_UNLOCK_COST) {
            data.unlocked[(int)UnlockCode.soldierRanged] = 1;
            Save();
            return true;
        }
        return false;
    }

    public void Save() {
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + Config.FILE_PROGRESS, json);
    }

    public void Load() {
        string path = Application.persistentDataPath + Config.FILE_PROGRESS;
        data = null;
        if (File.Exists(path)) {
            string json = File.ReadAllText(path);
            data = JsonUtility.FromJson<ProgressData>(json);
        }
        else {
            data = new ProgressData();
        }
    }

    public bool IsUnlocked(UnlockCode code) {
        return data.unlocked[(int)code] == 1;
    }

    public void AddDiamonds(int collectedDiamonds) {
        data.diamonds += collectedDiamonds;
    }

    public int GetDiamonds() {
        return data.diamonds;
    }


    //! ProgressData - Subclass
    [System.Serializable]
    public class ProgressData {

        //! Variables
        public List<int> unlocked;
        public int diamonds;

        //! ProgressData - Public
        public ProgressData() {
            unlocked = new List<int>();
            unlocked.Add(1);
            unlocked.Add(0);
            diamonds = 0;
        }
    }


    //! UnlockCode - Enum
    public enum UnlockCode {
        soldierMelee = 0,
        soldierRanged = 1
    }
}
