using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class price : MonoBehaviour
{
    public price instance;

    public const int CANON_NORMAL = 75;
    public const int CANON_TRIPLE = 120;
    public const int CANON_FLAME = 150;

    public const int UPGRADE_CANON = 100;
    public const int COUT_CANON_BASE = 75;

    public const int UPGRADE_COQUE = 100;
    public const int REPAIR_COQUE = 100;
    public const int ADD_CREW = 100;

    public void Awake()
    {
        instance = this;
    }
}
