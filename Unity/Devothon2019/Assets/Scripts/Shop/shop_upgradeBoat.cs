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
        if(shop_moneyControl.Transaction(-price.UpgradeCoque))
        {
            PlayerInstance.playerStats.maxHp += 10;
        }
    }

    public void ReparerCoque()
    {
        if(shop_moneyControl.Transaction(-price.RepairCoque))
        {
            PlayerInstance.playerStats.currentHp = PlayerInstance.playerStats.maxHp;
        }
    }

    public void AjouterCrew()
    {
        if(shop_moneyControl.Transaction(-price.AddCrew))
        PlayerInstance.playerStats.crewMembers++;
    }
}
