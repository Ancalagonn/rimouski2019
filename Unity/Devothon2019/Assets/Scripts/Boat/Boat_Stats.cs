using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Boat_Stats
{
    public float maxHp;
    [HideInInspector]
    public float currentHp;

    public Stats moveSpeed = new Stats();
    public Stats rotationSpeed = new Stats();

    public Stats shotCooldown = new Stats();
    public Stats repairSpeed = new Stats();

    public List<Canon> canons;

    public int crewMembers = 4;
    public int maxCanons = 6;

    public float GetHp()
    {
        return currentHp;
    }

    public void TakeDamage(float p_dmg)
    {
        currentHp -= p_dmg;
    }

    public bool isDead()
    {
        return currentHp <= 0;
    }

    public Boat_Stats()
    {
        canons = new List<Canon>(maxCanons);
        for (int i = 0; i < maxCanons; i++)
        {
            canons.Add(null);
        }
    }       
}

[System.Serializable]
public class Stats
{
    public float value = 0;
    public int crewAssigned = 1;

    public Stats()
    {
        crewAssigned = 1;
    }

    public void AddCrewMember()
    {
        crewAssigned++;
    }

    public void RemoveCrewMember()
    {
        if (crewAssigned > 1)
            crewAssigned--;
    }
}





