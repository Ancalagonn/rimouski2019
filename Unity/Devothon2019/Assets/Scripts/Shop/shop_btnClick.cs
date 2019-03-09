using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class shop_btnClick : MonoBehaviour
{
    // Start is called before the first frame update
    public int id = 0;

    //Sélectionne un bouton
    public void btn_CanonClick()
    {
        ResetColors();
        gameObject.GetComponent<Image>().color = Color.red;

        shop_loadShop.btn_select = id;
        shop_loadShop.LoadPanel(this.gameObject);
    }

    public void ResetColors()
    {
        GameObject[] lst_btn = GameObject.FindGameObjectsWithTag("shop_btnCanon");
        foreach(GameObject btn in lst_btn)
        {
            btn.GetComponent<Image>().color = Color.white;
        }
    }
}
