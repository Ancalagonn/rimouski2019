using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movemement : MonoBehaviour
{
    private Boat_Stats playerStats;

    private void Awake()
    {
        playerStats = GetComponent<Player_Stat>().playerStats;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
