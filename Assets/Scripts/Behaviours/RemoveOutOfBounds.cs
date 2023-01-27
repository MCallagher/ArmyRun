using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveOutOfBounds : MonoBehaviour {
    //! MonoBehaviour
    void Update() {
        if(IsOutOfBounds()) {
            RemoveFromGame();
        }
    }

    //! RemoveOutOfBounds - Private
    private bool IsOutOfBounds() {
        bool outOfBoundX = Mathf.Abs(transform.position.x) > Config.WORLD_BOUND_X;
        bool outOfBoundY = transform.position.y > Config.WORLD_BOUND_Y_UP || transform.position.y < Config.WORLD_BOUND_Y_DOWN;
        bool outOfBoundZ = transform.position.z > Config.WORLD_BOUND_Z_FORWARD || transform.position.z < Config.WORLD_BOUND_Z_BACK;
        return outOfBoundX || outOfBoundY || outOfBoundZ;
    }

    private void RemoveFromGame() {
        gameObject.SetActive(false);
    }
}
