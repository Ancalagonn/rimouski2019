using UnityEngine;

[System.Serializable]
public class Canon
{
    public CanonType canonType;
    public float damage;
    public float baseCooldown;
    public GameObject canonball;
    public Transform shootPoint;

    public Transform canonPosition;
}
