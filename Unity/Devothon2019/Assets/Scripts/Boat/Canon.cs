using UnityEngine;

[System.Serializable]
public class Canon
{
    public CanonType canonType;
    [HideInInspector]
    public float damage;
    public float baseCooldown;
    public float currentCooldownTime = 0;
    public GameObject canonball;
    public Transform shootPoint;
    public int level;

    private float canonballLifetime = 1f;

    public Canon(CanonType p_type, float p_damage, float p_baseCooldown, GameObject p_canonball, int p_level, float p_canonballLifetime)
    {
        canonType = p_type;
        damage = p_damage;
        baseCooldown = p_baseCooldown;
        canonball = p_canonball;
        level = p_level;
        canonballLifetime = p_canonballLifetime;
    }

    public Canon()
    {
        canonType = CanonType.Normal;
        damage = 10;
        baseCooldown = 2;
        level = 1;
        canonballLifetime = 1;
    }

    public bool canFire()
    {
        return currentCooldownTime <= 0;
    }

    public float GetDamage()
    {
        return damage * ((damage * 10 / 100) * ((level <= 0)? 1 : level));
    }

    public void ResetCooldown(float modifier = 0)
    {
        currentCooldownTime = baseCooldown - modifier;
    }

    public override string ToString()
    {
        return canonType.ToString() + " Lv : " + level + " Dmg : " + damage;
    }
}
