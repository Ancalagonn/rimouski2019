using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public static class SoundManager {

    public static List<Sound_SO> Sounds = new List<Sound_SO>();
    public static GameObject soundObjPrefabs;

    

    public static void LoadSound()
    {
        GameObject SoundObject = GameObject.Find("SoundObject");

        if (SoundObject == null)
        {
            SoundObject = new GameObject();
            SoundObject.name = "SoundObject";
            SoundObject.transform.SetParent(null);
        }

        if (Sounds.Count == 0)
        {
            var sds = Resources.LoadAll<Sound_SO>("Sounds");
            soundObjPrefabs = Resources.Load("SoundObject") as GameObject;

            LoadAudioSource(SoundObject, sds);
        }
        else
        {
            LoadAudioSource(SoundObject, Sounds.ToArray());
        }
    }

    private static void LoadAudioSource(GameObject parent, Sound_SO[] sounds)
    {


        foreach (var s in sounds)
        {
            AudioSource src = parent.AddComponent<AudioSource>();

            src.clip = s.clip;
            src.volume = s.volume;
            src.loop = s.loop;

            s.source = src;

            Sounds.Add(s);
        }
    }

    public static void Play(string name, Vector3 SoundPos = new Vector3())
    {
        GameObject SoundObject = GameObject.Find("SoundObject");
        if(SoundObject == null)
        {
            LoadSound();
        }

        Sound_SO s = Array.Find(Sounds.ToArray(), sound => sound.name == name);

        if(s == null)
        {
            return;
        }

        if(s.isMusic)
        {
            if(s.source != null)
                s.source.Play();
            else
            {
                AudioSource src = new GameObject().AddComponent<AudioSource>();

                src.clip = s.clip;
                src.volume = s.volume;
                src.loop = s.loop;

                s.source = src;

                //Sounds.Add(s);

                s.source.Play();
            }
        }
        else
        {
            GameObject soundObj = GameObject.Instantiate(soundObjPrefabs);
            soundObj.name = name + "_Sound";

            soundObj.transform.position = SoundPos;

            AudioSource src = soundObj.AddComponent<AudioSource>();
            SoundObject obj = soundObj.GetComponent<SoundObject>();

            obj.Init(s.clip.length + 0.3f);

            src.clip = s.clip;
            src.volume = s.volume;
            src.loop = s.loop;
            
            src.Play();
        }
    }

    public static void Stop(string name)
    {
        Sound_SO s = Array.Find(Sounds.ToArray(), sound => sound.name == name);

        if (s == null)
        {
            return;
        }

        if(s.source != null)
            s.source.Stop();
    }

    public static void StopAll()
    {
        foreach (var s in Sounds)
        {
            s.source.Stop();
        }
    }

}
