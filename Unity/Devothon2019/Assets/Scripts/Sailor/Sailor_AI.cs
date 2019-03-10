using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sailor_AI : MonoBehaviour
{
    private Sailor_Actions actions;
    private Sailor_Movement movements;

    private float agroRange = 20f;
    private float attackRange = 8f;

    private GameObject target;

    private void Awake() {
        this.target = GameObject.Find("Sailor");

        this.actions = GetComponent<Sailor_Actions>();
        if (this.actions == null) {
            Debug.Log("No Sailer_Actions on Ai sailor");
        }

        this.movements = GetComponent<Sailor_Movement>();
        if (this.movements == null) {
            Debug.Log("No Sailor_Movement on Ai sailor");
        }
    }

    private void Update() {
        this.makeDecision();
    }

    private void makeDecision() {
        float distanceToTarget = Vector2.Distance(this.transform.position, target.transform.position);
        if (distanceToTarget > this.agroRange) {
            return;
        }

        this.movements.LookAt(this.target.transform.position);

        if (distanceToTarget > this.attackRange) {
            this.handleMovement();
            return;
        }
        if (distanceToTarget <= this.attackRange) {
            this.handleAttack();
        }
    }

    private void handleMovement() {
        this.movements.MoveTo(this.target.transform.position); 
    }

    private void handleAttack() {
        this.actions.Shoot();
    }
}
