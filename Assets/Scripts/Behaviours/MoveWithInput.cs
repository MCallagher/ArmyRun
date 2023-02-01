using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveWithInput : MonoBehaviour {

    void Update() {
        ControlMovement();
    }

    private void ControlMovement() {
        // Read input
        float sideInput = Input.GetAxis("Horizontal");
        float frontalInput = Input.GetAxis("Vertical");

        // Apply movement
        Vector3 direction = (Vector3.right * sideInput + Vector3.forward * frontalInput).normalized;
        Vector3 movement = direction * Config.WAYPOINT_VELOCITY * Time.deltaTime;
        transform.Translate(movement);

        // Clamp positions
        float x = Mathf.Clamp(transform.position.x, -Config.WORLD_BOUND_PLAYER_X, Config.WORLD_BOUND_PLAYER_X);
        float y = transform.position.y;
        float z = Mathf.Clamp(transform.position.z, -Config.WORLD_BOUND_PLAYER_Z, Config.WORLD_BOUND_PLAYER_Z);
        transform.position = new Vector3(x, y, z);
    }
}
