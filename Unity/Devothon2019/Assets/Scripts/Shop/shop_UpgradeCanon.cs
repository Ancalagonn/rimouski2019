using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class shop_UpgradeCanon : MonoBehaviour
{
    public int m_canonSelect;
    public Dropdown m_dllType;
    void Start()
    {

    }

    //Pour l'upgrade (changer les stats)
    public void UpgradeCanon()
    {
        if(shop_moneyControl.Transaction(-50))
        {
            PlayerInstance.playerStats.canons[shop_loadShop.btn_select].level++;
        }
    }

    //Changer le type de canon
    public void ChangerType()
    {
        CanonType newType = (CanonType)m_dllType.value;
        int cost = 0;

        switch(newType)
        {
            case CanonType.FlameThrower:
                cost = 40;
                break;
            case CanonType.Normal:
                cost = 40;
                break;
            case CanonType.TripleShot:
                cost = 40;
                break;
        }

        if (shop_moneyControl.Transaction(-cost))
        {
            PlayerInstance.playerStats.canons[shop_loadShop.btn_select].canonType = (CanonType)m_dllType.value;
        }
    }
}
