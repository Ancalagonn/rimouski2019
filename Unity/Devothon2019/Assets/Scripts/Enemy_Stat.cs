using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Stat : MonoBehaviour
{
    public Boat_Stats enemyStats;

    // Start is called before the first frame update
    void Start()
    {
        enemyStats.currentHp = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyStats.isDead())
            Destroy(gameObject);
    }

    public void TakeDamage(float p_damage)
    {
        enemyStats.TakeDamage(p_damage);
        Debug.Log(gameObject.name + " : " + enemyStats.currentHp);
    }


}
