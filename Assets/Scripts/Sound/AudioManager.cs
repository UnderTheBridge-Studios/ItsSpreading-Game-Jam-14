using UnityEngine.Audio;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;





public class AudioManager : MonoBehaviour
{



    public static AudioManager instance;

    public AudioSource themeSource;
    public Theme[] themes;


    public AudioSource SFXSource;
    public SFXs[] Sounds;


    private Theme m_currentTheme = null;

    


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


    }

    void Start()
    {


    }


    //THEME FUNCTIONS
    public void PlayTheme(string name,bool fade)
    {

        Theme t = Array.Find(themes, selectedTheme => selectedTheme.name == name);

        if (t != null)
        {

            if (themeSource.clip == t.track)
            {
                Debug.Log(t.name + " is already playing");
            }
            else
            {

                if (fade)
                {
                    themeSource.clip = t.track;
                
                    if (themeSource.isPlaying) {

                        //StartCoroutine(FadeTheme(themeSource,themeSource.volume,0f,1f));
                        //themeSource.Play();
                        //StartCoroutine(FadeTheme(themeSource, themeSource.volume, 1f, 1f));

                    }
                    else
                    {

                       // StartCoroutine(FadeTheme(themeSource,0,1f, 1f));

                    }
         
                }
                else
                {

                    themeSource.clip = t.track;
                    themeSource.Play();
                  
                }


                m_currentTheme = t;
            }


          
        }
        else
        {
            Debug.Log("Theme with name: " + name + " not found");
            return;
        }


       

    }
    public void StopTheme() { themeSource.Stop(); }
    public void ModifyVolume(float vol) { themeSource.volume = vol; }
    private Theme ReturnClip(string name){

        foreach(Theme t in themes)
        {
            if (t.name == name)
                return t;

        }

        Debug.Log("Clip not found");
        return null;
        
     }
    public static IEnumerator FadeTheme(AudioSource audioSource,float startVolume,float targetVolume, float duration)
    {
        float currentTime = 0;
        float start = startVolume;

        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(start, targetVolume, currentTime / duration);
            yield return null;
        }
        yield break;
    }

   //SOUND FUNCTIONS

    public void PlayOneShot(SFXs sound)
    {

    }


}
