using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public Sound[] sounds;

    public static AudioManager instance;



     void Awake()
    {

        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        foreach(Sound s in sounds)
        {


            s.source=gameObject.AddComponent<AudioSource>();
          
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;



        }        


    }

     void Start()
    {
        PlaySound("theme");
    }

    public void PlaySound(string soundToPlay)
    {

        
        Sound s = Array.Find(sounds, sound => sound.name == soundToPlay);

        if (s == null)
        {
            Debug.Log("Sound "+ soundToPlay + " not found");
            return;
        }
        Debug.Log("Sound Playing");
        s.source.Play();
        
        
        
    }




}