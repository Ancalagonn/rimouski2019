using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;   

public class shop_moneyControl : MonoBehaviour
{
    public static shop_moneyControl instance;

    public Text moneyDisplay;
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        AfficherArgent();
    }


    public static void AfficherArgent()
    {
        instance.moneyDisplay.text = "Argent : " + PlayerInstance.playerCash.ToString() + " écu ";
    }

    public static bool Transaction(int p_money)
    {
        
        if (PlayerInstance.playerCash + p_money < 0)
        {
            //il n'y a rien qui se fait puisqu'on serait en négatif
            return false;
        }
        else
        {
            PlayerInstance.playerCash += (int)p_money;
            if(PlayerInstance.playerCash < 0)
            {
                //met son ici
            }

            //On affiche le changement de prix
            AfficherArgent();
            return true;
        }
    }
}
