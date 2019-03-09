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
        if(shop_moneyControl.Transaction(-price.UPGRADE_CANON))
        {
            PlayerInstance.playerStats.canons[shop_loadShop.btn_select].level++;
        }
    }

    //Changer le type de canon
    public void ChangerType()
    {
        CanonType newType = (CanonType)m_dllType.value;

        if (newType == PlayerInstance.playerStats.canons[shop_loadShop.btn_select].canonType) {
            return;
        }

        int cost = 0;

        switch(newType)
        {
            case CanonType.FlameThrower:
                cost = price.CANON_FLAME;
                break;
            case CanonType.Normal:
                cost = price.CANON_NORMAL;
                break;
            case CanonType.TripleShot:
                cost = price.CANON_TRIPLE;
                break;
        }

        if (shop_moneyControl.Transaction(-cost))
        {
            PlayerInstance.playerStats.canons[shop_loadShop.btn_select].canonType = (CanonType)m_dllType.value;
        }
    }
}
