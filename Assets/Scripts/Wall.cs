using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    //! Monobehaviour
    void Update()
    {
        if (IsOutOfBound()) {
            Destroy(gameObject);
        }
        MoveForward();
    }

    //! Wall - Private
    private void MoveForward() {
        transform.Translate(Vector3.back * Config.WORLD_SCROLL_VELOCITY * Time.deltaTime);
    }

    private bool IsOutOfBound() {
        bool outBack = transform.position.z < Config.WORLD_BOUND_Z_BACK;
        return outBack;
    }
}
