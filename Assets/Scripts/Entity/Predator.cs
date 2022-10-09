using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Predator : LivingEntity {
    public PredatorData predatorData { get; private set; }

    private int howManyPreyHaveEaten;
    private float starvingTime;

    protected override void Awake() {
        base.Awake();

        predatorData = GetComponent<PredatorData>();

        speed = predatorData.moveSpeed;
    }

    protected override void Update() {
        base.Update();

        starvingTime += Time.deltaTime;

        if (GameManager.instance.currentPredator <= 4) {
            if (!doesHaveChild && lifeTimeElapsed >= predatorData.lifeSpan * 0.8f) {
                // CellDivision();
                doesHaveChild = true;
            }
        }

        
        // if (lifeTimeElapsed <= predatorData.lifeSpan * 0.5f) {
        //     speed += Time.deltaTime * 0.3f;
        // }

        // else if (lifeTimeElapsed < predatorData.lifeSpan) {
        //     speed -= Time.deltaTime * 0.3f;
        // }

        
        if (lifeTimeElapsed >= predatorData.lifeSpan || 
            starvingTime >= predatorData.lifeSpan * 0.4f) {
            // DieEntity();
        }
    }

    protected override void CellDivision() {
        base.CellDivision();

        int actualBirthNumber = Mathf.Min(howManyPreyHaveEaten / 3, 1);
        for (int i = 0; i < actualBirthNumber; i++) {
            Predator predator = 
                Instantiate(Spawner.predatorPrefab, transform.position, transform.rotation, Spawner.predators.transform);
            predator.SetupData(predatorData);

            (GameManager.instance.currentPredator)++;
            (GameManager.instance.accumulatedPredator)++;

            // Debug.Log("포식자\n" +
            //     "세대 : " + predator.predatorData.generation + "\n" +
            //     "이동속도 : " + predator.predatorData.moveSpeed + "\n" +
            //     "이동방향 변환 시간 : " + predator.predatorData.maintainDirectionTime + "\n" +
            //     "수명 : " + predator.predatorData.lifeSpan + "\n");
        }
    }

    protected override void DieEntity() {
        base.DieEntity();
        (GameManager.instance.currentPredator)--;
    }

    protected override void OnTriggerEnter2D(Collider2D other) {
        base.OnTriggerEnter2D(other);
        
        if (other.tag == "Prey") {
            howManyPreyHaveEaten++;
            starvingTime = 0f;
        }
    }

    public override void SetupData(PredatorData parentData) {
        base.SetupData(parentData);

        predatorData.generation = parentData.generation + 1;

        predatorData.maintainDirectionTime = 
            Mathf.Max(
                LivingEntityData.getDeviation(
                    parentData.maintainDirectionTime, parentData.maintainDirectionTime * 0.3f), 1f);
            
        predatorData.moveSpeed =
            Mathf.Clamp(
                LivingEntityData.getDeviation(
                    predatorData.moveSpeed, predatorData.moveSpeed * 0.3f), 1f, 10f);
        
        predatorData.lifeSpan =
            Mathf.Clamp(
                LivingEntityData.getDeviation(
                    parentData.lifeSpan, parentData.lifeSpan * 0.3f), 10f, 30f);
        

        predatorData.entityTransform = GetComponent<Transform>();
        Vector3 parentScale = 
            new Vector3(
                parentData.transform.localScale.x, 
                parentData.transform.localScale.y, 
                parentData.transform.localScale.z);

        predatorData.entityTransform.localScale = 
            new Vector3(
                LivingEntityData.getDeviation(parentScale.x, parentScale.x * 0.2f), 
                LivingEntityData.getDeviation(parentScale.y, parentScale.y * 0.2f),
                parentScale.z);

        howManyPreyHaveEaten = 0;
        starvingTime = 0f;
    }
}
