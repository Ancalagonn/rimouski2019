using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sailor_Controller : MonoBehaviour
{
    private Sailor_Movement movement;
    private Sailor_Actions actions;
    private Vector2 move;

    void Awake() {
        this.move = new Vector2();

        this.movement = GetComponent<Sailor_Movement>();
        if (this.movement == null) {
            Debug.Log("No Sailor_Movement script on Sailor");
        }

        this.actions = GetComponent<Sailor_Actions>();
        if (this.actions == null) {
            Debug.Log("No Sailor_Actions script on sailor");
        }

        
        // Override default stats
        this.actions.Stats.HP = 150;
        this.actions.Stats.weaponDamage = 20;
        this.actions.Stats.weaponCooldown = 1f;
    }

    // Update is called once per frame
    void Update()
    {
       this.handleMove();
       this.handleActions();
       this.followMouse();
    }

    private void handleMove() {
        this.move.x = Input.GetAxisRaw("Horizontal");
        this.move.y = Input.GetAxisRaw("Vertical");
        this.movement.Move(this.move);
    }

    private void handleActions() {
        if (Input.GetMouseButtonDown(0)) {
            this.actions.Shoot(); 
        }

        if (Input.GetMouseButtonDown(1)) {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            var ray = Physics2D.Raycast(mousePos, Vector2.up);

            if (ray.collider == null || ray.collider.tag != "Loot") {
                return;
            }

            this.actions.Loot(ray.transform.gameObject);
        }
    }

    private void followMouse() {
        this.movement.LookAt(Camera.main.ScreenToWorldPoint(Input.mousePosition));
    }
}
