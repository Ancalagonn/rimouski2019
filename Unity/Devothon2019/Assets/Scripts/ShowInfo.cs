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
        int percent = (int)((PlayerInstance.playerStats.PercentHpLeft()) / 5) - 2;

        if(percent < 0 && !PlayerInstance.playerStats.isDead())
        {
            percent = 0;
        }

        bool disableOther = false;

        for (int i = 0; i < hpDisplay.Count; i++)
        {
            int index = (int)Mathf.Floor(percent / 2);         

            if(disableOther)
            {
                hpDisplay[i].enabled = false;
                continue;
            }

            //Show good plank
            if(i <= index)
            {
                hpDisplay[i].sprite = Static_Resources.plank;
                hpDisplay[i].enabled = true;
            }
            else //Show bad plank
            {
                if (percent % 2 == 0)
                {
                    hpDisplay[i].enabled = false;
                    disableOther = true;
                }
                else
                {
                    hpDisplay[i].sprite = Static_Resources.plankCracked;
                    hpDisplay[i].enabled = true;
                    disableOther = true;
                }
            }

        }

    }
}
