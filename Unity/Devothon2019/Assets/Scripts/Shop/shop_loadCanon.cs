using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class shop_loadCanon : MonoBehaviour
{
    // Start is called before the first frame update

    public Dropdown m_ddlChoix;
    void Start()
    {
        List<Dropdown.OptionData> lst_options = new List<Dropdown.OptionData>();

        for(int i = 0; i < System.Enum.GetNames(typeof(CanonType)).Length; i++)
        {
            Dropdown.OptionData newOption = new Dropdown.OptionData();
            newOption.text = System.Enum.GetNames(typeof(CanonType))[i].ToString();
            lst_options.Add(newOption);
        }
        if(m_ddlChoix != null)
        {
            m_ddlChoix.AddOptions(lst_options);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
