using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Static_Resources
{
    public static GameObject defaultCanonball;
    public static GameObject fireEffect;
    public static GameObject defaultCanon;
    public static GameObject tripleCanon;

    public static int SmallBoatValue = 100;
    public static int BigBoatValue = 200;

    /*private static Boat_Stats smallEnemyStats;
    private static Boat_Stats bigEnemyStats;*/

    public static void LoadResources()
    {
        defaultCanonball = Resources.Load<GameObject>("Canonball");
        fireEffect = Resources.Load<GameObject>("FireEffect");
        defaultCanon = Resources.Load<GameObject>("DefaultCanon");
        tripleCanon = Resources.Load<GameObject>("TripleCanon");
    }

    public static Boat_Stats GenerateBoatStats(EnemySize size, EnemyType type)
    {
        Boat_Stats stats = null;
        switch (size)
        {
            case EnemySize.Small:
                stats = new Boat_Stats(100, new Stats(8, 1), new Stats(50, 1), new Stats(5, 1), new Stats(0,0));
                break;
            case EnemySize.Big:
                stats = new Boat_Stats(200, new Stats(4, 1), new Stats(50, 1), new Stats(5, 1), new Stats(0, 0));
                break;
        }

        return null;
    }
}
