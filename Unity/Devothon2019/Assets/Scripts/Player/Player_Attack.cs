using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Attack : MonoBehaviour
{
    public KeyCode fireKey = KeyCode.Space;

    private GameObject defaultCanonball;

    private void Awake()
    {
        defaultCanonball = Resources.Load<GameObject>("Canonball");
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

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
        Debug.Log("Fired with canon : " + canon);

        if(canon.canonball == null)
        {
            canon.canonball = defaultCanonball;
        }

        GameObject canonballObj;
        Canonball canonball;

        switch (canon.canonType)
        {
            default:
            case CanonType.Normal:
                canonballObj = Instantiate(canon.canonball, null);
                canonball = canonballObj.GetComponent<Canonball>();
                canonball.InitCanonball(canon.shootPoint.up, canon.GetDamage());
                canonball.transform.position = canon.shootPoint.position;
                SoundManager.Play("FireCanon", canon.shootPoint.position);
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
                   
                    canonball.InitCanonball(temp.transform.up, canon.GetDamage());
                    canonball.transform.position = canon.shootPoint.position;
                    SoundManager.Play("FireCanon", canon.shootPoint.position);
                    //yield return new WaitForSeconds(0.1f);
                }
                break;
            case CanonType.FlameThrower:
                break;
        }


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
