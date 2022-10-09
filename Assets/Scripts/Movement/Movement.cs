using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {
    protected LivingEntityData entityData;
    protected Rigidbody2D entityRigidbody;
    protected Vector2 moveDirection;
    protected float changeDirectionTime = 0f;
    

    protected virtual void Awake() {
        entityRigidbody = GetComponent<Rigidbody2D>();
    }

    protected virtual void Update() {

    }

    protected Vector2 RandomizeDirection() {
        float xVector = Random.Range(-1f, 1f);
        float yVector = Random.Range(-1f, 1f);

        Vector2 direction = new Vector2(xVector, yVector);
        direction.Normalize();

        return direction;
    }

    protected virtual void ChangeRotation(Vector2 direction) {}

    private Vector2 GetReflectedDirection(Vector2 inDirection, Vector2 normal) {
        if (inDirection == null || normal == null) return new Vector2(0f, 0f);
        
        Vector2 outDirection = inDirection + (2 * normal * Vector2.Dot(-1 * inDirection, normal));

        return outDirection; 
    }

    // private void OnCollisionEnter2D(Collision2D other) {
    //     // 벽과 부딪힐 시 포식자와 피식자 모두 반사
    //     if (other.gameObject.tag == "Wall") {
    //         Vector2 reflectedDirection = Vector2.Reflect(moveDirection, other.contacts[0].normal);
    //         moveDirection = reflectedDirection.normalized;
    //         ChangeRotation(moveDirection);
    //     }
    // }

    protected virtual void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "UpsideWall" ||
            other.tag == "DownsideWall" ||
            other.tag == "LeftWall" ||
            other.tag == "RightWall") {
            
            Vector2 normalVector = Vector2.zero;

            switch (other.tag) {
                case "UpsideWall":
                    normalVector = Vector2.down;
                    break;
                
                case "DownsideWall":
                    normalVector = Vector2.up;
                    break;

                case "LeftWall":
                    normalVector = Vector2.right;
                    break;

                case "RightWall":
                    normalVector = Vector2.left;
                    break;
            }

            Vector2 reflectedDirection = GetReflectedDirection(moveDirection, normalVector);
            moveDirection = reflectedDirection.normalized;
            ChangeRotation(moveDirection);
        }
    }
}
