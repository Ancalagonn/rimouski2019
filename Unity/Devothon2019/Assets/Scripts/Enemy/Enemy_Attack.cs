using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Attack : MonoBehaviour
{
    private string CollidingTag = "Player";
    private Enemy_Stat enemyStat;

    private void Awake()
    {
        enemyStat = GetComponent<Enemy_Stat>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CanonsCooldown();
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
                canon.ResetCooldown();
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
        //Debug.Log("Fired with canon : " + canon);

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
                lifetime = canon.baseCooldown;

                flames.InitFlame(canon.shootPoint,canon.shootPoint.up, canon.GetDamage(), CollidingTag, lifetime);
                flames.transform.position = canon.shootPoint.position + (canon.shootPoint.up * 5);

                soundName = "FireCanon";
                break;
        }

        SoundManager.Play("FireCanon", canon.shootPoint.position);

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
