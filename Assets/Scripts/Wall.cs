using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    private static float scrollVelocity = 5;

    void Update()
    {
        MoveForward();
    }

    private void MoveForward() {
        transform.Translate(Vector3.back * scrollVelocity * Time.deltaTime);
    }
}
