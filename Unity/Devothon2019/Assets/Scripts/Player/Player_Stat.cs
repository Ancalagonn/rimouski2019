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
    float repairTime = 1;

    public List<TargetPoint> targetPoints = new List<TargetPoint>();

    private void Awake()
    {
        Static_Resources.LoadResources();
        SoundManager.LoadSound();
        playerStats = PlayerInstance.playerStats;
        LoadCanons();
        playerStats.mySelf = this.transform;
        playerMovement = this.GetComponent<Player_Movemement>();
        GenerateTargetPoint();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //for debug purpose
        playerStats = PlayerInstance.playerStats;
        if(repairTime < 0)
        {
            Repair();
            repairTime = 1;
        }
        else
        {
            repairTime -= Time.deltaTime;
        }
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
        PlayerInstance.playerStats.TakeDamage(p_damage);

        if (PlayerInstance.playerStats.currentHp <= 0)
        {
            PlayerInstance.playerStats.currentHp = -500;
            ManageScene.instance.LoadSceneBlack("End_Scene");
            return;
        }

        if (Random.Range(0, 1000) < 1)
        {
            playerStats.RemoveRandomMember();
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
                case CanonType.TirTriple:
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
                case CanonType.LanceFlammes:
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

    public TargetPoint GetClosestTarget(Transform p_enemy)
    {
        TargetPoint closest = null;

        float minDist = float.MaxValue;

        foreach (var item in targetPoints)
        {
            float thisDistance = Vector3.Distance(item.targetPoint.position, p_enemy.position);

            if (item.enemy == p_enemy)
            {
                return item;
            }

            if (thisDistance < minDist && item.enemy == null)
            {
                minDist = thisDistance;
                closest = item;
            }

        }

        if(closest != null)
        {
            closest.AssignEnemy(p_enemy);
        }

        return closest;
    }

    public void DeleteTargetReference(Transform target)
    {
        foreach (var item in targetPoints)
        {
            if(item.targetPoint == target)
            {
                item.enemy = null;
            }
        }
    }

    private void GenerateTargetPoint()
    {
        targetPoints = new List<TargetPoint>();

        float offset = 25.5f;

        GameObject point = new GameObject();
        point.transform.SetParent(transform);
        point.transform.localPosition = new Vector2(-offset, 0);
        point.name = "Left Point";
        targetPoints.Add(new TargetPoint(point.transform, Quaternion.Euler(new Vector3(0, 0, 0))));

        point = new GameObject();
        point.transform.SetParent(transform);
        point.transform.localPosition = new Vector2(offset, 0);
        point.name = "Right Point";
        targetPoints.Add(new TargetPoint(point.transform, Quaternion.Euler(new Vector3(0, 0, 0))));

        point = new GameObject();
        point.transform.SetParent(transform);
        point.transform.localPosition = new Vector2(0, offset);
        point.name = "Top Point";
        targetPoints.Add(new TargetPoint(point.transform, Quaternion.Euler(new Vector3(0, 0, 90))));

        point = new GameObject();
        point.transform.SetParent(transform);
        point.transform.localPosition = new Vector2(0, -offset);
        point.name = "Bot Point";
        targetPoints.Add(new TargetPoint(point.transform, Quaternion.Euler(new Vector3(0, 0, -90))));


        float diagonal = 0.7071f * offset;

        point = new GameObject();
        point.transform.SetParent(transform);
        point.transform.localPosition = new Vector2(-diagonal, diagonal);
        point.name = "Left Up Point";
        targetPoints.Add(new TargetPoint(point.transform, Quaternion.Euler(new Vector3(0, 0, 45))));

        point = new GameObject();
        point.transform.SetParent(transform);
        point.transform.localPosition = new Vector2(diagonal, diagonal);
        point.name = "Right Up Point";
        targetPoints.Add(new TargetPoint(point.transform, Quaternion.Euler(new Vector3(0, 0, -45))));

        point = new GameObject();
        point.transform.SetParent(transform);
        point.transform.localPosition = new Vector2(-diagonal, -diagonal);
        point.name = "Left Bot Point";
        targetPoints.Add(new TargetPoint(point.transform, Quaternion.Euler(new Vector3(0, 0, -45))));

        point = new GameObject();
        point.transform.SetParent(transform);
        point.transform.localPosition = new Vector2(diagonal, -diagonal);
        point.name = "Rigt Bot Point";
        targetPoints.Add(new TargetPoint(point.transform, Quaternion.Euler(new Vector3(0, 0, 45))));
    }
    
}
