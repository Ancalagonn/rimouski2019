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

    public Boat_Stats(Stats p_moveSpeed, Stats p_rotationSpeed, Stats p_shotCooldown, Stats p_repairSpeed)
    {
        moveSpeed = p_moveSpeed;
        rotationSpeed = p_rotationSpeed;
        shotCooldown = p_shotCooldown;
        repairSpeed = p_repairSpeed;

        //canons = new List<Canon>(maxCanons) { new Canon(), new Canon(), new Canon(), new Canon(), new Canon(), new Canon() };
        canons = new List<Canon>(maxCanons) { null, new Canon(), null, null, new Canon(), null };
    }       
}

[System.Serializable]
public class Stats
{
    public float value = 0;
    public int crewAssigned = 1;

    public Stats(float p_value, int p_crewAssigned)
    {
        value = p_value;
        crewAssigned = p_crewAssigned;
    }

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





