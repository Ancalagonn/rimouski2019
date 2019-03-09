using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Introduction : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Narrator.SayTextStatic(false, "Au lendemain de la capitulation de Ville-Marie, quatres aventuriers français sont déterminés à libérer la Nouvelle-France de l'envahisseur Anglais.");
        Narrator.SayTextStatic(false, "Notre capitaine François et ses trois acolytes Pierre, Jacques et Charles, longeront la rive du fabuleux Fleuve St-Laurent, voie maritime importante de la colonie, afin de libérer les grandes villes au prise des Anglais.");
        Narrator.SayTextStatic(false, "Leur aventure ne se terminera pas avant une traversée de l'Atlantique pour anéantir totalement l'Angleterre.");
    }
}
