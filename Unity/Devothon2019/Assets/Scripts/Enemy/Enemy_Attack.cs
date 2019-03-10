using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Attack : MonoBehaviour
{
    private string CollidingTag = "Player";
    private Enemy_Stat enemyStat;
    private Enemy_Movement enemyMovement;
    private Transform target;

    private void Awake()
    {
        enemyStat = GetComponent<Enemy_Stat>();
        enemyMovement = GetComponent<Enemy_Movement>();
        target = FindObjectOfType<Player_Stat>().transform;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CanonsCooldown();

        if(Vector2.Distance(target.position, transform.position) < 10f)
            FireCanons();
    }

    void FireCanons()
    {
        foreach (Canon canon in enemyStat.enemyStats.canons)
        {
            if (canon == null || canon.shootPoint == null)
                continue;

            if (canon.canFire())
            {
                Debug.Log(canon);
                StartCoroutine(Fire(canon));
                canon.ResetCooldown(-Random.Range(0.15f, 0.35f));
            }
        }
    }

    /// <summary>
    /// fire the param canon
    /// </summary>
    /// <param name="canon"></param>
    /// <returns></returns>
    IEnumerator Fire(Canon canon)
    {
        yield return new WaitForSeconds(Random.Range(0.0f, 0.5f));

        if (canon.canonball == null)
        {
            canon.canonball = Static_Resources.defaultCanonball;
        }

        GameObject canonballObj;
        Canonball canonball;
        float lifetime;

        string soundName = "FireCanon";

        switch (canon.canonType)
        {
            default:
            case CanonType.Normal:
                canonballObj = Instantiate(canon.canonball, null);
                canonball = canonballObj.GetComponent<Canonball>();
                lifetime = Random.Range(0.75f, 1.10f);
                    
                canonball.InitCanonball(canon.shootPoint.up, canon.GetDamage(), CollidingTag, lifetime);
                canonball.transform.position = canon.shootPoint.position;
                soundName = "FireCanon";
                break;

            case CanonType.TripleShot:

                for (int i = 0; i < 3; i++)
                {
                    canonballObj = Instantiate(canon.canonball, null);
                    canonball = canonballObj.GetComponent<Canonball>();

                    GameObject temp = new GameObject();
                    temp.transform.SetParent(canon.shootPoint);
                    temp.transform.rotation = canon.shootPoint.rotation;

                    if (i == 0)
                    {
                        temp.transform.Rotate(temp.transform.forward, -12);
                    }
                    else if (i == 2)
                    {
                        temp.transform.Rotate(temp.transform.forward, 12);
                    }

                    lifetime = Random.Range(0.75f, 1.10f);

                    canonball.InitCanonball(temp.transform.up, canon.GetDamage(), CollidingTag, lifetime);
                    canonball.transform.position = canon.shootPoint.position;

                    Destroy(temp);
                }

                soundName = "FireCanon";
                break;

            case CanonType.FlameThrower:
                canonballObj = Instantiate(canon.canonball, null);
                Flames flames = canonballObj.GetComponent<Flames>();
                lifetime = canon.baseCooldown * 0.75f;

                GameObject t = new GameObject();
                t.transform.SetParent(canon.shootPoint);
                t.transform.position = canon.shootPoint.position + canon.shootPoint.up;
                t.transform.rotation = canon.shootPoint.rotation;

                flames.InitFlame(t.transform, t.transform.up, canon.GetDamage(), CollidingTag, lifetime);

                Destroy(t, lifetime);


                soundName = "FlameThrower";
                break;
        }

        SoundManager.Play(soundName, canon.shootPoint.position);

        yield return null;
    }

    void CanonsCooldown()
    {
        foreach (Canon canon in enemyStat.enemyStats.canons)
        {
            if (canon == null)
                continue;

            if (!canon.canFire())
                canon.currentCooldownTime -= Time.deltaTime;
        }
    }
}
