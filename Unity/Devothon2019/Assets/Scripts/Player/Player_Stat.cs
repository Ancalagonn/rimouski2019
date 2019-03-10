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
        Repair();
    }

    private void Repair()
    {
        PlayerInstance.playerStats.Repair();
    }

    /// <summary>
    /// Take damage (Use with SendMessage)
    /// </summary>
    /// <param name="p_damage"></param>
    public void TakeDamage(float p_damage)
    {
        Debug.Log(PlayerInstance.playerStats.crewMembers);
        PlayerInstance.playerStats.TakeDamage(p_damage);

        Debug.Log(PlayerInstance.playerStats.crewMembers);
        if (PlayerInstance.playerStats.crewMembers > 4)
        {
            Debug.Log(PlayerInstance.playerStats.crewMembers);
            if (Random.Range(0, 100) < 5)
            {
                PlayerInstance.playerStats.crewMembers--;
                switch (Random.Range(1, 4))
                {
                    case 1:
                        PlayerInstance.playerStats.moveSpeed.RemoveCrewMember();
                        break;
                    case 2:
                        PlayerInstance.playerStats.rotationSpeed.RemoveCrewMember();
                        break;
                    case 3:
                        PlayerInstance.playerStats.shotCooldown.RemoveCrewMember();
                        break;
                    case 4:
                        PlayerInstance.playerStats.repairSpeed.RemoveCrewMember();
                        break;

                }
            }
        }
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
                case CanonType.TripleShot:
                case CanonType.Normal:
                    if (canon.canonType == CanonType.Normal)
                        canonObj = Instantiate(Static_Resources.defaultCanon);
                    else
                        canonObj = Instantiate(Static_Resources.tripleCanon);

                    if (canon.canonball == null)
                        canon.canonball = Static_Resources.defaultCanonball;

                    canonObj.name = "Canon" + i;
                    canonObj.transform.SetParent(CanonsSpots[i]);
                    canonObj.transform.position = new Vector3(0, 0, -1);
                    canonObj.transform.localPosition = new Vector3(0, 0, -1);
                    canonObj.transform.rotation = CanonsSpots[i].rotation;

                    Canon_Controller canonCtrl = canonObj.AddComponent<Canon_Controller>();
                    canonCtrl.canonInfo = canon;
                    canonCtrl.canonInfo.shootPoint = CanonsSpots[i];

                    break;
                case CanonType.FlameThrower:
                    canonObj = Instantiate(Static_Resources.flameThrower);
                    canonObj.name = "CanonFlame" + i;
                    canonObj.transform.SetParent(CanonsSpots[i]);
                    canonObj.transform.position = new Vector3(0, 0, -1);
                    canonObj.transform.localPosition = new Vector3(0, 0, -1);
                    canonObj.transform.rotation = CanonsSpots[i].rotation;


                    if (canon.canonball == null)
                        canon.canonball = Static_Resources.flameEffect;

                    canon.baseCooldown = 5f;

                    Flame_Controller flameCtrl = canonObj.AddComponent<Flame_Controller>();
                    flameCtrl.canonInfo = canon;
                    flameCtrl.canonInfo.shootPoint = CanonsSpots[i];

                    break;
            }
            
      
        }
    }

    /// <summary>
    /// Load every canon spot on the current boat
    /// </summary>
    void LoadCanonsSpots()
    {
        CanonsSpots = new List<Transform>();
        DestroyCanons();
        foreach (Transform child in CanonsSpotsParent)
        {
            CanonsSpots.Add(child);

        }
    }

    public void DestroyCanons()
    {
        foreach (Transform child in CanonsSpotsParent)
            foreach (Transform c2 in child)
                Destroy(c2.gameObject);
        
    }
    
}
