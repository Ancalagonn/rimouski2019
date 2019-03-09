using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class shop_loadShop : MonoBehaviour
{
    public static shop_loadShop intance;

    public GameObject boatLocation;
    public GameObject boatPrefab;
    public GameObject btnPrefab;
    public Dropdown ddl_canonType;

    public GameObject m_panelCanon;

    private GameObject boat;

    public static int btn_select;

    public void Awake()
    {
        intance = this;
    }

    void Start()
    {
        boat = Instantiate(boatPrefab, boatLocation.transform);
        boat.transform.localScale *= 100;
        boat.GetComponent<Player_Movemement>().enabled = false;

        for (int i = 0; i < boat.GetComponent<Player_Stat>().CanonsSpots.Count; i++)
        {
            GameObject btn = Instantiate(btnPrefab, boat.GetComponent<Player_Stat>().CanonsSpots[i].transform);
            btn.transform.localScale = new Vector3(0.007f, 0.002f, 0.01f);
            btn.transform.localPosition = new Vector3(0,0,-1);
            btn.transform.Rotate(Vector3.forward, 90);
            btn.GetComponent<shop_btnClick>().id = i;
        }
    }

    public static void LoadPanel()
    {
        intance.m_panelCanon.SetActive(true);

        if (PlayerInstance.playerStats.canons[btn_select] != null)
        {
            Debug.Log("C'est cancer sur un temps");
            Canon selectedCanon = PlayerInstance.playerStats.canons[btn_select];
            intance.ddl_canonType.value = (int)selectedCanon.canonType;
        }
        else
        {
            //Lors de l'achat d'un canon
            shop_moneyControl.Transaction(34);

            //Créé un canon de base
            PlayerInstance.playerStats.canons[btn_select] = new Canon();
            Debug.Log("Canon créé");
        }
    }
}
