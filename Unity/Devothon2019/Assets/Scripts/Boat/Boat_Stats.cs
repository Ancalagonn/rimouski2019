using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Boat_Stats
{
    public float maxHp;
    public float currentHp;

    public Stats moveSpeed = new Stats();
    public Stats rotationSpeed = new Stats();

    public Stats shotCooldown = new Stats();
    public Stats repairSpeed = new Stats();

    public List<Canon> canons;

    public int crewMembers = 4;
    public int maxCanons = 6;

    int lastHpStade = 5;

    [HideInInspector]
    public Transform mySelf;

    public float GetHp()
    {
        return currentHp;
    }

    public void Repair()
    {

        currentHp += repairSpeed.crewAssigned;

        if(currentHp > maxHp)
        {
            currentHp = maxHp;
        }

        CheckForFire();
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

        if (lastHpStade > currentHpStade)
        {
            int hpStadeDiff = lastHpStade - currentHpStade;

            for (int i = 0; i < hpStadeDiff; i++)
            {
                //float tileWidth = (float)tileSet[0].renderer.bounds.size.x;
                SpriteRenderer renderer = mySelf.GetComponent<SpriteRenderer>();

                float tileWidth = renderer.bounds.size.x / 2;
                float tileheight = renderer.bounds.size.y / 2;

                Vector3 pos = new Vector3(Random.Range(-tileWidth, tileWidth), Random.Range(-tileheight, tileheight), -1);
                GameObject fire = GameObject.Instantiate(Static_Resources.fireEffect);
                fire.transform.SetParent(mySelf);
                fire.transform.localPosition = pos;
            }

            lastHpStade = currentHpStade;
        }

    }

    private void CheckForFire()
    {
        int currentHpStade = (int)(PercentHpLeft() / 20f);

        if (lastHpStade < currentHpStade)
        {
            lastHpStade = currentHpStade;
            FireEffect[] flames = mySelf.GetComponentsInChildren<FireEffect>();
            if (flames.Length > 0)
            {
                GameObject.Destroy(flames[0].gameObject);
                Debug.Log("Destroy one flame");
            }
        }
        else if (currentHp == maxHp)
        {
            FireEffect[] flames = mySelf.GetComponentsInChildren<FireEffect>();
            foreach (var item in flames)
            {
                GameObject.Destroy(item.gameObject);
            }
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

        canons = new List<Canon>(maxCanons);


        for (int i = 0; i < maxCanons; i++)
        {

            canons.Add(null);

        }



        canons[maxCanons/4] = new Canon();
        canons[maxCanons/4 + maxCanons/2] = new Canon();
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





