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

    bool canCollisionDamage = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
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
        if (!pa.isBoarding)
        {
            float rotationSpeed = PlayerInstance.playerStats.rotationSpeed.value + ((PlayerInstance.playerStats.rotationSpeed.value * 0.05f) * PlayerInstance.playerStats.rotationSpeed.crewAssigned);

            float boatSpeed = PlayerInstance.playerStats.moveSpeed.value + ((PlayerInstance.playerStats.moveSpeed.value * 0.05f) * PlayerInstance.playerStats.moveSpeed.crewAssigned);

            float z = Input.GetAxisRaw("Vertical");
            float x = Input.GetAxisRaw("Horizontal") * Time.deltaTime * rotationSpeed;

            speed = (boatSpeedCurve.Evaluate(lastTimeForwardPressed) * Time.deltaTime * boatSpeed) * 1.5f;


            if (z > 0)
                lastTimeForwardPressed += Time.deltaTime * 0.30f;
            else
            {
                lastTimeForwardPressed -= Time.deltaTime * 0.40f;
                if (z < 0)
                {
                    if (lastTimeForwardPressed < 0.01f)
                    {
                        speed = -(boatSpeedCurve.Evaluate(0.1f) * Time.deltaTime * boatSpeed) * 1.5f;
                    }
                }
            }

            if (lastTimeForwardPressed >= 1)
                lastTimeForwardPressed = 1;
            else if (lastTimeForwardPressed <= 0f)
                lastTimeForwardPressed = 0f;


            float sensibility = 0.15f;

            //Pressed
            if (Mathf.Abs(x) >= sensibility)
            {

                if (x > sensibility)
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

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Enemy"))
        {
            Enemy_Stat stat = col.gameObject.GetComponent<Enemy_Stat>();

            switch (stat.enemySize)
            {
                case EnemySize.Small:
                    stat.TakeDamage(15);
                    if (canCollisionDamage)
                        StartCoroutine(TakeCollisionDamage(5));
                    break;
                case EnemySize.Big:
                    stat.TakeDamage(5);
                    if (canCollisionDamage)
                        StartCoroutine(TakeCollisionDamage(10));
                    break;
            }

        }
        else if (col.gameObject.CompareTag("Rock"))
        {
            lastTimeForwardPressed = 0;
            if(canCollisionDamage)
                StartCoroutine(TakeCollisionDamage(15));
        }
    }
    
    IEnumerator TakeCollisionDamage(float p_dmg)
    {
        canCollisionDamage = false;
        PlayerInstance.playerStats.TakeDamage(p_dmg);
        yield return new WaitForSeconds(1f);
        canCollisionDamage = true;
    }
}
