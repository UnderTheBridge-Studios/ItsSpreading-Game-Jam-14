using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CryochamberController : MonoBehaviour
{
    [SerializeField] private Transform m_door;
    [SerializeField] private float m_rotation = -60f;
    [SerializeField] private float m_duration = 3f;
    [SerializeField] private float m_timeBeforeOpen = 3f;

    void Start()
    {
        StartCoroutine(OpenDoor());
    }

    private IEnumerator OpenDoor()
    {
        yield return new WaitForSeconds(m_timeBeforeOpen);
        SoundManager.instance.PlayClip(2,1);
        m_door.DOLocalRotate(new Vector3(m_rotation, 0, 0), m_duration);
    }
}
