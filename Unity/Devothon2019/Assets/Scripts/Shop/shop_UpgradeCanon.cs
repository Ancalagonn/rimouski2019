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
        //PlayerInstance.playerStats.canons[0];
        Canon newCanon = new Canon();
        newCanon.level = 1;
        newCanon.baseCooldown = 10;


        //PlayerInstance.playerStats.canons.Add(newCanon);
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(shop_loadShop.btn_select);
        //Debug.Log(PlayerInstance.playerStats.canons[shop_loadShop.btn_select].level);
    }

    //Pour l'upgrade (changer les stats)
    public void UpgradeCanon()
    {
        //PlayerInstance.playerStats.canons[shop_loadShop.btn_select].level++;

        //newCanon.
    }

    //Changer le type de canon
    public void ChangerType()
    {
        Canon newCanon = new Canon();
        newCanon.canonType = (CanonType)m_dllType.value;
    }
}
