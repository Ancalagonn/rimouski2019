using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ouragan : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        Vector2 Pos = this.transform.position;

        Pos.x += Mathf.Pow(10, 2);
        Pos.y += 10;
    }
}
