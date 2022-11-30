using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdvancedRandom {

    public static Vector3 PositionOnDisk(Vector3 center, float radius, float height) {
        float theta = Random.Range(0, 2 * Mathf.PI);
        float rho = Random.Range(0f, radius);
        Vector3 offset = new Vector3(rho * Mathf.Sin(theta), height, rho * Mathf.Cos(theta));
        return center + offset;
    }

    public static int RangeWithWeight(List<float> weights) {
        float targetValue = Random.Range(0f, 1f);
        float currValue = 0f;
        for (int i = 0; i < weights.Count; i++) {
            currValue += weights[i];
            if (currValue > targetValue) {
                return i;
            }
        }
        return weights.Count -1;
    }
}
