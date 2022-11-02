using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]
public class Sound 
{
    public string _name;

    public AudioClip _clip;

    [Range(0f,1f)]
    public float _volume;

    [Range(.1f,3f)]
    public float _pitch;

    public bool loop;

    [HideInInspector]
    public AudioSource source;
}
