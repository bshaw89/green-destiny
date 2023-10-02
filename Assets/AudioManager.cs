using UnityEngine.Audio;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    // To Play sounds: FindObjectOfType<AudioManager>().Play("filename");

    public Sound[] sounds;
    void Awake()
    {
        foreach (Sound s in sounds)
        {
           s.source = gameObject.AddComponent<AudioSource>();
           s.source.clip = s.clip;

           s.source.volume = s.volume;
           s.source.pitch = s.pitch;
           s.source.loop = s.loop;
        }
    }

    void Start()
    {
        // if (SceneManager.GetActiveScene().buildIndex == 0 )
        // {
            Play("Miasma");
        // }
    }

   
   public void Play (string name)
   {
      Sound s = Array.Find(sounds, sound => sound.name == name);

        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found.");
            return;
        }

        // if (s.source.isPlaying);
        // {
        //     // Debug.LogWarning("Sound: " + name + " not found.");
        //     return;
            s.source.Play();
        // }
   }

   public void Stop (string name)
   {
      Sound s = Array.Find(sounds, sound => sound.name == name);

        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found.");
            return;
        }
      s.source.Stop();
   }
}
