using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Static_Resources
{
    public static GameObject defaultCanonball;
    public static GameObject fireEffect;

    public static void LoadResources()
    {
        defaultCanonball = Resources.Load<GameObject>("Canonball");
        fireEffect = Resources.Load<GameObject>("FireEffect");
    }
}
