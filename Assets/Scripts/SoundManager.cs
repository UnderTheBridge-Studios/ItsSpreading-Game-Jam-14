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

    [Header("Reload Sound")]
    [SerializeField] private AudioSource m_reloadSource;
    [SerializeField] private float m_reloadVolume = 0.5f;
    [SerializeField] private float m_reloadFadeTime = 0.2f;

    [Header("Other Sound")]
    [SerializeField] private AudioSource m_multipleSource;
    [SerializeField] private AudioClip[] m_clips;

    private Coroutine m_walkCoroutine;
    private Coroutine m_reloadCoroutine;

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
        if (m_walkCoroutine != null)
            StopCoroutine(m_walkCoroutine);

        m_walkCoroutine = StartCoroutine(FadeIn(m_walkingSource, m_walkingVolume, m_walkingFadeTime));
    }

    public void StopFootSteps()
    {
        if (m_walkCoroutine != null)
            StopCoroutine(m_walkCoroutine);

        m_walkCoroutine = StartCoroutine(FadeOut(m_walkingSource, m_walkingFadeTime));
    }

    public void PlayReload()
    {
        if (m_reloadCoroutine != null)
            StopCoroutine(m_reloadCoroutine);

        m_reloadCoroutine = StartCoroutine(FadeIn(m_reloadSource, m_reloadVolume, m_reloadFadeTime));
    }

    public void StopReload()
    {
        if (m_reloadCoroutine != null)
            StopCoroutine(m_reloadCoroutine);

        m_reloadCoroutine = StartCoroutine(FadeOut(m_reloadSource, m_reloadFadeTime));
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
        m_walkCoroutine = null;
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
        m_walkCoroutine = null;
    }
}
