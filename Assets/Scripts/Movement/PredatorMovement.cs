using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PredatorMovement : Movement {
    private Predator predator;
    private PredatorData predatorData;

    protected override void Awake() {
        base.Awake();

        predator = GetComponent<Predator>();
        predatorData = GetComponent<PredatorData>();
        moveDirection = RandomizeDirection();

        ChangeRotation(moveDirection);
    }

    protected override void Update() {
        base.Update();

        if (changeDirectionTime <= predatorData.maintainDirectionTime) {
            changeDirectionTime += Time.deltaTime;
        }

        else {
            moveDirection = RandomizeDirection();
            ChangeRotation(moveDirection.normalized);
            
            changeDirectionTime = 0f;
        }
    }

    protected override void ChangeRotation(Vector2 direction) {
        base.ChangeRotation(direction);

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        entityRigidbody.velocity = direction * predator.speed;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
}
