using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public Sound[] _sounds;

    // Start is called before the first frame update
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
        foreach(Sound s in _sounds)
        {
           s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s._clip;

            s.source.volume = s._volume;
            s.source.pitch = s._pitch;
            s.source.loop = s.loop;
        }
    }
    private void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public void Play (string name)
    {

        Sound s = Array.Find(_sounds, sound => sound._name == name);
        if (s == null)
        {
            return;
        }
        s.source.Play();
    }
}
