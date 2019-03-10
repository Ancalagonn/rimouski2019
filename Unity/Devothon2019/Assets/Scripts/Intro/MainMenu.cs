using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public void StartGame() {

        Progression.CURRENT_LEVEL=-1;
        PlayerInstance.playerCash = 100;
        PlayerInstance.playerStats = new Boat_Stats(500, new Stats(10, 1), new Stats(50, 1), new Stats(5, 1), new Stats(2, 1));

        ManageScene.instance.LoadSceneBlack("Introduction_Scene");
    }
}
