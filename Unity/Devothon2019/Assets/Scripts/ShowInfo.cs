using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowInfo : MonoBehaviour
{
    public Text cashText;
    public Transform hpImageParent;

    private List<Image> hpDisplay = new List<Image>();

    private void Start()
    {
        foreach (Transform child in hpImageParent)
        {
            hpDisplay.Add(child.GetComponent<Image>());
        }
    }

    // Update is called once per frame
    void Update()
    {
        cashText.text = PlayerInstance.playerCash + "$";
        //hpText.text = PlayerInstance.playerStats.PercentHpLeft() + "%";
        CalculateHp();

    }

    void CalculateHp()
    {
        int percent = (int)(PlayerInstance.playerStats.PercentHpLeft() / 10) - 1;

        if(percent <= 0 && !PlayerInstance.playerStats.isDead())
        {
            percent = 1;
        }

        for (int i = 0; i < hpDisplay.Count; i++)
        {
            if(i <= percent)
            {
                hpDisplay[i].color = Color.blue;
            }
            else
            {
                hpDisplay[i].color = Color.red;
            }
        }

    }
}
