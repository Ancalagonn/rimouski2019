using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ouragan : MonoBehaviour
{
    //Direction de l'ouragan, pour determiner si on soustrait le y ou on additionne
    Vector2 Direction;
    //Position de départ de l'ouragan
    Vector2 StartPos;
    //OffSet Random pour varier les déplacements des ouragans
    float offsetY;
    float offsetX;
    //Variable pour savoir si le bateau peu être toucher
    bool CanbeDamaged = true;
    //Temps avant la prochaine phase de dégats
    float invulnerabilityFrame = 0.4f;

    //Variable pour savoir si le bateau peu être toucher
    bool EnemyDamaged = true;
    //Temps avant la prochaine phase de dégats
    float invulnerabilityEnnemy = 0.4f;

    // Start is called before the first frame update
    void Start()
    {
        //On dsactive le sprite renderer pour éviter de le voir durant l'initialisation
        this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
        //On stocke la position de départ de l'ouragan
        StartPos = this.transform.position;
        //On obtient des offset random pour l'ouragan
        offsetY = Random.Range(-10, 10);
        offsetX = Random.Range(-10, 10);
        //On obtient une direction aléatoire qu'on normalize par la suite
        Direction = new Vector2(Random.Range(-100, 100), Random.Range(-100, 100));
        Direction.Normalize();
        
        //On deplace l'ouragan à sa position de départ
        this.transform.position = new Vector2(transform.position.x + offsetX, transform.position.y + StartPos.y + offsetY);
        //On l'affiche
        this.gameObject.GetComponent<SpriteRenderer>().enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        //Déplacement
        Move();

        //Si il ne peut pas être damager
        if(!CanbeDamaged)
        {
            //On reduit le temps de l'invulnérabilité
            invulnerabilityFrame -= Time.deltaTime;

            //Si le temps est fini, l'ouragan peut faire des dégats à nouveaux
            if(invulnerabilityFrame < 0)
            {
                CanbeDamaged = true;
                invulnerabilityFrame = 0.4f;
            }
        }

        //Si il ne peut pas être damager
        if (!EnemyDamaged)
        {
            //On reduit le temps de l'invulnérabilité
            invulnerabilityEnnemy -= Time.deltaTime;

            //Si le temps est fini, l'ouragan peut faire des dégats à nouveaux
            if (invulnerabilityEnnemy < 0)
            {
                EnemyDamaged = true;
                invulnerabilityEnnemy = 0.4f;
            }
        }

        this.transform.Rotate(new Vector3(0, 0, 5));
    }

    //Methode de deplacement
    private void Move()
    {
        //On obtient la position actuel
        Vector2 Pos = this.transform.position;
        
        //On bouge l'ouragan sur l'axe des x
        Pos.x += Direction.x * 0.1f;
        ////En fonction de la direction de y, on soustrait ou aditionne le déplacement vertical
        //if(Direction.y > 0)
        //{
        //    //Positif, on ajoute la valeur de x a la 2 plus les offset de depart vertical
        //    Pos.y = (Mathf.Pow((Pos.x + offsetX) * 0.1f, 2) + StartPos.y + offsetY) * Direction.y;
        //}
        //else
        //{
        //    //Negatif, on retire la valeur de x a la 2 plus les offset de depart vertical
        //    Pos.y = (-Mathf.Pow((Pos.x + offsetX) * 0.1f, 2) + StartPos.y + offsetY) * -Direction.y;
        //}
        Pos.y += 0.1f * Direction.y;

        //On affecte la nouvelle pos à l'ouragan
        this.transform.position = Pos;

        
    }

    //Methode lorsqu'un bateau entre dans le trigger de l'ouragan
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(CanbeDamaged & collision.CompareTag("Player"))
        {
            Debug.Log("calis");
            //collision.gameObject.SendMessage("TakeDamage", 5);
            PlayerInstance.playerStats.TakeDamage(5);
            CanbeDamaged = false;
        }
        else if(collision.CompareTag("Enemy") & EnemyDamaged)
        {
            collision.gameObject.GetComponent<Enemy_Stat>().TakeDamage(5);
            EnemyDamaged = false;
        }
            
    }

    //Methode lorsqu'un bateau est dans le trigger de l'ouragan
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (CanbeDamaged & collision.CompareTag("Player"))
        {
            collision.gameObject.SendMessage("TakeDamage", 50);
            //PlayerInstance.playerStats.TakeDamage(5);
            CanbeDamaged = false;
        }
        else if (collision.CompareTag("Enemy") & EnemyDamaged)
        {
            collision.gameObject.GetComponent<Enemy_Stat>().TakeDamage(5);
            EnemyDamaged = false;
        }
    }
}
