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

    [HideInInspector]
    public float rotationMomentum = 0;

    private Player_Abordage pa;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        if(rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody2D>();    
        }

        rb.gravityScale = 0;
        rb.isKinematic = false;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        //On obtient le script d'abordage
        pa = this.gameObject.GetComponent<Player_Abordage>();
    }
    // Update is called once per frame
    void Update()
    {
        //On vérifie si le bateau est en cours d'abordage
        if(!pa.isBoarding)
        {
            float z = Input.GetAxisRaw("Vertical");
            float x = Input.GetAxisRaw("Horizontal") * Time.deltaTime * PlayerInstance.playerStats.rotationSpeed.value;

            speed = (boatSpeedCurve.Evaluate(lastTimeForwardPressed) * Time.deltaTime * PlayerInstance.playerStats.moveSpeed.value) * 1.5f;

            if (z > 0)
                lastTimeForwardPressed += Time.deltaTime * 0.30f;
            else
                lastTimeForwardPressed -= Time.deltaTime * 0.40f;

            if (lastTimeForwardPressed >= 1)
                lastTimeForwardPressed = 1;
            else if (lastTimeForwardPressed <= 0)
                lastTimeForwardPressed = 0;


        float sensibility = 0.15f;

        //Pressed
        if (Mathf.Abs(x) >= sensibility)
        {
            
            if(x > sensibility)
            {
                //Rotation side changed
                if (rotationMomentum < 0)
                {
                    rotationMomentum = 0;
                    lastTimeRotationPressed = 0;
                }
            }

            if (x < -sensibility)
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
}
