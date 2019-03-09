using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Canonball : MonoBehaviour
{
    private float speed = 15;
    private Vector3 direction;
    private float damage;

    public void InitCanonball(Vector3 p_dir, float p_damage)
    {
        direction = p_dir;
        Destroy(gameObject, 5f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += direction * speed * Time.deltaTime;
    }



}
