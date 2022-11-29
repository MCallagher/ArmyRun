using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pool : MonoBehaviour {

    //! Static variables
    public static Pool instance;

    //! Variables
    [SerializeField] private List<GameObject> meleeSoldierPool;
    [SerializeField] private List<GameObject> wallPool;

    //! References
    [SerializeField] private GameObject meleeSoldierPrefab;
    [SerializeField] private GameObject wallPrefab;
    [SerializeField] private GameObject meleeSoldierDestination;
    [SerializeField] private GameObject wallDestination;


    //! MonoBehaviour
    void Awake() {
        if (instance == null) {
            instance = this;
        }
        else {
            Destroy(gameObject);
        }
    }

    void Start() {
        meleeSoldierPool = new List<GameObject>();
        for (int i = 0; i < Config.SOLDIER_MELEE_POOL; i++) {
            AddMeleeSoldier();
        }
        wallPool = new List<GameObject>();
        for (int i = 0; i < Config.WALL_POOL; i++) {
            AddWall();
        }
    }


    //! Pool - Public
    public GameObject GetMeleeSoldier() {
        foreach (GameObject entity in meleeSoldierPool) {
            if (!entity.activeInHierarchy) {
                return entity;
            }
        }
        return AddMeleeSoldier();
    }

    public List<GameObject> GetActiveMeleeSoldiers() {
        List<GameObject> meleeSoldiers = new List<GameObject>();
        foreach (Transform meleeSoldierTransform in meleeSoldierDestination.transform) {
            if (meleeSoldierTransform.gameObject.activeInHierarchy) {
                meleeSoldiers.Add(meleeSoldierTransform.gameObject);
            }
        }
        return meleeSoldiers;
    }

    public GameObject GetWall() {
        foreach (GameObject entity in wallPool) {
            if (!entity.activeInHierarchy) {
                return entity;
            }
        }
        return AddWall();
    }

    public List<GameObject> GetActiveWalls() {
        List<GameObject> walls = new List<GameObject>();
        foreach (Transform wallTransform in wallDestination.transform) {
            if (wallTransform.gameObject.activeInHierarchy) {
                walls.Add(wallTransform.gameObject);
            }
        }
        return walls;
    }

    //! Pool - Private
    private GameObject AddMeleeSoldier() {
        GameObject entity = Instantiate(meleeSoldierPrefab, Vector3.zero, meleeSoldierPrefab.transform.rotation);
        entity.transform.parent = meleeSoldierDestination.transform;
        entity.name = "Melee Soldier (" + (meleeSoldierPool.Count + 1) + ")";
        entity.SetActive(false);
        meleeSoldierPool.Add(entity);
        return entity;
    }

    private GameObject AddWall() {
        GameObject entity = Instantiate(wallPrefab, Vector3.zero, wallPrefab.transform.rotation);
        entity.transform.parent = wallDestination.transform;
        entity.name = "Wall (" + (wallPool.Count + 1) + ")";
        entity.SetActive(false);
        wallPool.Add(entity);
        return entity;
    }
}
