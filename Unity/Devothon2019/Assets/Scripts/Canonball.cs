using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Canonball : MonoBehaviour
{
    private float speed = 15;
    private Vector3 direction;
    private float damage;

    private string collidingTag;

    private bool hasCollided = false;

    public void InitCanonball(Vector3 p_dir, float p_damage, string p_collidingTag)
    {
        direction = p_dir;
        damage = p_damage;
        collidingTag = p_collidingTag;
        Destroy(gameObject, 5f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += direction * speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (hasCollided)
            return;

        if(col.CompareTag(collidingTag))
        {
            hasCollided = true;
            col.SendMessage("TakeDamage", this.damage);
            Destroy(gameObject);
        }
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if (hasCollided)
            return;

        if (col.CompareTag(collidingTag))
        {
            hasCollided = true;
            col.SendMessage("TakeDamage", this.damage);
            Destroy(gameObject);
        }
    }



}
