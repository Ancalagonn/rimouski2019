using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sailor_Movement : MonoBehaviour
{
    public float MovementSpeed = 10f;
    private Rigidbody2D rigidBody;

    private void Awake() {
        this.rigidBody = GetComponent<Rigidbody2D>();
        if (this.rigidBody == null) {
            Debug.Log("Sailor Doesn't have a rigidbody2D");
        }
    }

    public void Move(Vector2 p_direction) {
        this.rigidBody.position += p_direction.normalized * this.MovementSpeed * Time.deltaTime;
    }

    public void MoveTo(Vector2 p_target) {
        // Move Slower for AI
        this.LookAt(p_target);
        this.transform.position = Vector2.MoveTowards(this.transform.position, p_target, this.MovementSpeed * Time.deltaTime * 0.8f);
    }

    public void LookAt(Vector2 p_target) {
        Vector2 direction = (Vector2)this.transform.position - p_target;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        this.transform.rotation = Quaternion.AngleAxis(angle + 90, Vector3.forward);
    }
}
