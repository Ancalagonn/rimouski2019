﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flames : MonoBehaviour
{
    private Vector3 direction;
    private float damage;

    private string collidingTag;

    private bool hasCollided = false;
    private Transform parent;

    private float currentLifetime = 0;

    AnimationCurve sizeCurve;

    Animator anim;

    public void InitFlame(Transform p_parent, Vector3 p_dir, float p_damage, string p_collidingTag, float p_lifeTime)
    {
        direction = p_dir;
        damage = p_damage;
        collidingTag = p_collidingTag;
        parent = p_parent;

        float initialSize = transform.localScale.x;

        Keyframe[] keys = new Keyframe[] {
            new Keyframe(0, initialSize * 0.5f),
            new Keyframe(p_lifeTime * 0.15f, initialSize),
            new Keyframe(p_lifeTime * 0.50f, initialSize * 1.5f),
            new Keyframe(p_lifeTime * 0.75f, initialSize * 1.5f),
            new Keyframe(p_lifeTime * 0.90f, initialSize * 0.80f),
            new Keyframe(p_lifeTime * 0.90f, initialSize * 0.5f)};

        sizeCurve = new AnimationCurve(keys);

        Destroy(gameObject, p_lifeTime);

    }

    // Update is called once per frame
    void Update()
    {
        if(parent == null)
        {
            Destroy(gameObject);
            return;
        }
        transform.GetChild(0).gameObject.SetActive(true);

        transform.localPosition = parent.position;
        transform.rotation = parent.rotation;

        currentLifetime += Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.CompareTag(collidingTag))
        {
            col.SendMessage("TakeDamage", damage / 2f);
        }
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.CompareTag(collidingTag))
        {
            col.SendMessage("TakeDamage", damage * Time.deltaTime);
        }
    }
}
