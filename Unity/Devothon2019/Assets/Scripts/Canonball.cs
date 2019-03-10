using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Canonball : MonoBehaviour
{
    private float speed = 15;
    private Vector3 direction;
    private float damage;

    private string collidingTag = "";

    private bool hasCollided = false;

    private float maxLifetime = 0;
    private float currentLifetime = 0;

    AnimationCurve sizeCurve;

    bool hasDropBelowWater = false;
    Collider2D collider;

    private void Awake()
    {
        collider = GetComponent<Collider2D>();
        collider.enabled = false;
    }

    public void InitCanonball(Vector3 p_dir, float p_damage, string p_collidingTag, float p_lifeTime)
    {
        direction = p_dir;
        damage = p_damage;
        collidingTag = p_collidingTag;
        
        maxLifetime = p_lifeTime;

        transform.position = direction * 0.5f;

        float initialSize = transform.localScale.x;

        Keyframe[] keys = new Keyframe[] {
            new Keyframe(0, initialSize),
            new Keyframe(p_lifeTime * 0.25f, initialSize * 1.35f),
            new Keyframe(p_lifeTime * 0.50f, initialSize * 1.5f),
            new Keyframe(p_lifeTime * 0.75f, initialSize * 1.35f),
            new Keyframe(p_lifeTime * 0.90f, initialSize * 0.95f),
            new Keyframe(p_lifeTime * 0.95f, initialSize * 0.25f) };

        sizeCurve = new AnimationCurve(keys);
        Destroy(gameObject, p_lifeTime);
    }

    // Update is called once per frame
    void Update() 
    {
        if (sizeCurve == null)
            return;

        collider.enabled = true;

        transform.position += direction * speed * Time.deltaTime;
        currentLifetime += Time.deltaTime;

        if(currentLifetime >= maxLifetime * 0.95f && !hasDropBelowWater && !hasCollided)
        {
            hasDropBelowWater = true;
            SoundManager.Play("Sploush", transform.position);
            
            var waterSpashParticules = Instantiate(Static_Resources.WaterSplashParticule, this.transform.position, Quaternion.identity);
            Destroy(waterSpashParticules, 1);
        }

        transform.localScale = Vector3.one * sizeCurve.Evaluate(currentLifetime);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (hasCollided || collidingTag.Length == 0)
            return;

        if(col.CompareTag(collidingTag))
        {
            hasCollided = true;
            col.SendMessage("TakeDamage", this.damage);
            SoundManager.Play("ShipHit", transform.position);
            Destroy(gameObject);
        }
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if (hasCollided || collidingTag.Length == 0)
            return;

        if (col.CompareTag(collidingTag))
        {
            hasCollided = true;
            col.SendMessage("TakeDamage", this.damage);
            SoundManager.Play("ShipHit", transform.position);
            Destroy(gameObject);
        }
    }
}
