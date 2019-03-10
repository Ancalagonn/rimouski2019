using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KrakenController : MonoBehaviour
{
    private Transform player;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player_Stat>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
