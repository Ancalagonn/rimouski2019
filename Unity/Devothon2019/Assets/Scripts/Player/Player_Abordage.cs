using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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
        else
        {
            if(!gameObject.activeSelf)
            {
                Debug.Log("Emergency Change state");
                ChangeGameobjectState(true);
            }
        }
    }

    //Methode qui obtient le bateau qui se stue dans le radius du bateau pour l'aborder
    void GetBoardableShip()
    {   
        //On obtient le nombre d'ennemies et leur collider dans une liste    
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, BoardingRadius);

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

                    if (boardingShip.enemySize == EnemySize.Big) {
                        this.boardingTime = 0;
                    }
                    else {
                        this.boardingTime = 5;
                    }

                    this.gameObject.GetComponent<Player_Movemement>().enabled = false;
                }
            }
        }

    }

    void BoardShip()
    {
        this.boardingShip.StopAllCoroutines();
        isBoarding = false;

        //On attribue l'argent en fonction du type de bateau ennemie
        if (boardingShip.enemySize == EnemySize.Small) {
            PlayerInstance.playerCash += Static_Resources.SmallBoatValue;
            
            SoundManager.Play("Abordage", Vector3.zero);

            this.gameObject.GetComponent<Player_Movemement>().enabled = true;
            Destroy(boardingShip.gameObject);
            boardingShip = null;
        }
        else if (boardingShip.enemySize == EnemySize.Big) {
            SceneManager.LoadScene("BoatScene", LoadSceneMode.Additive);
            Scene boatScene = SceneManager.GetSceneByName("BoatScene");
            SceneManager.sceneUnloaded += sceneUnloaded;

            ChangeGameobjectState(false);
        }
    }

    private void OnDestroy() {
        SceneManager.sceneUnloaded -= sceneUnloaded;
    }

    private void sceneUnloaded(Scene p_scene) {
        if (this == null) {
            return;
        }

        if (boardingShip != null) {
            Destroy(boardingShip.gameObject);
        }
        boardingShip = null;

        ChangeGameobjectState(true);
    }

    private void ChangeGameobjectState(bool state)
    {
        foreach (GameObject g in SceneManager.GetActiveScene().GetRootGameObjects())
        {
            g.SetActive(state);
        }

        this.gameObject.GetComponent<Player_Movemement>().enabled = true;
    }
}
