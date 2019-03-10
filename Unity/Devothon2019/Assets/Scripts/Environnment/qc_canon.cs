using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class qc_canon : MonoBehaviour
{
    public Canon CanonInfos;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        CanonInfos.currentCooldownTime -= Time.deltaTime;
        if(CanonInfos.canFire())
        {
            Debug.DrawRay(CanonInfos.shootPoint.position, CanonInfos.shootPoint.up * 50);

            RaycastHit2D hit = Physics2D.Raycast(transform.GetChild(0).GetChild(0).position, transform.GetChild(0).GetChild(0).up, 2000, 1 << LayerMask.NameToLayer("Ennemy"));
            if (hit.collider != null)
            {
                Debug.Log("shoot");
                GameObject canonball = Instantiate(Static_Resources.defaultCanonball, CanonInfos.shootPoint.position, CanonInfos.shootPoint.rotation);

                Canonball c = canonball.GetComponent<Canonball>();
                c.InitCanonball(CanonInfos.shootPoint.up, 20, "Enemy", 2);
                CanonInfos.ResetCooldown();
                c.transform.position = CanonInfos.shootPoint.position; 
            }
        }
    }
}
