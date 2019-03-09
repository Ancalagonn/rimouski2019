using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Stat : MonoBehaviour
{
    [SerializeField]
    public Boat_Stats playerStats;

    private void Awake()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        playerStats = PlayerInstance.playerStats;
        Debug.Log(playerStats);
        playerStats.currentHp = playerStats.maxHp;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
