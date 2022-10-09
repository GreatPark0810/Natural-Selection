using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    private static GameManager mInstance;
    public static GameManager instance {
        get {
            if (mInstance == null)
                mInstance = FindObjectOfType<GameManager>();

            return mInstance;
        }
    }
    
    public int accumulatedPredator { get; set; }
    public int accumulatedPrey { get; set; }
    public int currentPredator { get; set; }
    public int currentPrey { get; set; }

    private void Awake() {
        if (instance != this) {
            Destroy(gameObject);
        }

        accumulatedPredator = 0;
        accumulatedPrey = 0;
        currentPredator = 0;
        currentPrey = 0;
    }

    void Start() {
        
    }

    void Update() {
        Debug.Log($"{currentPredator}, {currentPrey}");
    }
}
