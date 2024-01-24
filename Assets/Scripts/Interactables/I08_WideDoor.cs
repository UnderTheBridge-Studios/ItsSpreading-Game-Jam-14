using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class I08_WideDoor : I08_Door
{
    [SerializeField] private Transform m_topDoor;
    [SerializeField] private Vector3 m_topOrigin;
    [SerializeField] private Vector3 m_topFinal;
    [SerializeField] private float m_topTime;

    [SerializeField] private Transform m_bottomDoor;
    [SerializeField] private Vector3 m_bottomOrigin;
    [SerializeField] private Vector3 m_bottomFinal;
    [SerializeField] private float m_bottomTime;

    public override void OpenDoor()
    {
        base.OpenDoor();
        m_topDoor.DOLocalMove(m_topFinal, m_topTime);
        m_bottomDoor.DOLocalMove(m_bottomFinal, m_bottomTime);
    }

    public override void CloseDoor()
    {
        base.CloseDoor();
        m_topDoor.DOLocalMove(m_topOrigin, m_topTime);
        m_bottomDoor.DOLocalMove(m_bottomOrigin, m_bottomTime);
    }
}
