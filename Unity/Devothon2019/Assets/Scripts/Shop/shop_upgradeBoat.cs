using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shop_upgradeBoat : MonoBehaviour
{
    
    void Start()
    {
        
    }

    public void UpgradeCoque()
    {
        if(shop_moneyControl.Transaction(-100))
        {
            PlayerInstance.playerStats.maxHp += 10;
        }
    }

    public void ReparerCoque()
    {
        if(shop_moneyControl.Transaction(-100))
        {
            PlayerInstance.playerStats.currentHp = PlayerInstance.playerStats.maxHp;
        }
    }

    public void AjouterCrew()
    {
        PlayerInstance.playerStats.crewMembers++;
    }
}
