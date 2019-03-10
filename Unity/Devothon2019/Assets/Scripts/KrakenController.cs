using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KrakenController : MonoBehaviour
{
    private Transform player;
    public GameObject ouragan;
    public GameObject storm;
    bool isStorming = false;

    float timeBeforeOuragan = 2;

    public int hp = 1000;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player_Stat>().transform;
       
    }

    public void TakeDamage(float p_damage)
    {
        hp -= (int)p_damage;
    }

    // Update is called once per frames
    void Update()
    {
        if(timeBeforeOuragan < 0)
        {
            Instantiate(ouragan, this.transform.GetChild(1).transform);
            timeBeforeOuragan = Random.Range(1f,3f);
        }
        else
        {
            timeBeforeOuragan -= Time.deltaTime;
        }

        if(hp < 500 && !isStorming)
        {
            isStorming = true;
            Instantiate(storm);
        }

        if(hp <= 0)
        {
            PlayerInstance.playerStats.currentHp = 500;
            ManageScene.instance.LoadSceneBlack("End_Scene");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            PlayerInstance.playerStats.TakeDamage(50);
            hp -= 30;
        }
    }


}
