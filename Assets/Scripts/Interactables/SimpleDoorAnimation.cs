using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleDoorAnimation : MonoBehaviour
{
    [Header("Animation Values")]
    [SerializeField] private Transform m_topDoor;
    [SerializeField] private float m_topOrigin = 2.2f;
    [SerializeField] private float m_topFinal = 7f;
    [SerializeField] private float m_topTimeOpen = 1f;
    [SerializeField] private float m_topTimeClose = 0.7f;

    [Header("Sound values")]
    [SerializeField] private AudioSource m_audioSource;
    [SerializeField] private AudioClip m_openDoor;
    [SerializeField] private AudioClip m_closeDoor;
    [SerializeField] private float m_soundVolumeOpen = 0.5f;
    [SerializeField] private float m_soundVolumeClose = 0.2f;

    public virtual void OpenDoorAnimation()
    {
        m_topDoor.DOLocalMoveY(m_topFinal, m_topTimeOpen);

        if (m_audioSource.isPlaying)
            m_audioSource.Stop();

        m_audioSource.clip = m_openDoor;
        m_audioSource.volume = m_soundVolumeOpen;
        m_audioSource.Play();
    }

    public virtual void CloseDoorAnimation()
    {
        m_topDoor.DOLocalMoveY(m_topOrigin, m_topTimeClose);
        
        if (m_audioSource.isPlaying)
            m_audioSource.Stop();

        m_audioSource.clip = m_closeDoor;
        m_audioSource.volume = m_soundVolumeClose;
        m_audioSource.Play();
    }
}
