using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinAroundY : MonoBehaviour {

    //! Parameters
    [SerializeField] private float rotationSpeed = 120; // in deg/s

    //! MonoBehaviour
    void Update() {
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime, Space.World);
    }
}
