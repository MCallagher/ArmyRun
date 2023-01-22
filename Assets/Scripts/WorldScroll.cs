using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldScroll : MonoBehaviour {
    //! MonoBehaviour
    void Update() {
        if(!gameObject.CompareTag(Config.TAG_PLAYER)) {
            transform.Translate(Vector3.back * Config.WORLD_SCROLL_VELOCITY * Time.deltaTime);
        }
    }
}
