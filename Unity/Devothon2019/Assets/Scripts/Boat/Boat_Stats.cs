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

    int hpStade = 5;

    [HideInInspector]
    public Transform mySelf;

    public float GetHp()
    {
        return currentHp;
    }

    public void TakeDamage(float p_dmg)
    {
        currentHp -= p_dmg;
        SetBoatOnFire();
    }

    public bool isDead()
    {
        return GetHp() <= 0;
    }

    public float PercentHpLeft()
    {
        return currentHp * 100 / maxHp;
    }

    private void SetBoatOnFire()
    {
        int currentHpStade = (int)(PercentHpLeft() / 20f);
        Debug.Log(currentHpStade);

        if (hpStade != currentHpStade)
        {
            int hpStadeDiff = hpStade - currentHpStade;

            for (int i = 0; i < hpStadeDiff; i++)
            {
                Vector3 pos = new Vector3(Random.Range(-mySelf.localScale.x, mySelf.localScale.x), Random.Range(-mySelf.localScale.y, mySelf.localScale.y), -1);
                GameObject fire = GameObject.Instantiate(Static_Resources.fireEffect);
                fire.transform.SetParent(mySelf);
                fire.transform.localPosition = pos;
            }

            hpStade = currentHpStade;
        }

    }

    public Boat_Stats(float p_maxHp, Stats p_moveSpeed, Stats p_rotationSpeed, Stats p_shotCooldown, Stats p_repairSpeed)
    {
        maxHp = p_maxHp;
        currentHp = maxHp;
        moveSpeed = p_moveSpeed;
        rotationSpeed = p_rotationSpeed;
        shotCooldown = p_shotCooldown;
        repairSpeed = p_repairSpeed;

        //canons = new List<Canon>(maxCanons) { new Canon(), new Canon(), new Canon(), new Canon(), new Canon(), new Canon() };

        canons = new List<Canon>(maxCanons);

        for (int i = 0; i < maxCanons; i++)
        {
            canons.Add(new Canon());
        }
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





