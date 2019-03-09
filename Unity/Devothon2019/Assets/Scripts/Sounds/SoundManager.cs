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
        if (Sounds.Count != 0)
            return;

        Sound_SO[] sds = Resources.LoadAll<Sound_SO>("Sounds");
        soundObjPrefabs = Resources.Load("SoundObject") as GameObject;

        GameObject SoundObject = new GameObject();
        SoundObject.name = "SoundObject";
        SoundObject.transform.SetParent(null);

        foreach (var s in sds)
        {
            AudioSource src = GameObject.Find("SoundObject").AddComponent<AudioSource>();

            src.clip = s.clip;
            src.volume = s.volume;
            src.loop = s.loop;

            s.source = src;

            Sounds.Add(s);
        }
    }

    public static void Play(string name, Vector3 SoundPos = new Vector3())
    {
        Sound_SO s = Array.Find(Sounds.ToArray(), sound => sound.name == name);

        if(s == null)
        {
            Debug.Log("Sound with the name " + name + " not found");
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
            Debug.Log("Sound with the name " + name + " not found");
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
