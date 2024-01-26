using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienGroup : MonoBehaviour
{
    [SerializeField] private float m_spreadSpeed;
    [SerializeField] private float m_growSpeed;
    [SerializeField] private bool m_stacksDamage;


    [Header("Sound")]
    [SerializeField] private AudioSource m_growthSource;
    [SerializeField] private float m_growthTimeToStop = 0.5f;
    [SerializeField] private float m_growthVolume = 0.5f;
    [SerializeField] private AudioSource m_burnSource;
    [SerializeField] private float m_burnTimeToStop = 0.5f;
    [SerializeField] private float m_burnVolume = 0.5f;
    [SerializeField] private float m_fadeTime = 0.5f;

    private float m_growthTimer;
    private bool m_isGrowing;
    private float m_burnTimer;
    private bool m_isBurning;
    private Coroutine m_growthCoroutine;
    private Coroutine m_burnCoroutine;

    private Transform m_spreadingCenter;

    SortedList<float, GameObject> m_aliens;

    void Start()
    {
        m_aliens = new SortedList<float, GameObject>();
        m_spreadingCenter = gameObject.transform.GetChild(0).transform;

        //skip first child, the spreading center, and the second one, the trigger
        for (int i = 2; i < gameObject.transform.childCount; i++)
        {
            GameObject child = gameObject.transform.GetChild(i).gameObject;
            child.SetActive(false);
            child.GetComponentInChildren<AlienController>().StacksDamage = m_stacksDamage;
            m_aliens.Add(Vector3.Distance(m_spreadingCenter.position, child.transform.position), child);
        }
    }

    private void Update()
    {
        //Growth
        m_growthTimer += Time.deltaTime;

        if (m_growthTimer <= m_growthTimeToStop)
        {
            m_isGrowing = true;
        }
        else
        {
            m_isGrowing = false;

            if (m_growthSource.isPlaying)
                StopGrowingSound();
        }

        //Burn
        m_burnTimer += Time.deltaTime;

        if (m_burnTimer <= m_burnTimeToStop)
        {
            m_isBurning = true;
        }
        else
        {
            m_isBurning = false;

            if (m_burnSource.isPlaying)
                StopBurningSound();
        }
    }
    #region Growth
    public void IsGrowing()
    {
        m_growthTimer = 0;

        if (!m_isGrowing)
            StartGrowingSound();
    }

    private void StartGrowingSound()
    {
        if (m_growthCoroutine != null)
            StopCoroutine(m_growthCoroutine);

        m_growthCoroutine = StartCoroutine(SoundManager.instance.FadeIn(m_growthSource, m_growthVolume, m_fadeTime));
    }

    private void StopGrowingSound()
    {
        if (m_growthCoroutine != null)
            StopCoroutine(m_growthCoroutine);

        m_growthCoroutine = StartCoroutine(SoundManager.instance.FadeOut(m_growthSource, m_fadeTime));
    }
    #endregion
    #region Burn
    public void IsBurning()
    {
        m_burnTimer = 0;

        if (!m_isBurning)
            StartBurningSound();
    }

    private void StartBurningSound()
    {
        if (m_burnCoroutine != null)
            StopCoroutine(m_burnCoroutine);

        m_burnCoroutine = StartCoroutine(SoundManager.instance.FadeIn(m_burnSource, m_burnVolume, m_fadeTime));
    }

    private void StopBurningSound()
    {
        if (m_burnCoroutine != null)
            StopCoroutine(m_burnCoroutine);

        m_burnCoroutine = StartCoroutine(SoundManager.instance.FadeOut(m_burnSource, m_fadeTime));
    }
    #endregion

    public void StartSpreading()
    {
        foreach (var child in m_aliens)
        {
            StartCoroutine(child.Value.GetComponent<MaterialBlockGrowth>().Inicialize(child.Key * 10 / m_spreadSpeed, m_growSpeed));
        }
    }
}
