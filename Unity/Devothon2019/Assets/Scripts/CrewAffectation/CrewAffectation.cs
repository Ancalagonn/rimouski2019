using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CrewAffectation : MonoBehaviour
{
    //Nombre de marins affectés a chaque fonctions
    Text text_Rotation;
    Text text_Deplacement;
    Text text_Tir;
    Text text_Reparation;
    //Nombre de marins restants a affecté
    Text text_NbTotal;

    //Variable contenant les marins restants
    int total;
    
    void Start()
    {
        //On obtient les champs texte de l'interface
        text_Rotation = GameObject.Find("Text_NbRotation").GetComponent<Text>();
        text_Deplacement = GameObject.Find("Text_NbDeplacement").GetComponent<Text>();
        text_Tir = GameObject.Find("Text_NbTir").GetComponent<Text>();
        text_Reparation = GameObject.Find("Text_NbReparation").GetComponent<Text>();
        text_NbTotal = GameObject.Find("Text_NbTotal").GetComponent<Text>();

        //On met a jour le nombre de marins restants et on l'affiche
        UpdateCrewAffectation();
        //On verifie si les boutons - doivent etre visible ou non
        CanRemoveCrew();
    }

    // Update is called once per frame
    void Update()
    {
        //On verifie si les boutons - doivent etre visible ou non
        CanRemoveCrew();
    }

    //On verifie si les boutons pour retirer des marins sont visibles ou non
    private void CanRemoveCrew()
    {
        //On stocke le gameObject panel à l'aide du panel parent
        GameObject go;


        //Si la valeur est 1
        go = GameObject.Find("Panel_Rotation").transform.Find("Rotation-").gameObject;
        if (text_Rotation.text == "1")
        {
            //On desactive le bouton
            if(go != null)
            {
                go.SetActive(false);
            }
        }
        //Sinon on l'active
        else
        {
            go.SetActive(true);
        }

        //Si la valeur est 1
        go = GameObject.Find("Panel_Deplacement").transform.Find("Deplacement-").gameObject;
        if (text_Deplacement.text == "1")
        {
            //On desactive le bouton
            if (go != null)
            {
                go.SetActive(false);
            }
        }
        //Sinon on l'active
        else
        {
            go.SetActive(true);
        }

        //Si la valeur est 1
        go = GameObject.Find("Panel_Tir").transform.Find("Tir-").gameObject;
        if (text_Tir.text == "1")
        {
            //On desactive le bouton
            if (go != null)
            {
                go.SetActive(false);
            }
        }
        //Sinon on l'active
        else
        {
            go.SetActive(true);
        }

        //Si la valeur est 1
        go = GameObject.Find("Panel_Reparation").transform.Find("Reparation-").gameObject;
        if (text_Reparation.text == "1")
        {
            //On desactive le bouton
            if (go != null)
            {
                go.SetActive(false);
            }
        }
        //Sinon on l'active
        else
        {
            go.SetActive(true);
        }
    }

    //Methode appeler au clique des boutons + ou -
    public void CrewAddOrRemove(GameObject boutton)
    {
        //Si le total est supérieur à 0
        if(total != 0)
        {
            //On peut utiliser tous les boutons
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
        }
        //Sinon, seul les boutons pour retirer des marins est fonctionnels
        else
        {
            switch (boutton.name)
            {
                case "Rotation-":
                    PlayerInstance.playerStats.rotationSpeed.RemoveCrewMember();
                    break;
                case "Deplacement-":
                    PlayerInstance.playerStats.moveSpeed.RemoveCrewMember();
                    break;
                case "Tir-":
                    PlayerInstance.playerStats.shotCooldown.RemoveCrewMember();
                    break;
                case "Reparation-":
                    PlayerInstance.playerStats.repairSpeed.RemoveCrewMember();
                    break;
            }
        }

        //On met a jour le nombre total de matelos restants
        UpdateCrewAffectation();

    }

    //Calcul du nombre de marins restants a affecté et mise a jour du label contenant la valeur
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

        if (total < 1) {
            GameObject.Find("Button_Confirmer").GetComponent<Button>().interactable = true;
        } else {
            GameObject.Find("Button_Confirmer").GetComponent<Button>().interactable = false;
        }
    }

    bool quitting = false;
    public void QuitScene() {
        if (!quitting) {
            quitting = true;
            ManageScene.instance.LoadSceneBlack(Progression.LEVEL_NAMES[Progression.CURRENT_LEVEL]);
        }
    }
}
