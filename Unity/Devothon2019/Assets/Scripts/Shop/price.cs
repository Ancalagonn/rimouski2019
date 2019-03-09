using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class price : MonoBehaviour
{
    public price instance;

    public const int Canon_Normal = 40;
    public const int Canon_Triple = 40;
    public const int Canon_Flame = 40;

    public const int UpgradeCanon = 40;
    public const int CoutCanonBase = 30;

    public const int UpgradeCoque = 100;
    public const int RepairCoque = 100;
    public const int AddCrew = 100;

    public void Awake()
    {
        instance = this;
    }
}
