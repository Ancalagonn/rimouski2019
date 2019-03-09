using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Attack : MonoBehaviour
{
    public KeyCode fireKey = KeyCode.Space;

    private string CollidingTag = "Enemy";

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(fireKey))
        {
            FireCanons();
        }

        CanonsCooldown();
    }

    /// <summary>
    /// Fire with all available canons
    /// </summary>
    void FireCanons()
    {
        foreach (Canon canon in PlayerInstance.playerStats.canons)
        {
            if (canon == null)
                continue;

            if (canon.canFire())
            {
                StartCoroutine(Fire(canon));

                float percentCooldown = (PlayerInstance.playerStats.shotCooldown.value / 100) * canon.baseCooldown;

                float modifier = percentCooldown * PlayerInstance.playerStats.shotCooldown.crewAssigned;

                canon.ResetCooldown(modifier);
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
        Debug.Log("Fired with canon : " + canon);

        if(canon.canonball == null)
        {
            canon.canonball = Static_Resources.defaultCanonball;
        }

        GameObject canonballObj;
        Canonball canonball;
        float lifetime;

        switch (canon.canonType)
        {
            default:
            case CanonType.Normal:
                canonballObj = Instantiate(canon.canonball, null);
                canonball = canonballObj.GetComponent<Canonball>();
                lifetime = Random.Range(0.75f, 1.10f);

                canonball.InitCanonball(canon.shootPoint.up, canon.GetDamage(), CollidingTag, lifetime);
                canonball.transform.position = canon.shootPoint.position;
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

                break;
            case CanonType.FlameThrower:
                break;
        }

        SoundManager.Play("FireCanon", canon.shootPoint.position);

        yield return null;
    }

    void CanonsCooldown()
    {
        foreach (Canon canon in PlayerInstance.playerStats.canons)
        {
            if (canon == null)
                continue;

            if (!canon.canFire())
                canon.currentCooldownTime -= Time.deltaTime;
        }
    }
}
