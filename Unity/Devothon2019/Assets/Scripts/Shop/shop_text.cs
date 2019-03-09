using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class shop_text : MonoBehaviour
{
    public Text money;

    public Text upgradeCoque;
    public Text repairCoque;
    public Text addCrewMember;
    
    public Text changeType;
    public Text upgradeCanon;

    public Dropdown ddl_typeCanon;

    private void Start() {
        upgradeCoque.text = "Améliorer la coque (" + price.UpgradeCoque + "$)";
        repairCoque.text = "Réparer la coque (" + price.RepairCoque + "$)";
        addCrewMember.text = "Ajouter un membre à l'équipage\n("+PlayerInstance.playerStats.crewMembers+" présentement) ("+price.AddCrew+"$)";
        upgradeCanon.text = "Améliorer le canon (" + price.UpgradeCanon + "$)";
    }

    private void Update() {
        CanonType newType = (CanonType)ddl_typeCanon.value;
        int cost = 0;

        switch(newType)
        {
            case CanonType.FlameThrower:
                cost = price.Canon_Flame;
                break;
            case CanonType.Normal:
                cost = price.Canon_Normal;
                break;
            case CanonType.TripleShot:
                cost = price.Canon_Triple;
                break;
            default:
                cost = 40;
                break;
        }

        changeType.text = "Changer type (" + cost + "$)";

        money.text = "Banque : " + PlayerInstance.playerCash + "$";
    }

}