using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Stat : MonoBehaviour
{
    [SerializeField]
    public Boat_Stats playerStats;
    public Player_Movemement playerMovement;

    public Transform CanonsSpotsParent;
    [HideInInspector]
    public List<Transform> CanonsSpots;

    private void Awake()
    {
        Static_Resources.LoadResources();
        SoundManager.LoadSound();
        playerStats = PlayerInstance.playerStats;
        LoadCanons();
        playerStats.mySelf = this.transform;
        playerMovement = this.GetComponent<Player_Movemement>();
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

    /// <summary>
    /// Take damage (Use with SendMessage)
    /// </summary>
    /// <param name="p_damage"></param>
    public void TakeDamage(float p_damage)
    {
        playerStats.TakeDamage(p_damage);
    }

    /// <summary>
    /// Load canons from playerinstance
    /// </summary>
    private void LoadCanons()
    {
        LoadCanonsSpots();

        for (int i = 0; i < PlayerInstance.playerStats.canons.Count; i++)
        {
            Canon canon = PlayerInstance.playerStats.canons[i];

            if (canon == null)
                continue;

            //Instantiate canon prefab
            GameObject canonObj = null;

            switch (canon.canonType)
            {
                default:
                case CanonType.Normal:
                    canonObj = Instantiate(Static_Resources.defaultCanon);
                    break;
                case CanonType.TripleShot:
                    canonObj = Instantiate(Static_Resources.defaultCanon);
                    break;
                case CanonType.FlameThrower:
                    break;
            }
            
            canonObj.name = "Canon" + i;
            canonObj.transform.SetParent(CanonsSpots[i]);
            canonObj.transform.position = new Vector3(0, 0, -1);
            canonObj.transform.localPosition = new Vector3(0, 0, -1);
            canonObj.transform.rotation = CanonsSpots[i].rotation;

            Canon_Controller canonCtrl = canonObj.AddComponent<Canon_Controller>();
            canonCtrl.canonInfo = canon;
            canonCtrl.canonInfo.shootPoint = CanonsSpots[i];         
        }
    }

    /// <summary>
    /// Load every canon spot on the current boat
    /// </summary>
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
