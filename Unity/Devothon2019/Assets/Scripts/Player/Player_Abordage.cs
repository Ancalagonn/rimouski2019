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

    [SerializeField]
    private LayerMask layerMaskBoarding;

    private Enemy_Stat boardingShip;

    // Update is called once per frame
    void Update()
    {
        //Si le joueur appui sur F
        if (Input.GetKeyDown(boardingKey))
        {
            Debug.Log("KeyDown");
            //On tente d'aborder
            GetBoardableShip();
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
                BoardShip();
            }
        }
    }

    //Methode qui obtient le bateau qui se stue dans le radius du bateau pour l'aborder
    void GetBoardableShip()
    {   
        //On obtient le nombre d'ennemies et leur collider dans une liste    
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, BoardingRadius, 1 << layerMaskBoarding);

        foreach (var item in colliders)
        {
            if(item.CompareTag("Enemy"))
            {
                Enemy_Stat stats = item.GetComponent<Enemy_Stat>();

                if (boardingShip != null)
                    continue;

                if(stats.isDying)
                {
                    isBoarding = true;
                    boardingShip = stats;

                    this.gameObject.GetComponent<Player_Movemement>().enabled = false;
                }
            }
        }

    }

    void BoardShip()
    {
        isBoarding = false;
        boardingTime = 5;

        //On attribue l'argent en fonction du type de bateau ennemie
        if (boardingShip.enemySize == EnemySize.Small)
            PlayerInstance.playerCash += Static_Resources.SmallBoatValue;
        else if (boardingShip.enemySize == EnemySize.Big)
            PlayerInstance.playerCash += Static_Resources.BigBoatValue;

        this.gameObject.GetComponent<Player_Movemement>().enabled = true;

        Destroy(boardingShip.gameObject);
        boardingShip = null;
    }
}
