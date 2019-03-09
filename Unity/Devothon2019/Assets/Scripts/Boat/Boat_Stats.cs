using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Boat_Stats
{
    public float maxHp;
    [HideInInspector]
    public float currentHp;

    public Stats moveSpeed;
    public Stats rotationSpeed;

    public Stats shotCooldown;
    public Stats repairSpeed;

    public List<Canon> canons;

    public int crewMembers;
    public int maxCanons;

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

    }       
}

[System.Serializable]
public class Stats
{
    public float value = 0;
    public int crewAssigned = 1;

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





