using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[CreateAssetMenu(fileName = "Sound", menuName = "New Sound")]
public class Sound_SO : ScriptableObject
{

    public new string name;
    public AudioClip clip;
    [Range(0, 1)]
    public float volume;
    public bool isMusic;
    public bool loop;

    public AudioSource source;
}
