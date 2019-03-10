using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TargetPoint
{
    public Transform targetPoint;
    public Transform enemy;
    public Quaternion rotation;

    public TargetPoint(Transform p_target, Quaternion p_rotation)
    {
        targetPoint = p_target;
        rotation = p_rotation;
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
