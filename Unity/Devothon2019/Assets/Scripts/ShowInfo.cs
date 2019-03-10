using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowInfo : MonoBehaviour
{
    public Text cashText;
    public Transform hpImageParent;
    public Text matelotText;

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
        matelotText.text = PlayerInstance.playerStats.crewMembers + " matelots";
        //hpText.text = PlayerInstance.playerStats.PercentHpLeft() + "%";
        CalculateHp();

    }

    void CalculateHp()
    {
        int percent = (int)(PlayerInstance.playerStats.PercentHpLeft() / 10) - 1;

        if(percent < 0 && !PlayerInstance.playerStats.isDead())
        {
            percent = 0;
        }

        for (int i = 0; i < hpDisplay.Count; i++)
        {
            //Show good plank
            if(i <= percent)
            {
                hpDisplay[i].sprite = Static_Resources.plank;
            }
            else //Show bad plank
            {
                hpDisplay[i].sprite = Static_Resources.plankCracked;
            }
        }

    }
}
