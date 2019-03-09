using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movemement : MonoBehaviour
{
    private Boat_Stats playerStats;
    private Rigidbody2D rb;

    public AnimationCurve boatSpeedCurve;

    private float lastTimePressed = 0;
    private float speed = 0;

    private void Awake()
    {
        playerStats = GetComponent<Player_Stat>().playerStats;
        rb = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float z = Input.GetAxisRaw("Vertical") * Time.deltaTime * playerStats.moveSpeed.value;
        float x = Input.GetAxis("Horizontal") * Time.deltaTime * playerStats.rotationSpeed.value;

        float timeSinceLastPressed = Time.time - lastTimePressed;

        if (timeSinceLastPressed >= 1)
            timeSinceLastPressed = 1;

        //Pressed
        if (z > 0)
        {
            lastTimePressed += Time.deltaTime * 0.8f;
            //Speed curve
            speed = z * boatSpeedCurve.Evaluate(timeSinceLastPressed);
        }
        else //Not pressed
        {
            lastTimePressed = Time.time;

            //80% less speed each seconds
            speed -= (Time.deltaTime * speed * 0.80f);
        }

        transform.Rotate(-transform.forward, x);

        rb.position += (Vector2)transform.up * speed;
    }
}
