﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Movement : MonoBehaviour
{
    private Player_Stat target;
    private float rangeAggro = 25;

    private bool isAggro = false;

    private Enemy_Stat enemyStat;
    private Rigidbody2D rb;

    [HideInInspector]
    public float distance;

    public TargetPoint lastTarget = null;

    private void Awake()
    {

        enemyStat = GetComponent<Enemy_Stat>();
        target = FindObjectOfType<Player_Stat>();
        
        rb = GetComponent<Rigidbody2D>();
        if(rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody2D>();
            
        }
        rb.isKinematic = false;
        rb.gravityScale = 0;

        rangeAggro = (enemyStat.enemySize == EnemySize.Small) ? Random.Range(20, 30) : Random.Range(15, 25);
    }

    // Update is called once per frame
    void Update()
    {
        if(target == null)
        {
            return;
        }

        if(!isAggro && Vector2.Distance(target.transform.position, transform.position) < rangeAggro)
        {
            isAggro = true;
        }


        //Enemy not aggro
        if (!isAggro)
            return;

        Vector3 closestSide = ClosestTargetSide();

        //Destination reached
        if (closestSide == Vector3.zero)
        {
            Quaternion targetRot = target.transform.rotation * Quaternion.Inverse(lastTarget.rotation);

            if (Approximately(transform.rotation, targetRot))           
                transform.rotation = Quaternion.Lerp(transform.rotation, targetRot, Time.deltaTime * 2f);

            return;
        }
        else
        {
            // get the angle
            Vector3 norTar = (closestSide - transform.position).normalized;
            float angle = Mathf.Atan2(norTar.y, norTar.x) * Mathf.Rad2Deg;
            // rotate to angle
            Quaternion rotation = new Quaternion();
            rotation.eulerAngles = new Vector3(0, 0, angle - 90);
            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * 2f);
        }
            

        Vector3 direction = closestSide - transform.position;
        direction.Normalize();
    
        distance = Vector3.Distance(closestSide, transform.position);

        rb.position += (Vector2)direction * enemyStat.enemyStats.moveSpeed.value * Time.deltaTime;
    }

    private bool Approximately(Quaternion val, Quaternion about)
    {
        float angle = Quaternion.Angle(val, about);
        return Mathf.Abs(angle) > 1f;
    }

    private Vector3 ClosestTargetSide()
    {
        TargetPoint closestPoint = target.GetClosestTarget(transform);

        if (closestPoint == null)
            closestPoint = lastTarget;

        if(lastTarget != closestPoint)
        {
            target.DeleteTargetReference(lastTarget.targetPoint);
            lastTarget = closestPoint;
        }

        if (Vector3.Distance(closestPoint.targetPoint.position, transform.position) <= 0.3f)
            return Vector3.zero;

        return closestPoint.targetPoint.position;

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, rangeAggro);
    }
}
