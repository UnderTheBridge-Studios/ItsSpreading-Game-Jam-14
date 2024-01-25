using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WideDoorAnimation : SimpleDoorAnimation
{
    [SerializeField] private Transform m_bottomDoor;
    [SerializeField] private float m_bottomOrigin = 1.25f;
    [SerializeField] private float m_bottomFinal = -1.3f;
    [SerializeField] private float m_bottomTime = 1f;

    public override void OpenDoorAnimation()
    {
        base.OpenDoorAnimation();
        m_bottomDoor.DOLocalMoveY(m_bottomFinal, m_bottomTime);
    }

    public override void CloseDoorAnimation()
    {
        base.CloseDoorAnimation();
        m_bottomDoor.DOLocalMoveY(m_bottomOrigin, m_bottomTime);
    }
}
