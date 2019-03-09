using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shop_btnClick : MonoBehaviour
{
    // Start is called before the first frame update
    public int id = 0;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Sélectionne un bouton
    public void btn_CanonClick()
    {
        shop_loadShop.btn_select = id;
        shop_loadShop.LoadPanel();
    }
}
