using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TargetPoint
{
    public Transform targetPoint;
    public Transform enemy;

    public TargetPoint(Transform p_target, Transform p_enemy)
    {
        targetPoint = p_target;
        enemy = p_enemy;
    }

    public void AssignEnemy(Transform p_enemy)
    {
        enemy = p_enemy;
    }

    public bool isAssigned()
    {
        return enemy == null;
    }
}
