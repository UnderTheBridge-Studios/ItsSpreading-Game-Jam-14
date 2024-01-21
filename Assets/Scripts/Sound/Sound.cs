using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class Theme 
{
    public string name;
    
    public AudioClip track;

}

[System.Serializable]
public class SFXs
{
    public string name;

    public AudioClip [] clips;

    private AudioSource Channel;

}