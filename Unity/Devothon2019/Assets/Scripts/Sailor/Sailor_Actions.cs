using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sailor_Actions : MonoBehaviour
{
    private GameObject particuleShoot;
    private Transform bulletInitPos;

    private int minLoot = 25;
    private int maxLoot = 40;

    private float cooldown = 0;
    private float lootRange = 7;

    public Sailor_Stats Stats { get; set; }
    private ExitPad exitPadScript;

    private void Awake() {
        this.Stats = new Sailor_Stats();
        this.bulletInitPos = this.transform.GetChild(0).GetComponent<Transform>();

        this.particuleShoot = Resources.Load<GameObject>("FireEffect");
        if (this.particuleShoot == null) {
            Debug.Log("Couldn't load 'FireEffect' game object from ressources");
        }

        this.exitPadScript = FindObjectOfType<ExitPad>();
        if (this.exitPadScript == null) {
            Debug.Log("No exit pad script found (sailor controller)");
        }
    }

    private void Update() {
        if (this.cooldown > 0) {
            this.cooldown -= Time.deltaTime;
        }
    }
    

    public void Shoot() {
        if (this.cooldown > 0) {
            return;
        }
        this.cooldown = this.Stats.weaponCooldown;
        SoundManager.Play("TirFusil", Vector3.zero);

        var anim = Instantiate(this.particuleShoot, this.bulletInitPos.position, this.bulletInitPos.rotation);
        Destroy(anim, 0.10f);

        var ray = Physics2D.Raycast(this.bulletInitPos.position, this.bulletInitPos.transform.up * 2);
        if (ray.collider) {
            if (ray.collider.tag == "Enemy" || ray.collider.tag == "Player") {
                var otherSailer = ray.transform.gameObject.GetComponent<Sailor_Actions>();
                if (otherSailer != null) {
                    SoundManager.Play("PerteMatelot");
                    otherSailer.takeDamage(this.Stats.weaponDamage);
                }
            }
        }
    }

    public void Loot(GameObject p_loot) {
        if (Vector2.Distance(this.transform.position, p_loot.transform.position) > this.lootRange) {
                return;
        }
        int lootAmount = Random.Range(this.minLoot, this.maxLoot);
        PlayerInstance.playerCash += lootAmount;

        Destroy(p_loot);
    }

    public void takeDamage(int p_damage) {
        this.Stats.HP -= p_damage;

        if (this.Stats.HP <= 0) {
            // If this is the player dying, we exit the scene
            if (this.tag == "Player") {
                PlayerInstance.playerStats.RemoveRandomMember();
                this.exitPadScript.ExitScene();
            }
            Destroy(this.gameObject);
        }
    }
}
