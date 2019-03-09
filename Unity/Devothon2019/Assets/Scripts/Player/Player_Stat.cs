using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Stat : MonoBehaviour
{
    [SerializeField]
    public Boat_Stats playerStats;

    public Transform CanonsSpotsParent;
    [HideInInspector]
    public List<Transform> CanonsSpots;

    private void Awake()
    {
        //playerStats = PlayerInstance.playerStats;
        Debug.Log(playerStats);
        LoadCanonsSpots();
    }

    // Start is called before the first frame update
    void Start()
    {

        playerStats.currentHp = playerStats.maxHp;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void LoadCanonsSpots()
    {
        foreach(Transform child in CanonsSpotsParent)
        {
            CanonsSpots.Add(child);
        }
    }
}
