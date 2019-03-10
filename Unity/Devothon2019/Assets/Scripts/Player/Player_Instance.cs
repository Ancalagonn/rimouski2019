using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerInstance
{
    public static Boat_Stats playerStats = new Boat_Stats(500, new Stats(10, 1), new Stats(50, 1), new Stats(5, 1), new Stats(2, 1));

    public static int playerCash = 100;
}
