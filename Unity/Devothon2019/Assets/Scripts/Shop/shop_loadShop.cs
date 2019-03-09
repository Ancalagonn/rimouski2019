using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class shop_loadShop : MonoBehaviour
{
    public GameObject boatLocation;
    public GameObject boatPrefab;
    public GameObject btnPrefab;

    private GameObject boat;

    public static int btn_select;

    void Start()
    {
        boat = Instantiate(boatPrefab, boatLocation.transform);
        boat.transform.localScale *= 100;

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

        if (PlayerInstance.playerStats.canons[btn_select] != null)
        {
            Debug.Log("C'est cancer sur un temps");
        }
        else
        {
            PlayerInstance.playerStats.canons[btn_select] = new Canon();
            Debug.Log("Canon créé");
        }
    }
}
