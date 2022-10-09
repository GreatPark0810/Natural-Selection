using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreyMovement : Movement {
    private Prey prey;
    private PreyData preyData;

    protected override void Awake() {
        base.Awake();

        prey = GetComponent<Prey>();
        preyData = GetComponent<PreyData>();
        moveDirection = RandomizeDirection();

        ChangeRotation(moveDirection);
    }

    protected override void Update() {
        base.Update();

        if (changeDirectionTime <= preyData.maintainDirectionTime) {
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

        entityRigidbody.velocity = direction * prey.speed;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
}
