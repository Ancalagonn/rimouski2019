using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Introduction : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Narrator.SayTextStatic(false, "Notre histoire se déroule au lendemain de la capitulation de Ville-Marie.");
        Narrator.SayTextStatic(false, "Quatres aventuriers français sont déterminés à libérer la Nouvelle-France de l'envahisseur Anglais, qui tente éperdumment de les assimiler.");
        Narrator.SayTextStatic(false, "Notre capitaine François et ses trois acolytes Pierre, Jacques et Charles, longeront alors la rive du fabuleux Fleuve St-Laurent afin de libérer les grandes villes de la colonie.");
        Narrator.SayTextStatic(false, "Leur aventure ne se terminera pas avant une traversée complète de l'Atlantique afin d'anéantir totalement l'Angleterre.");
        StartCoroutine(ChangeScene());
    }

    private IEnumerator ChangeScene() {
        yield return new WaitForSeconds(1f);
        yield return new WaitUntil(() => !Narrator.isTalking);
        Debug.Log("Changement de scène");
    }
}
