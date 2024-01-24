using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class I08_SimpleDoor : I08_Door
{
    [SerializeField] private Transform m_topDoor;
    [SerializeField] private float m_topOrigin = 2.2f;
    [SerializeField] private float m_topFinal = 7f;
    [SerializeField] private float m_animationTime;

    public override void OpenDoor()
    {
        base.OpenDoor();
        m_topDoor.DOLocalMoveY(m_topFinal, m_animationTime);
    }

    public override void CloseDoor()
    {
        base.CloseDoor();
        m_topDoor.DOLocalMoveY(m_topOrigin, m_animationTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        CloseDoor();
    }
}
