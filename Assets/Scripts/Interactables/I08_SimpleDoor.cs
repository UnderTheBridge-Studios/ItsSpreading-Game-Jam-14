using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class I08_SimpleDoor : I08_Door
{
    [SerializeField] private Transform m_topDoor;
    [SerializeField] private Vector3 m_topOrigin;
    [SerializeField] private Vector3 m_topFinal;
    [SerializeField] private float m_animationTime;

    public override void OpenDoor()
    {
        base.OpenDoor();
        m_topDoor.DOLocalMove(m_topFinal, m_animationTime);
    }

    public override void CloseDoor()
    {
        base.CloseDoor();
        m_topDoor.DOLocalMove(m_topOrigin, m_animationTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        CloseDoor();
    }
}
