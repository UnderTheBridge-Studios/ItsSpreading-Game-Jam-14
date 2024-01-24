using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class I08_WideDoor : I08_Door
{
    [SerializeField] private Transform m_topDoor;
    [SerializeField] private float m_topOrigin = 2.45f;
    [SerializeField] private float m_topFinal = 6f;
    [SerializeField] private float m_topTime = 1f;

    [SerializeField] private Transform m_bottomDoor;
    [SerializeField] private float m_bottomOrigin = 1.25f;
    [SerializeField] private float m_bottomFinal = -1.3f;
    [SerializeField] private float m_bottomTime = 1f;

    public override void OpenDoor()
    {
        base.OpenDoor();
        m_topDoor.DOLocalMoveY(m_topFinal, m_topTime);
        m_bottomDoor.DOLocalMoveY(m_bottomFinal, m_bottomTime);
    }

    public override void CloseDoor()
    {
        base.CloseDoor();
        m_topDoor.DOLocalMoveY(m_topOrigin, m_topTime);
        m_bottomDoor.DOLocalMoveY(m_bottomOrigin, m_bottomTime);
    }
}
