using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Static_Resources
{
    public static GameObject defaultCanonball;
    public static GameObject fireEffect;
    public static GameObject defaultCanon;
    public static GameObject tripleCanon;

    public static void LoadResources()
    {
        defaultCanonball = Resources.Load<GameObject>("Canonball");
        fireEffect = Resources.Load<GameObject>("FireEffect");
        defaultCanon = Resources.Load<GameObject>("DefaultCanon");
        tripleCanon = Resources.Load<GameObject>("TripleCanon");
    }
}
