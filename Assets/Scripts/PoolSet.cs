using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolSet : MonoBehaviour {

    //! Static variables
    public static PoolSet instance;

    //! Properties
    public Pool<Wall> Wall {
        get {
            return Pool<Wall>.instance;
        }
    }

    public Pool<MeleeSoldier> MeleeSoldier {
        get {
            return Pool<MeleeSoldier>.instance;
        }
    }


    //! MonoBehaviour
    void Awake() {
        if (instance == null) {
            instance = this;
        }
        else {
            Destroy(gameObject);
        }
    }
}
