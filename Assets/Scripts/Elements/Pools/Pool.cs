using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pool<T> : MonoBehaviour {

    //! Static variables
    public static Pool<T> instance;

    //! Variables
    [SerializeField] private string entityName;
    [SerializeField] private List<GameObject> pool;

    //! References
    [SerializeField] private GameObject prefab;


    //! MonoBehaviour
    void Awake() {
        if (instance == null) {
            instance = this;
            pool = new List<GameObject>();
            for (int i = 0; i < Config.POOL_INIT_SIZE; i++) {
                AddEntity();
            }
        }
        else {
            Destroy(gameObject);
        }
    }


    //! Pool - Public
    public GameObject GetEntity() {
        foreach (GameObject entity in pool) {
            if (!entity.activeInHierarchy) {
                return entity;
            }
        }
        return AddEntity();
    }

    public List<GameObject> GetActiveGameObject() {
        List<GameObject> entities = new List<GameObject>();
        foreach (Transform entityTransform in transform) {
            if (entityTransform.gameObject.activeInHierarchy) {
                entities.Add(entityTransform.gameObject);
            }
        }
        return entities;
    }

    public List<T> GetActiveEntities() {
        List<T> entities = new List<T>();
        foreach (GameObject entityObject in GetActiveGameObject()) {
            entities.Add(entityObject.GetComponent<T>());
        }
        return entities;
    }


    //! Pool - Private
    private GameObject AddEntity() {
        GameObject entity = Instantiate(prefab, Vector3.zero, prefab.transform.rotation);
        entity.transform.parent = transform;
        entity.name = entityName + " (" + (pool.Count + 1) + ")";
        entity.SetActive(false);
        pool.Add(entity);
        return entity;
    }
}
