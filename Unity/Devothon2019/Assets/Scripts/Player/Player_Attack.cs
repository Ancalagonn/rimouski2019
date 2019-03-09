using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Attack : MonoBehaviour
{
    private Boat_Stats playerStats;
    public KeyCode fireKey = KeyCode.Space;

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

    IEnumerator FireCanons()
    {
        foreach (Canon canon in playerStats.canons)
        {
            if(canon.canFire())
            {
                canon.ResetCooldown();
            }
        }

        yield return null;
    }

    void CanonsCooldown()
    {

    }
}
