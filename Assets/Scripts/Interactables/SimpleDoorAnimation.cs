using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleDoorAnimation : MonoBehaviour
{
    [SerializeField] private Transform m_topDoor;
    [SerializeField] private float m_topOrigin = 2.2f;
    [SerializeField] private float m_topFinal = 7f;
    [SerializeField] private float m_topTime = 1f;

    public virtual void OpenDoorAnimation()
    {
        m_topDoor.DOLocalMoveY(m_topFinal, m_topTime);
        SoundManager.instance.SelectAudio(0,1);
    }

    public virtual void CloseDoorAnimation()
    {
        m_topDoor.DOLocalMoveY(m_topOrigin, m_topTime);
        SoundManager.instance.SelectAudio(1, 1);
    }
}
