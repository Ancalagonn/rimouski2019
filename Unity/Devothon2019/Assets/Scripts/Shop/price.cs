using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class price : MonoBehaviour
{
    public price instance;

    public static int Canon_Normal = 40;
    public static int Canon_Triple = 40;
    public static int Canon_Flame = 40;

    public static int UpgradeCanon = 40;

    public static int UpgradeCoque = 100;
    public static int RepairCoque = 100;
    public static int AddCrew = 100;

    public void Awake()
    {
        instance = this;
    }
}
