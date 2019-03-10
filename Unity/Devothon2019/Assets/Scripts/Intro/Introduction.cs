using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Introduction : MonoBehaviour
{
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        SoundManager.Play("MusiqueIntro", Vector3.zero);
        Narrator.SayTextStatic(true, "Notre histoire se déroule au lendemain de la capitulation de Ville-Marie.");
        Narrator.SayTextStatic(true, "Quatre aventuriers français sont déterminés à libérer la Nouvelle-France de l'envahisseur Anglais, qui tente éperdumment de les assimiler.");
        Narrator.SayTextStatic(true, "Notre capitaine François et ses trois acolytes Pierre, Jacques et Charles, longeront alors la rive du fabuleux Saint-Laurent afin de libérer les grandes villes de la colonie.");
        Narrator.SayTextStatic(true, "Leur aventure ne se terminera pas avant une traversée complète de l'Atlantique afin d'anéantir totalement l'Angleterre.");
        StartCoroutine(ChangeScene());
    }

    private void Update() {
        player.transform.position += player.transform.up * Time.deltaTime * 5;   
    }

    private IEnumerator ChangeScene() {
        yield return new WaitForSeconds(1f);
        yield return new WaitUntil(() => !Narrator.isTalking);
        
        ManageScene.instance.LoadSceneBlack("Montreal_Intro");
    }
}
