using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using System;

public class Audio : MonoBehaviour
{

    public Sound[] sounds;

    private void Awake()
    {
       foreach (Sound sound in sounds)
        {
            sound.source = gameObject.AddComponent<AudioSource>();
            sound.source.clip = sound.clip;

            sound.source.volume = sound.volume;
            sound.source.pitch = sound.pitch;
            sound.source.loop = sound.loop;
        }
    }
    private void Start()
    {  
        // Main Menu theme
        if (SceneManager.GetActiveScene().name == "MainMenu")
            Play("Behold a Square House");

        // Main game theme
        if(SceneManager.GetActiveScene().name == "Main")
            Play("Marketplace");

        // End credit theme
        if (SceneManager.GetActiveScene().name == "EndCredits")
            Play("Ethereal Sunday");

    }

    public void Play (string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if(s == null)
        {
            return;
        }
        s.source.Play();
    }
}
