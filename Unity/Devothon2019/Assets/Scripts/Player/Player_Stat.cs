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
        SoundManager.LoadSound();
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

        for (int i = 0; i < PlayerInstance.playerStats.canons.Count; i++)
        {
            Canon canon = PlayerInstance.playerStats.canons[i];

            if (canon == null)
                continue;

            //Instantiate canon prefab
            GameObject canonObj = new GameObject();
            canonObj.name = "Canon" + i;
            canonObj.transform.SetParent(CanonsSpots[i]);        

            Canon_Controller canonCtrl = canonObj.AddComponent<Canon_Controller>();
            canonCtrl.canonInfo = canon;
            canonCtrl.canonInfo.shootPoint = CanonsSpots[i];         
        }
    }

    void LoadCanonsSpots()
    {
        CanonsSpots = new List<Transform>();
        foreach (Transform child in CanonsSpotsParent)
        {
            CanonsSpots.Add(child);
            foreach (Transform c2 in child)
            {
                Destroy(c2.gameObject);
            }
        }
    }
}
