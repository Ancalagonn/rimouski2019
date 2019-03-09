using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ouragan : MonoBehaviour
{
    Vector2 Direction;
    Vector2 StartPos;
    float offsetY;
    float offsetX;
    bool DoOffsetX = true;

    // Start is called before the first frame update
    void Start()
    {
        
        StartPos = this.transform.position;
        offsetY = Random.Range(-10, 10);
        offsetX = Random.Range(-10, 10);
        Direction = new Vector2(Random.Range(-100, 100), Random.Range(-100, 100));
        Direction.Normalize();
        
        Quaternion temp = this.transform.rotation;
        temp.z = Random.Range(-90, 90);
        this.transform.rotation = temp;
        this.transform.position = new Vector2(transform.position.x + offsetX, transform.position.y);
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        Vector2 Pos = this.transform.position;
        if(DoOffsetX)
        {
            Pos.x += offsetX;
            DoOffsetX = false;
        }
        
        Pos.x += Direction.x * 0.1f;
        if(Direction.y >0)
        {
            Pos.y = Mathf.Pow((Pos.x - offsetX) * 0.1f, 2) + StartPos.y + offsetY;
        }
        else
        {
            Pos.y = -Mathf.Pow((Pos.x - offsetX) * 0.1f, 2) + StartPos.y + offsetY;
        }
        
        this.transform.position = Pos;

        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerInstance.playerStats.TakeDamage(5);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        PlayerInstance.playerStats.TakeDamage(5);
    }
}
