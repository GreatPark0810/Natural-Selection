using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour {
    Vector3 randomPosition;
    float randomZAxisRotation;
    GameObject foods;

    private void Awake() {
        foods = GameObject.Find("Foods");
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Prey") {            
            randomPosition = new Vector3(Random.Range(-8.5f, 8.5f), Random.Range(-4.5f, 4.5f), 0);
            randomZAxisRotation = Random.Range(0f, 90f);

            gameObject.transform.position = randomPosition;
            gameObject.transform.rotation = Quaternion.Euler(0f, 0f, randomZAxisRotation);
        }
    }

    private void InstantiateAfter() {
                
    }
}