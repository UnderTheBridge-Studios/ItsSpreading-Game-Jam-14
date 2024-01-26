using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance { get; private set; }

    [Header("Walking Sound")]
    [SerializeField] private AudioSource m_walkingSource;
    [SerializeField] private float m_walkingVolume = 0.3f;
    [SerializeField] private float m_walkingFadeTime = 0.5f;

    [Header("Other Sound")]
    [SerializeField] private AudioSource m_multipleSource;
    [SerializeField] private AudioClip[] m_clips;

    private Coroutine m_fadeCoroutine;

    private void Awake()
    {
        if (instance != null && instance != this)
            Destroy(this);
        else
            instance = this;
    }

    public void PlayClip(int index, float volume)
    {
        m_multipleSource.PlayOneShot(m_clips[index], volume);
    }

    public void PlayFootSteps()
    {
        if (m_fadeCoroutine != null)
            StopCoroutine(m_fadeCoroutine);

        m_fadeCoroutine = StartCoroutine(FadeIn(m_walkingSource, m_walkingVolume, m_walkingFadeTime));
    }

    public void StopFootSteps()
    {
        if (m_fadeCoroutine != null)
            StopCoroutine(m_fadeCoroutine);

        m_fadeCoroutine = StartCoroutine(FadeOut(m_walkingSource, m_walkingFadeTime));
    }

    private IEnumerator FadeIn(AudioSource audioSource, float finalVolume, float fadeTime)
    {
        float startVolume = 0.2f;
        audioSource.volume = 0;
        audioSource.Play();

        while (audioSource.volume < finalVolume)
        {
            audioSource.volume += startVolume * Time.deltaTime / fadeTime;

            yield return null;
        }

        audioSource.volume = finalVolume;
        m_fadeCoroutine = null;
    }

    private IEnumerator FadeOut(AudioSource audioSource, float fadeTime)
    {
        float startVolume = audioSource.volume;

        while (audioSource.volume > 0)
        {
            audioSource.volume -= startVolume * Time.deltaTime / fadeTime;

            yield return null;
        }

        audioSource.Stop();
        audioSource.volume = startVolume;
        m_fadeCoroutine = null;
    }
}
