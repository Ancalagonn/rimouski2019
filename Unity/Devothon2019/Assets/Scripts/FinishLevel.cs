﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLevel : MonoBehaviour
{
    private Enemy_Stat[] enemy;

    private bool ending = false;

    // Start is called before the first frame update
    void Start()
    {
        enemy = FindObjectsOfType<Enemy_Stat>();
        SoundManager.Play("MusiqueBataille", Vector3.zero);
    }

    // Update is called once per frame
    void Update()
    {
        bool fini = true;

        foreach (Enemy_Stat stat in enemy)
        {
            if (stat != null) {
                fini = false;
                break;
            }
        }

        if (fini && !ending) {
            ending = true;
            Progression.CURRENT_LEVEL++;
            ManageScene.instance.LoadSceneBlack("Boutique_Scene");
        }
    }
}
