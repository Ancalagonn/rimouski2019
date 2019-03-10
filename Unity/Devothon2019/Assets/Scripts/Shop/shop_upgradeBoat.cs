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
        if(shop_moneyControl.Transaction(-price.UPGRADE_COQUE))
        {
            PlayerInstance.playerStats.maxHp += 10;
        }
    }

    public void ReparerCoque()
    {
        if(shop_moneyControl.Transaction(-shop_text.repairPrice))
        {
            PlayerInstance.playerStats.currentHp = PlayerInstance.playerStats.maxHp;
        }
    }

    public void AjouterCrew()
    {
        if(shop_moneyControl.Transaction(-price.ADD_CREW))
            PlayerInstance.playerStats.crewMembers++;
    }
}
