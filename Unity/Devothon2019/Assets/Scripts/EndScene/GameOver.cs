using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;  

public class GameOver : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if(PlayerInstance.playerStats.currentHp < -499)
        {
            SoundManager.Play("MortJoueur", Vector3.zero);
            SoundManager.Play("GameOver", Vector3.zero);
            GameObject.Find("Title").GetComponent<Text>().text = "Votre équipage a été anéantie.\n La Nouvelle-France appartient désormais à l'Angleterre.\nVous avez échoué.";

        }
        else if(PlayerInstance.playerStats.currentHp > 499)
        {
            GameObject.Find("Title").GetComponent<Text>().text = "Du beau texte de gagnant\n";
        }
    }

    public void RetournerAuMenu() {
        ManageScene.instance.LoadSceneBlack("Main_Scene");
    }




    // Update is called once per frame
    void Update()
    {
        
    }
}
