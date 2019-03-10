using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class Static_Resources
{
    public static GameObject defaultCanonball;
    public static GameObject fireEffect;
    public static GameObject defaultCanon;
    public static GameObject tripleCanon;
    public static GameObject flameThrower;
    public static GameObject flameEffect;
    public static GameObject whiteflag;

    public static Sprite plank;
    public static Sprite plankCracked;

    public static int SmallBoatValue = 50;
    public static int BigBoatValue = 100;

    public static GameObject WaterSplashParticule;

    /*private static Boat_Stats smallEnemyStats;
    private static Boat_Stats bigEnemyStats;*/

    public static void LoadResources()
    {
        defaultCanonball = Resources.Load<GameObject>("Canonball");
        flameEffect = Resources.Load<GameObject>("Flame");
        fireEffect = Resources.Load<GameObject>("FireEffect");
        defaultCanon = Resources.Load<GameObject>("DefaultCanon");
        tripleCanon = Resources.Load<GameObject>("TripleCanon");
        flameThrower = Resources.Load<GameObject>("Flamethrower");
        WaterSplashParticule = Resources.Load<GameObject>("WaterSplash");
        whiteflag = Resources.Load<GameObject>("Dapreau");

        plank = Resources.Load<Sprite>("Plank");
        plankCracked = Resources.Load<Sprite>("PlankCracked");
    }

    public static Boat_Stats GenerateBoatStats(EnemySize size, EnemyType type)
    {
        Boat_Stats stats = null;

        switch (size)
        {
            case EnemySize.Small:
                stats = new Boat_Stats(45, new Stats(5, 1), new Stats(50, 1), new Stats(5, 1), new Stats(0,0));
                break;
            case EnemySize.Big:
                stats = new Boat_Stats(160, new Stats(3, 1), new Stats(50, 1), new Stats(5, 1), new Stats(0, 0));
                break;
        }

        stats.canons = GenerateCanons(size, type);
        return stats;
    }

    private static List<Canon> GenerateCanons(EnemySize size, EnemyType type)
    {
        if (defaultCanonball == null)
            LoadResources();

        List<Canon> canons = new List<Canon>();

        CanonType canonType = CanonType.Normal;

        int maxCanon = (size == EnemySize.Small) ? 2 : 6;

        for (int i = 0; i < maxCanon; i++)
        {

            float damage = 25;
            float baseCooldown = 3;
            GameObject canonball = defaultCanonball;

            switch (type)
            {
                case EnemyType.Normal:
                    canonType = CanonType.Normal;
                    damage = 15;
                    baseCooldown = 3;
                    canonball = defaultCanonball;
                    break;
                case EnemyType.Fire:
                    canonType = CanonType.LanceFlammes;
                    damage = 10;
                    baseCooldown = 5;
                    canonball = flameEffect;
                    break;
                case EnemyType.Triple:
                    canonType = CanonType.TirTriple;
                    damage = 7;
                    baseCooldown = 3.5f;
                    canonball = defaultCanonball;

                    //Reduce power
                    switch (size)
                    {
                        case EnemySize.Big:
                            if (i == 1 || i == 4)
                            {
                                canons.Add(null);
                                continue;
                            }
                            break;
                    }

                    break;
            }

            canons.Add(new Canon(canonType, damage, baseCooldown, canonball, 1, 1.15f));
        }

        return canons;
    }
}
