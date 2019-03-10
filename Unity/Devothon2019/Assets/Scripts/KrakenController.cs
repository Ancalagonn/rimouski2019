using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KrakenController : MonoBehaviour
{
    private Transform player;
    public GameObject ouragan;

    float timeBeforeOuragan = 0;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player_Stat>().transform;
       
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
    }
}
