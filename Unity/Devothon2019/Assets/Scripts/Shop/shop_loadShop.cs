using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class shop_loadShop : MonoBehaviour
{
    public static shop_loadShop instance;

    public GameObject addCanonPrefab;

    public GameObject boatLocation;
    public GameObject boatPrefab;
    public GameObject btnPrefab;
    public Dropdown ddl_canonType;

    public Text levelCanon;

    public GameObject m_panelCanon;

    private GameObject boat;

    public static int btn_select;

    public void Awake()
    {
        instance = this;
    }

    void Start()
    {
        boat = Instantiate(boatPrefab, boatLocation.transform);
        boat.transform.localScale *= 100;
        //boat.transform.Rotate(new Vector3(0, 180, 0));
        boat.GetComponent<Player_Movemement>().enabled = false;
        boat.GetComponent<Player_Abordage>().enabled = false;
        boat.GetComponent<Animator>().enabled = false;

        for (int i = 0; i < boat.GetComponent<Player_Stat>().CanonsSpots.Count; i++)
        {
            //Big oof
            try {
                boat.GetComponent<Player_Stat>().CanonsSpots[i].transform.GetChild(0).GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
            } catch (Exception e) {
                //On s'en calice
            }

            if (PlayerInstance.playerStats.canons[i] == null) {
                GameObject btn = Instantiate(addCanonPrefab, boat.GetComponent<Player_Stat>().CanonsSpots[i].transform);
                btn.transform.localScale = new Vector3(0.01f, 0.01f, 0.02f);
                btn.transform.localPosition = new Vector3(0,0,-1);
                btn.GetComponentInChildren<Text>().text = price.COUT_CANON_BASE + "$";
                btn.GetComponent<shop_btnClick>().id = i;
            } else {
                GameObject btn = Instantiate(btnPrefab, boat.GetComponent<Player_Stat>().CanonsSpots[i].transform);
                btn.transform.localScale = new Vector3(0.014f, 0.004f, 0.02f);
                btn.transform.localPosition = new Vector3(0,0,-1);
                btn.transform.Rotate(Vector3.forward, 90);
                btn.GetComponent<shop_btnClick>().id = i;
            }
        }
    }

    public void QuitShop() {
        ManageScene.instance.LoadSceneBlack("CrewAffectation_Scene");
    }

    public static void LoadPanel(GameObject caller = null)
    {
        instance.m_panelCanon.SetActive(true);

        if (PlayerInstance.playerStats.canons[btn_select] != null)
        {
            Canon selectedCanon = PlayerInstance.playerStats.canons[btn_select];
            instance.ddl_canonType.value = (int)selectedCanon.canonType;
            instance.levelCanon.text = "Niveau : " + PlayerInstance.playerStats.canons[btn_select].level;
        }
        else
        {
            //Lors de l'achat d'un canon
            if(shop_moneyControl.Transaction(-price.COUT_CANON_BASE))
            {
                //Créé un canon de base
                PlayerInstance.playerStats.canons[btn_select] = new Canon();

                GameObject btn = Instantiate(instance.btnPrefab, instance.boat.GetComponent<Player_Stat>().CanonsSpots[btn_select].transform);
                btn.transform.localScale = new Vector3(0.014f, 0.004f, 0.02f);
                btn.transform.localPosition = new Vector3(0,0,-1);
                btn.transform.Rotate(Vector3.forward, 90);
                btn.GetComponent<shop_btnClick>().id = btn_select;
                btn.GetComponent<Image>().color = Color.red;

                instance.levelCanon.text = "Niveau : 1";

                Destroy(caller);
            } else {
                instance.m_panelCanon.SetActive(false);
            }
        }
    }
}
