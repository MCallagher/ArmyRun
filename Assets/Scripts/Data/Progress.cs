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
        if(code == UnlockCode.soldierMelee && data.unlocked[(int)UnlockCode.soldierMelee] == 0 && data.stones >= Config.SOLDIER_MELEE_UNLOCK_COST) {
            data.unlocked[(int)UnlockCode.soldierMelee] = 1;
            Save();
            return true;
        }
        if(code == UnlockCode.soldierRanged && data.unlocked[(int)UnlockCode.soldierRanged] == 0 && data.stones >= Config.SOLDIER_RANGED_UNLOCK_COST) {
            data.unlocked[(int)UnlockCode.soldierRanged] = 1;
            Save();
            return true;
        }
        if(code == UnlockCode.soldierGunner && data.unlocked[(int)UnlockCode.soldierGunner] == 0 && data.stones >= Config.SOLDIER_GUNNER_UNLOCK_COST) {
            data.unlocked[(int)UnlockCode.soldierGunner] = 1;
            Save();
            return true;
        }
        if(code == UnlockCode.soldierSniper && data.unlocked[(int)UnlockCode.soldierSniper] == 0 && data.stones >= Config.SOLDIER_SNIPER_UNLOCK_COST) {
            data.unlocked[(int)UnlockCode.soldierSniper] = 1;
            Save();
            return true;
        }
        if(code == UnlockCode.soldierBumper && data.unlocked[(int)UnlockCode.soldierBumper] == 0 && data.stones >= Config.SOLDIER_BUMPER_UNLOCK_COST) {
            data.unlocked[(int)UnlockCode.soldierBumper] = 1;
            Save();
            return true;
        }
        if(code == UnlockCode.soldierKamikaze && data.unlocked[(int)UnlockCode.soldierKamikaze] == 0 && data.stones >= Config.SOLDIER_KAMIKAZE_UNLOCK_COST) {
            data.unlocked[(int)UnlockCode.soldierKamikaze] = 1;
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
            Reset();
        }
    }

    public void Reset() {
        data = new ProgressData();
        Save();
    }

    public bool IsUnlocked(UnlockCode code) {
        return data.unlocked[(int)code] == 1;
    }

    public void AddStones(int collectedStones) {
        data.stones += collectedStones;
    }

    public int GetStones() {
        return data.stones;
    }


    //! ProgressData - Subclass
    [System.Serializable]
    public class ProgressData {

        //! Variables
        public List<int> unlocked;
        public int stones;

        //! ProgressData - Public
        public ProgressData() {
            unlocked = new List<int>(new int[]{1, 0, 0, 0, 0, 0});
            stones = 0;
        }
    }


    //! UnlockCode - Enum
    public enum UnlockCode {
        soldierMelee = 0,
        soldierRanged = 1,
        soldierGunner = 2,
        soldierSniper = 3,
        soldierBumper = 4,
        soldierKamikaze = 5
    }
}
