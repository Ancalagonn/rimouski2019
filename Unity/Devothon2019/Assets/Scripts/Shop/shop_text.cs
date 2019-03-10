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
    public Text levelCoque;
    public Text hp;

    public Dropdown ddl_typeCanon;

    public static int repairPrice = 0;

    private void Start() {
        repairPrice = (int)(PlayerInstance.playerStats.maxHp - PlayerInstance.playerStats.currentHp);

        upgradeCoque.text = "Améliorer la coque (" + price.UPGRADE_COQUE + "$)";
        
        upgradeCanon.text = "Améliorer le canon (" + price.UPGRADE_CANON + "$)";
    }

    private void Update() {
        CanonType newType = (CanonType)ddl_typeCanon.value;
        int cost = 0;

        switch(newType)
        {
            case CanonType.LanceFlammes:
                cost = price.CANON_FLAME;
                break;
            case CanonType.Normal:
                cost = price.CANON_NORMAL;
                break;
            case CanonType.TirTriple:
                cost = price.CANON_TRIPLE;
                break;
            default:
                cost = 40;
                break;
        }

        changeType.text = "Changer type (" + cost + "$)";

        addCrewMember.text = "Ajouter un membre à l'équipage\n("+PlayerInstance.playerStats.crewMembers+" présentement) ("+price.ADD_CREW+"$)";
        money.text = "Banque : " + PlayerInstance.playerCash + "$";
        levelCoque.text = "Niveau : " + ((PlayerInstance.playerStats.maxHp / 10) - 49);
        hp.text = "HP : " + (int)PlayerInstance.playerStats.currentHp + "/" + PlayerInstance.playerStats.maxHp;
        repairCoque.text = "Réparer la coque (" + repairPrice + "$)";
    }

}