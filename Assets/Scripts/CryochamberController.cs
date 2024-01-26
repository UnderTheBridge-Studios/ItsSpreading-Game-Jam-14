using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CryochamberController : MonoBehaviour
{
    [SerializeField] private Transform m_door;
    [SerializeField] private float m_rotation = -60f;
    [SerializeField] private float m_duration = 7f;
    [SerializeField] private float m_timeBeforeOpen = 3f;
    [SerializeField] private float m_soundVolume = 0.3f;

    private float m_timeToOpen;


    void Start()
    {
        m_timeToOpen = 0;
        GameManager.instance.Cryochamber(this.gameObject);
        StartCoroutine(OpenDoor());
    }

    public void SetTime(float time)
    {
        m_timeBeforeOpen = time;
    }

    private IEnumerator OpenDoor()
    {
        yield return new WaitForSeconds(0.1f);
        yield return new WaitForSeconds(m_timeBeforeOpen);
        SoundManager.instance.PlayClip(2, m_soundVolume);
        m_door.DOLocalRotate(new Vector3(m_rotation, 0, 0), m_duration);
    }
}
