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

    // Start is called before the first frame update
    void Start()
    { 
        text_Rotation = GameObject.Find("Text_NbRotation").GetComponent<Text>();
        text_Deplacement = GameObject.Find("Text_NbDeplacement").GetComponent<Text>();
        text_Tir = GameObject.Find("Text_NbTir").GetComponent<Text>();
        text_Reparation = GameObject.Find("Text_NbReparation").GetComponent<Text>();
        Debug.Log(text_Rotation);
        text_Rotation.text = PlayerInstance.playerStats.rotationSpeed.crewAssigned.ToString();
        text_Deplacement.text = PlayerInstance.playerStats.moveSpeed.crewAssigned.ToString();
        text_Tir.text = PlayerInstance.playerStats.shotCooldown.crewAssigned.ToString();
        text_Reparation.text = PlayerInstance.playerStats.repairSpeed.crewAssigned.ToString();

        CanRemoveCrew();
    }

    // Update is called once per frame
    void Update()
    {
        CanRemoveCrew();
    }

    private void CanRemoveCrew()
    {
        if(text_Rotation.text == "1")
        {
            GameObject.Find("Rotation-").SetActive(false);
        }
        if (text_Deplacement.text == "1")
        {
            GameObject.Find("Deplacement-").SetActive(false);
        }
        if (text_Tir.text == "1")
        {
            GameObject.Find("Tir-").SetActive(false);
        }
        if (text_Reparation.text == "1")
        {
            GameObject.Find("Reparation-").SetActive(false);
        }
    }
}
