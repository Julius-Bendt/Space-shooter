using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour {


    public GameObject PlaySound(AudioClip clip)
    {
        return PlaySound(clip, false, 1, true);
    }

    public GameObject PlaySound(AudioClip clip, bool loop = false,float volume = 1)
    {
        GameObject g = PlaySound(clip, loop,volume,true);


        return g;
    }

    public GameObject PlaySound(AudioClip clip, bool loop = false,float volume = 1,bool _2d = true)
    {
        GameObject g = new GameObject("Audio source");

        AudioSource s = g.AddComponent<AudioSource>();

        volume = Mathf.Clamp(volume, 0, 1);

        s.loop = loop;
        s.clip = clip;
        s.volume = volume;
        s.spatialBlend = (_2d) ? 0 : 1;

        if (!loop)
        {
            Destroy(g, clip.length);
        }


        s.Play();

        return g;
    }
}
