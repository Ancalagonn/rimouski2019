using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class qc_canon : MonoBehaviour
{
    public Canon CanonInfos;
    public float timer;
    public const float TEMPS_AVANT_SWITCH = 30;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        CanonInfos.currentCooldownTime -= Time.deltaTime;
        if(CanonInfos.canFire())
        {
            if(timer >= TEMPS_AVANT_SWITCH)
            {
                RaycastHit2D hit = Physics2D.Raycast(transform.GetChild(0).GetChild(0).position, transform.GetChild(0).GetChild(0).up, 2000, 1 << LayerMask.NameToLayer("Ennemy"));
                if (hit.collider != null)
                {
                    GameObject canonball = Instantiate(Static_Resources.defaultCanonball, CanonInfos.shootPoint.position, CanonInfos.shootPoint.rotation);

                    Canonball c = canonball.GetComponent<Canonball>();
                    c.InitCanonball(CanonInfos.shootPoint.up, 20, "Enemy", 2);
                    CanonInfos.ResetCooldown();
                    c.transform.position = CanonInfos.shootPoint.position;
                }
            }
            else
            {
                RaycastHit2D hit = Physics2D.Raycast(transform.GetChild(0).GetChild(0).position, transform.GetChild(0).GetChild(0).up, 2000, 1 << LayerMask.NameToLayer("Player"));
                if (hit.collider != null)
                {
                    GameObject canonball = Instantiate(Static_Resources.defaultCanonball, CanonInfos.shootPoint.position, CanonInfos.shootPoint.rotation);

                    Canonball c = canonball.GetComponent<Canonball>();
                    c.InitCanonball(CanonInfos.shootPoint.up, 20, "Player", 2);
                    CanonInfos.ResetCooldown();
                    c.transform.position = CanonInfos.shootPoint.position;
                }
            }
            
        }
    }
}
