using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivingEntityData : MonoBehaviour {
    public int generation = 0;
    public float maintainDirectionTime = 2f;
    public float moveSpeed = 5f;
    public float lifeSpan = 5f;
    public Transform entityTransform;

    void Awake() {
        entityTransform = GetComponent<Transform>();
    }

    public static float getDeviation(float mean, float standard) {
        float x1 = Random.Range(0f, 1f);
        float x2 = Random.Range(0f, 1f);

        return mean + standard * Mathf.Sqrt(-2.0f * Mathf.Log(x1)) * Mathf.Sin(2 * Mathf.PI * x2);
    }
}
