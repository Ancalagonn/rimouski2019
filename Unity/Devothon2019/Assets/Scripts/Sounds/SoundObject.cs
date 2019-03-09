using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundObject : MonoBehaviour
{
    public void Init(float time)
    {
        Destroy(gameObject, time);
    }
}
