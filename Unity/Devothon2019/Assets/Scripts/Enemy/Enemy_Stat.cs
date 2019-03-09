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

    

    private void Awake()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        enemyStats.maxHp = 50;
        enemyStats.currentHp = enemyStats.maxHp;
        enemyStats.mySelf = this.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyStats.isDead())
            Destroy(gameObject);
    }

    public void TakeDamage(float p_damage)
    {
        enemyStats.TakeDamage(p_damage);
        //Debug.Log(gameObject.name + " : " + enemyStats.currentHp);
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
