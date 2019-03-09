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
        LoadCanons();
    }

    // Start is called before the first frame update
    void Start()
    {

        playerStats.currentHp = playerStats.maxHp;
    }

    // Update is called once per frame
    void Update()
    {
        //for debug purpose
        playerStats = PlayerInstance.playerStats;
    }

    private void LoadCanons()
    {
        LoadCanonsSpots();
        PlayerInstance.playerStats.canons = new List<Canon>();
        foreach (Transform canonSpot in CanonsSpotsParent)
        {
            if (canonSpot.childCount == 0)
                continue;

            Canon_Controller canonCtrl = canonSpot.GetComponentInChildren<Canon_Controller>();

            if (canonCtrl != null)
            {
                canonCtrl.canonInfo.shootPoint = canonSpot;
                canonCtrl.canonInfo.canonPosition = canonSpot;
                PlayerInstance.playerStats.canons.Add(canonCtrl.canonInfo);
            }
        }
    }

    public void AddCanon(int position, Canon canonInfo)
    {

    }

    void LoadCanonsSpots()
    {
        CanonsSpots = new List<Transform>();
        foreach (Transform child in CanonsSpotsParent)
        {
            CanonsSpots.Add(child);
        }
    }
}
