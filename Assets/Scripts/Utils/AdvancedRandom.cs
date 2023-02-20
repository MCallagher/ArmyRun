using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdvancedRandom {

    public static Vector3 PositionOnDisk(Vector3 center, float radius, float height) {
        float theta = Random.Range(0, 2 * Mathf.PI);
        float rho = Random.Range(0f, radius);
        return new Vector3(center.x + rho * Mathf.Sin(theta), height, center.z + rho * Mathf.Cos(theta));
    }

    public static Vector3 PositionOnRect(Vector3 center, float dimensionX, float dimensionZ, float height) {
        float deltaX = Random.Range(-dimensionX, dimensionX);
        float deltaZ = Random.Range(-dimensionZ, dimensionZ);
        return new Vector3(center.x + deltaX, height, center.z + deltaZ);
    }

    public static int RangeWithWeight(List<float> weights) {
        float sumWeight = 0;
        foreach (float weight in weights) {
            sumWeight += weight;
        }
        List<float> normWeights = new List<float>();
        foreach (float weight in weights) {
            normWeights.Add(weight / sumWeight);
        }
        return RangeWithNormalizedWeight(normWeights);
    }

    public static int RangeWithNormalizedWeight(List<float> weights) {
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

    public static bool CoinFlip(float prob) {
        return Random.Range(0f, 1f) < prob;
    }

    public static bool CoinFlip() {
        return CoinFlip(0.5f);
    }
}
