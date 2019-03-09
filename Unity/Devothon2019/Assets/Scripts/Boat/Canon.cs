using UnityEngine;

[System.Serializable]
public class Canon
{
    public CanonType canonType;
    public float damage;
    public float baseCooldown;
    public float currentCooldownTime = 0;
    public GameObject canonball;
    public Transform shootPoint;
    public int level;

    public Transform canonPosition;

    public bool canFire()
    {
        return currentCooldownTime <= 0;
    }

    public void ResetCooldown()
    {
        currentCooldownTime = baseCooldown;
    }

    public override string ToString()
    {
        return canonType.ToString() + " Lv : " + level + " Dmg : " + damage;
    }
}
