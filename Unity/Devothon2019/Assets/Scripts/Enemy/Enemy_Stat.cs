using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Stat : MonoBehaviour
{
    public EnemyType enemyType;
    public EnemySize enemySize;

    public Boat_Stats enemyStats;

    public Transform CanonsSpotsParent;
    public List<Transform> CanonsSpots;

    [HideInInspector]
    public bool isDying = false;

    private void Awake()
    {
        enemyStats = Static_Resources.GenerateBoatStats(enemySize, enemyType);
        LoadCanons();
    }

    // Start is called before the first frame update
    void Start()
    {
        enemyStats.currentHp = enemyStats.maxHp;
        enemyStats.mySelf = this.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isDying && enemyStats.isDead())
        {
            isDying = true;

            GetComponent<Enemy_Attack>().enabled = false;
            GetComponent<Enemy_Movement>().enabled = false;

            //White flags
            PlayerInstance.playerCash += (enemySize == EnemySize.Small) ? Static_Resources.SmallBoatValue : Static_Resources.BigBoatValue;

            Destroy(gameObject, 15f);
        }
    }

    public void TakeDamage(float p_damage)
    {
        enemyStats.TakeDamage(p_damage);
    }

    /// <summary>
    /// Load canons from playerinstance
    /// </summary>
    private void LoadCanons()
    {
        LoadCanonsSpots();

        for (int i = 0; i < enemyStats.canons.Count; i++)
        {
            Canon canon = enemyStats.canons[i];

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
