using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movemement : MonoBehaviour
{
    private Rigidbody2D rb;

    public AnimationCurve boatSpeedCurve;
    public AnimationCurve boatRotationCurve;

    private float lastTimeForwardPressed = 0;
    private float lastTimeRotationPressed = 0;
    private float speed = 0;
    private float rotationMomentum = 0;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        if(rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody2D>();
            rb.isKinematic = true;
        }
    }


    // Update is called once per frame
    void Update()
    {
        float z = Input.GetAxisRaw("Vertical") * Time.deltaTime * PlayerInstance.playerStats.moveSpeed.value;
        float x = Input.GetAxisRaw("Horizontal") * Time.deltaTime * PlayerInstance.playerStats.rotationSpeed.value;

        float timeSinceLastForward = Time.time - lastTimeForwardPressed;
        float timeSinceLastRotation = Time.time - lastTimeRotationPressed;

        if (timeSinceLastForward >= 1)
            timeSinceLastForward = 1;

        //Pressed
        if (z > 0)
        {
            lastTimeForwardPressed += Time.deltaTime * 0.8f;
            //Speed curve
            speed = z * boatSpeedCurve.Evaluate(timeSinceLastForward);
        }
        else //Not pressed
        {
            lastTimeForwardPressed = Time.time;

            //80% less speed each seconds
            speed -= (Time.deltaTime * speed * 0.80f);
        }

        //Pressed
        if (x != 0)
        {
            
            if(x > 0)
            {
                //Rotation side changed
                if (rotationMomentum < 0)
                {
                    rotationMomentum = 0;
                    lastTimeRotationPressed = 0;
                }
            }

            if (x < 0)
            {
                //Rotation side changed
                if (rotationMomentum > 0)
                {
                    rotationMomentum = 0;
                    lastTimeRotationPressed = 0;
                }
            }

            lastTimeRotationPressed += Time.deltaTime * 0.6f;
            //Speed curve
            rotationMomentum = x * boatRotationCurve.Evaluate(lastTimeRotationPressed);
        }
        else //Not pressed
        {
            lastTimeRotationPressed = Time.time;

            //40% less rotation speed each seconds
            rotationMomentum -= (Time.deltaTime * rotationMomentum * 0.98f);
        }


        transform.Rotate(-transform.forward, rotationMomentum);

        rb.position += (Vector2)transform.up * speed;
    }
}
