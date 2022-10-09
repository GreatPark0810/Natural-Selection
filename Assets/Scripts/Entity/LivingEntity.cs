using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class LivingEntity : MonoBehaviour {
    // protected LivingEntityData entityData;
    protected float lifeTimeElapsed = 0f;
    protected bool doesHaveChild = false;
    public bool isDead { get; private set; }
    public float speed;


    protected virtual void Awake() {
        isDead = false;
    }

    protected virtual void Update() {
        lifeTimeElapsed += Time.deltaTime;
    }


    // protected virtual void OnCollisionEnter2D(Collision2D other) {
    //     // 피식자가 포식자에 부딪힐 시 피식자 사망
    //     if (gameObject.tag == "Prey" && other.gameObject.tag == "Predator") {
    //         DieEntity();
    //     }
    // }

    protected virtual void OnTriggerEnter2D(Collider2D other) {
        // 피식자가 포식자에 부딪힐 시 피식자 사망
        if (gameObject.tag == "Prey" && other.tag == "Predator") {
            DieEntity();
        }
    }

    protected virtual void DieEntity() {
        isDead = true;
        Destroy(gameObject);
    }

    protected virtual void CellDivision() {}

    public virtual void SetupData(LivingEntityData parentData) {}

    public virtual void SetupData(PredatorData parentData) {}

    public virtual void SetupData(PreyData parentData) {}


}