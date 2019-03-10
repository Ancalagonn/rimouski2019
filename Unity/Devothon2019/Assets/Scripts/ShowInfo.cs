using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowInfo : MonoBehaviour
{
    public Text cashText;
    public Text hpText;

    // Update is called once per frame
    void Update()
    {
        cashText.text = PlayerInstance.playerCash + "$";
        hpText.text = PlayerInstance.playerStats.PercentHpLeft() + "%";
    }
}
