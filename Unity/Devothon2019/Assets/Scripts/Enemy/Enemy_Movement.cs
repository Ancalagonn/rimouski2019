using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Movement : MonoBehaviour
{
    private Transform target;
    public float rangeAggro = 25;

    private bool isAggro = false;

    private Enemy_Stat enemyStat;
    private Rigidbody2D rb;

    private void Awake()
    {
        enemyStat = GetComponent<Enemy_Stat>();
        target = FindObjectOfType<Player_Movemement>().transform;
        
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
        if(target == null)
        {
            Debug.Log("cannot find player");
            return;
        }

        if(!isAggro && Vector2.Distance(target.position, transform.position) < rangeAggro)
        {
            isAggro = true;
        }


        //Enemy not aggro
        if (!isAggro)
            return;

        Vector3 closestSide = ClosestTargetSide();

        //Destination reached
        if (closestSide == Vector3.zero)
            return;


        Vector3 direction = closestSide - transform.position;
        direction.Normalize();

        //transform.rotation = Quaternion.RotateTowards(target.rotation, , enemyStat.enemyStats.rotationSpeed.value * Time.deltaTime);

        Vector3 targetRotation = new Vector3(target.transform.rotation.x, target.transform.rotation.y, target.transform.rotation.z + 90);

        Quaternion targetRot = Quaternion.Euler(targetRotation);


        transform.rotation = Quaternion.Lerp(transform.rotation, targetRot, Time.deltaTime);
        rb.position += (Vector2)direction * enemyStat.enemyStats.moveSpeed.value * Time.deltaTime;
    }

    private Vector3 ClosestTargetSide()
    {
        float offset = 2f;

        bool ypos = Mathf.Abs(transform.position.y - target.position.y) < 3f;
        bool xpos = Mathf.Abs(transform.position.y - target.position.y) < 3f;

        Vector3 leftSide = target.position + (-target.right * offset * 4);
        Vector3 rightSide = target.position + (target.right * offset * 4);
        Vector3 botSide = target.position + (-target.up * offset * 4);
        Vector3 topSide = target.position + (target.up * offset * 4);

        float leftDist = Vector3.Distance(transform.position, leftSide);
        float rightDist = Vector3.Distance(transform.position, leftSide);

        float topDist = Vector3.Distance(transform.position, topSide);
        float botDist = Vector3.Distance(transform.position, botSide);

        Vector3 X;
        Vector3 Y;

        if (leftDist < rightDist)
        {
            X = leftSide;
        }
        else
        {
            X = rightSide;
        }

        if (topDist < botDist)
        {
            Y = topSide;
        }
        else
        {
            Y = botSide;
        }

        Vector3 closest = Vector3.Distance(X, target.position) < Vector3.Distance(Y, target.position) ? X : Y;



        if(Vector3.Distance(closest, transform.position) <= 0.3f)
            return Vector3.zero;


        return closest;

    }
}
