using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prey : LivingEntity {
    public PreyData preyData { get; private set; }

    private int howManyFoodHaveEaten;

    protected override void Awake() {
        base.Awake();

        preyData = GetComponent<PreyData>();

        speed = preyData.moveSpeed;
    }
    protected override void Update() {
        base.Update();
        
        if (!doesHaveChild && lifeTimeElapsed >= preyData.lifeSpan * 0.5f) {
            CellDivision();
            doesHaveChild = true;
        }


        if (lifeTimeElapsed <= preyData.lifeSpan * 0.5f) {
            speed += Time.deltaTime * 0.3f;
        }

        else if (lifeTimeElapsed < preyData.lifeSpan) {
            speed -= Time.deltaTime * 0.3f;
        }


        if (lifeTimeElapsed >= preyData.lifeSpan) {
            DieEntity();
        }
    }

    protected override void DieEntity() {
        base.DieEntity();
        (GameManager.instance.currentPrey)--;
    }

    protected override void CellDivision() {

        int actualBirthNumber = Mathf.Min(howManyFoodHaveEaten / 3, 4);

        for (int i = 0; i < actualBirthNumber; i++) {
            Prey prey = 
                Instantiate(Spawner.preyPrefab, transform.position, transform.rotation, Spawner.preys.transform);
            prey.SetupData(preyData);

            (GameManager.instance.currentPrey)++;
            (GameManager.instance.accumulatedPrey)++;

            // Debug.Log("피식자\n" +
            //     "세대 : " + prey.preyData.generation + "\n" +
            //     "이동속도 : " + prey.preyData.moveSpeed + "\n" +
            //     "이동방향 변환 시간 : " + prey.preyData.maintainDirectionTime + "\n" +
            //     "수명 : " + prey.preyData.lifeSpan + "\n");
        }
    }

    protected override void OnTriggerEnter2D(Collider2D other) {
        base.OnTriggerEnter2D(other);

        if (other.tag == "Food") {
            howManyFoodHaveEaten++;
        }
    }

    public override void SetupData(PreyData parentData)
    {
        base.SetupData(parentData);
        
        preyData.generation = parentData.generation + 1;

        preyData.maintainDirectionTime = 
            Mathf.Max(
                LivingEntityData.getDeviation(
                    parentData.maintainDirectionTime, parentData.maintainDirectionTime * 0.3f), 1f);
            
        preyData.moveSpeed =
            Mathf.Clamp(
                LivingEntityData.getDeviation(
                    preyData.moveSpeed, preyData.moveSpeed * 0.3f), 1f, 10f);
        
        preyData.lifeSpan =
            Mathf.Clamp(
                LivingEntityData.getDeviation(
                    parentData.lifeSpan, parentData.lifeSpan * 0.2f), 5f, 25f);
        

        preyData.entityTransform = GetComponent<Transform>();
        Vector3 parentScale = 
            new Vector3(
                parentData.transform.localScale.x, 
                parentData.transform.localScale.y, 
                parentData.transform.localScale.z);

        preyData.entityTransform.localScale = 
            new Vector3(
                LivingEntityData.getDeviation(parentScale.x, parentScale.x * 0.1f), 
                LivingEntityData.getDeviation(parentScale.y, parentScale.y * 0.1f),
                parentScale.z);

        howManyFoodHaveEaten = 0;
    }
}
