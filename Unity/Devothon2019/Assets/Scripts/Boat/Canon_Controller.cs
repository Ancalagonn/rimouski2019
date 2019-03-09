using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Canon_Controller : MonoBehaviour
{
    public Canon canonInfo;
    private Animator anim;

    private void Start()
    {
        anim = GetComponentInChildren<Animator>();
        anim.Play("DefaultCanon-Shoot");
    }

}
