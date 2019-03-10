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
       SoundManager.Play("MusiqueBoutique", Vector3.zero);
    }


    public static bool Transaction(int p_money)
    {
        
        
        if (PlayerInstance.playerCash + p_money < 0)
        {
            //il n'y a rien qui se fait puisqu'on serait en négatif
            SoundManager.Play("ClickBtn", Vector3.zero);
            return false;
        }
        else
        {
            PlayerInstance.playerCash += p_money;
            SoundManager.Play("Achat", Vector3.zero);

            //On affiche le changement de prix
            return true;
        }
    }
}
