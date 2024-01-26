using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WideDoorAnimation : SimpleDoorAnimation
{
    [Header("Animation bottom values")]
    [SerializeField] private Transform m_bottomDoor;
    [SerializeField] private float m_bottomOrigin = 1.25f;
    [SerializeField] private float m_bottomFinal = -1.3f;
    [SerializeField] private float m_bottomTimeOpen = 1f;
    [SerializeField] private float m_bottomTimeClose = 0.7f;

    public override void OpenDoorAnimation()
    {
        base.OpenDoorAnimation();
        m_bottomDoor.DOLocalMoveY(m_bottomFinal, m_bottomTimeOpen);
    }

    public override void CloseDoorAnimation()
    {
        base.CloseDoorAnimation();
        m_bottomDoor.DOLocalMoveY(m_bottomOrigin, m_bottomTimeClose);
    }
}
