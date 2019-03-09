using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CrewAffectation : MonoBehaviour
{
    Text text_Rotation;
    Text text_Deplacement;
    Text text_Tir;
    Text text_Reparation;
    Text text_NbTotal;

    int total;

    // Start is called before the first frame update
    void Start()
    {
        text_Rotation = GameObject.Find("Text_NbRotation").GetComponent<Text>();
        text_Deplacement = GameObject.Find("Text_NbDeplacement").GetComponent<Text>();
        text_Tir = GameObject.Find("Text_NbTir").GetComponent<Text>();
        text_Reparation = GameObject.Find("Text_NbReparation").GetComponent<Text>();
        text_NbTotal = GameObject.Find("Text_NbTotal").GetComponent<Text>();

        UpdateCrewAffectation();
        CanRemoveCrew();
    }

    // Update is called once per frame
    void Update()
    {
        CanRemoveCrew();
    }

    private void CanRemoveCrew()
    {
        GameObject go;
        go = GameObject.Find("Panel_Rotation").transform.Find("Rotation-").gameObject;

        if (text_Rotation.text == "1")
        {
            
            if(go != null)
            {
                go.SetActive(false);
            }
            
        }
        else
        {
            go.SetActive(true);
        }

        go = GameObject.Find("Panel_Deplacement").transform.Find("Deplacement-").gameObject;
        if (text_Deplacement.text == "1")
        {
            if (go != null)
            {
                go.SetActive(false);
            }
        }
        else
        {
            go.SetActive(true);
        }

        go = GameObject.Find("Panel_Tir").transform.Find("Tir-").gameObject;
        if (text_Tir.text == "1")
        {
            if (go != null)
            {
                go.SetActive(false);
            }
        }
        else
        {
            go.SetActive(true);
        }

        go = GameObject.Find("Panel_Reparation").transform.Find("Reparation-").gameObject;
        if (text_Reparation.text == "1")
        {
            if (go != null)
            {
                go.SetActive(false);
            }
        }
        else
        {
            go.SetActive(true);
        }
    }

    public void CrewAddOrRemove(GameObject boutton)
    {
        if(total != 0)
        {
            switch (boutton.name)
            {
                case "Rotation-":
                    PlayerInstance.playerStats.rotationSpeed.RemoveCrewMember();
                    break;
                case "Rotation+":
                    PlayerInstance.playerStats.rotationSpeed.AddCrewMember();
                    break;
                case "Deplacement-":
                    PlayerInstance.playerStats.moveSpeed.RemoveCrewMember();
                    break;
                case "Deplacement+":
                    PlayerInstance.playerStats.moveSpeed.AddCrewMember();
                    break;
                case "Tir-":
                    PlayerInstance.playerStats.shotCooldown.RemoveCrewMember();
                    break;
                case "Tir+":
                    PlayerInstance.playerStats.shotCooldown.AddCrewMember();
                    break;
                case "Reparation-":
                    PlayerInstance.playerStats.repairSpeed.RemoveCrewMember();
                    break;
                case "Reparation+":
                    PlayerInstance.playerStats.repairSpeed.AddCrewMember();
                    break;
            }

            UpdateCrewAffectation();
        }
        
    }

    private void UpdateCrewAffectation()
    {
        total = PlayerInstance.playerStats.crewMembers;

        text_Rotation.text = PlayerInstance.playerStats.rotationSpeed.crewAssigned.ToString();
        total -= int.Parse(text_Rotation.text);
        text_Deplacement.text = PlayerInstance.playerStats.moveSpeed.crewAssigned.ToString();
        total -= int.Parse(text_Deplacement.text);
        text_Tir.text = PlayerInstance.playerStats.shotCooldown.crewAssigned.ToString();
        total -= int.Parse(text_Tir.text);
        text_Reparation.text = PlayerInstance.playerStats.repairSpeed.crewAssigned.ToString();
        total -= int.Parse(text_Reparation.text);

        text_NbTotal.text = "Marins restants : " + total;
    }
}
