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
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Pour la sélection des canons
    public void OnCanonClick(int id)
    {

    }

    //Pour l'upgrade (changer les stats)
    public void UpgradeCanon()
    {
        Canon newCanon = new Canon();

        //newCanon.
    }

    //Changer le type de canon
    public void ChangerType()
    {
        Canon newCanon = new Canon();
        newCanon.canonType = (CanonType)m_dllType.value;
    }
}
