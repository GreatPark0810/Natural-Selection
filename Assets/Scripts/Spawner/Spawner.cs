using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public static Food foodPrefab;
    public static Predator predatorPrefab;
    public static Prey preyPrefab;

    public static GameObject foods;
    public static GameObject predators;
    public static GameObject preys;

    // Start is called before the first frame update
    void Start() {
        foodPrefab = Resources.Load<Food>("Prefabs/Food");
        predatorPrefab = Resources.Load<Predator>("Prefabs/Predator");
        preyPrefab = Resources.Load<Prey>("Prefabs/Prey");

        foods = GameObject.Find("Foods");
        predators = GameObject.Find("Predators");
        preys = GameObject.Find("Preys");

        for (int i = 0; i < 100; i++) {
            Food food = InstantiateAtRandomPosition(foodPrefab);
        }

        for (int i = 0; i < 3; i++) {
            Predator predator = InstantiateAtRandomPosition(predatorPrefab);
            predator.SetupData(predator.predatorData);
            (GameManager.instance.accumulatedPredator)++;
            (GameManager.instance.currentPredator)++;
        }

        for (int i = 0; i < 200; i++) {
            Prey prey = InstantiateAtRandomPosition(preyPrefab);
            prey.SetupData(prey.preyData);
            (GameManager.instance.accumulatedPrey)++;
            (GameManager.instance.currentPrey)++;
        }
    }

    private static Food InstantiateAtRandomPosition(Food foodPrefab) {
        Vector3 randomPosition = new Vector3(Random.Range(-8.5f, 8.5f), Random.Range(-4.5f, 4.5f), 0);
        float randomZAxisRotation = Random.Range(0f, 90f);

        Food food = 
            Instantiate(foodPrefab, randomPosition, Quaternion.Euler(0f, 0f, randomZAxisRotation), foods.transform);
        
        return food;
    }

    private static Predator InstantiateAtRandomPosition(Predator predatorPrefab) {
        Vector3 randomPosition = new Vector3(Random.Range(-8.5f, 8.5f), Random.Range(-4.5f, 4.5f), 0);
        float randomZAxisRotation = Random.Range(0f, 90f);

        Predator predator = 
            Instantiate(predatorPrefab, randomPosition, Quaternion.Euler(0f, 0f, randomZAxisRotation), predators.transform);
    
        return predator;
    }

    private static Prey InstantiateAtRandomPosition(Prey preyPrefab) {
        Vector3 randomPosition = new Vector3(Random.Range(-8.5f, 8.5f), Random.Range(-4.5f, 4.5f), 0);
        float randomZAxisRotation = Random.Range(0f, 90f);

        Prey prey = 
            Instantiate(preyPrefab, randomPosition, Quaternion.Euler(0f, 0f, randomZAxisRotation), preys.transform);
    
        return prey;
    }
}
