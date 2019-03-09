using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Storm : MonoBehaviour
{
    bool windBlow;
    float timeNextWindBlow;
    Player_Movemement pm;
    public float StormViolence = 5;

    // Start is called before the first frame update
    void Start()
    {
        pm = FindObjectOfType<Player_Movemement>();
        windBlow = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(windBlow)
        {
            pm.rotationMomentum += Random.Range(-StormViolence, StormViolence);
            windBlow = false;
            timeNextWindBlow = 5;
        }
        else
        {
            if(timeNextWindBlow < 0)
            {
                windBlow = true;
                
            }
            else
            {
                timeNextWindBlow -= Time.deltaTime;
            }

        }
    }
}
