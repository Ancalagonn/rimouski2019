using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Abordage : MonoBehaviour
{
    //F sert a faire l'aordage d'un ennemi proche
    public KeyCode boardingKey = KeyCode.F;
    public float BoardingRadius = 5;
    //Si le player est en cours d'abordage
    public bool isBoarding = false;
    //Temps d'abodage
    float boardingTime = 5;
    //Le bateau qui se fait aborder
    Collider2D[] boardedShip = new Collider2D[1];

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //Si le joueur appui sur F
        if (Input.GetKey(boardingKey))
        {
            //On tente d'aborder
            Boarding();
        }

        //Si le player est en cours d'abordage
        if(isBoarding)
        {
            //On reduit le temps restant de l'abordage
            boardingTime -= Time.deltaTime;

            //Si le temps est fini, on detruit le bateau abordé
            //On désactive l'abordage et on reset le temps d'abordage
            if(boardingTime < 0)
            {
                Destroy(boardedShip[0].transform.gameObject);
                isBoarding = false;
                boardingTime = 5;
            }
        }
    }

    //Methode qui obtient le bateau qui se stue dans le radius du bateau pour l'aborder
    void Boarding()
    {
        //On définit le mask des ennemies
        ContactFilter2D cf = new ContactFilter2D();
        cf.layerMask = LayerMask.NameToLayer("Ennemy");
        
        //On obtient le nombre d'ennemies et leur collider dans une liste
        float nbEnnemy = Physics2D.OverlapCircle(this.transform.position, BoardingRadius, cf , boardedShip);

        //Si un bateau est présent dans le radius
        if(boardedShip[0] != null)
        {
            //On aborder le bateau et on commence sa desctruction
            BoardShip();
        }
    }

    void BoardShip()
    {
        if(Enemy_Stat.EnnemySize)
        PlayerInstance.playerCash += Static_Resources.
        //On active le mode d'abordage pour le bateau
        isBoarding = true;
    }
}
