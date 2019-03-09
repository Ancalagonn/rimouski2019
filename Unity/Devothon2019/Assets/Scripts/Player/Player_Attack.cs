using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Attack : MonoBehaviour
{
    public KeyCode fireKey = KeyCode.Space;

    private void Awake()
    {
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
        yield return new WaitForSeconds(Random.Range(0.0f, 0.25f));
        Debug.Log("Fired with canon : " + canon);
        GameObject canonballObj = Instantiate(canon.canonball, null);
        Canonball canonball = canonballObj.GetComponent<Canonball>();
        canonball.SetDirection(canon.shootPoint.up);
        canonball.transform.position = canon.shootPoint.position;

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
