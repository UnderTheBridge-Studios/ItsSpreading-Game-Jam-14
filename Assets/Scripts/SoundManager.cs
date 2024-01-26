using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance { get; private set; }

    [SerializeField] private AudioSource m_source;
    [SerializeField] private AudioClip[] m_clips;

    private void Awake()
    {
        if (instance != null && instance != this)
            Destroy(this);
        else
            instance = this;
    }

    public void PlayClip(int index, float volume)
    {
        m_source.PlayOneShot(m_clips[index], volume);
    }
}
