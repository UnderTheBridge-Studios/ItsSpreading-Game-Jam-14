using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienController : MonoBehaviour
{
    [Header("Alien Gets Hit")]
    [Tooltip("Wait time to recover sice last hit")]
    [SerializeField] private float m_timeRecovery = 5f;
    [Tooltip("Wait time to recover sice death")]
    [SerializeField] private float m_deathTimeRecovery = 10f;

    [Header("Alien Hits")]
    [SerializeField] private float m_damage = 0.5f;
    [SerializeField] private float m_timeForNextHit = 0.5f;

    [Header("References")]
    [SerializeField] private MaterialBlockGrowth m_materialGrowth;

    private BoxCollider m_collider;
    private float m_targuetGrowth;
    private Coroutine m_growthCoroutine;
    private Coroutine m_hitPlayerCoroutine;
    private bool m_isTouchingPlayer = false;

    private void Awake()
    {
        m_targuetGrowth = m_materialGrowth.CurrentGrowth;
        m_collider = GetComponent<BoxCollider>();
    }

    public void AlienHits()
    {
        m_isTouchingPlayer = true;
        m_hitPlayerCoroutine = StartCoroutine(AlienHitsCoroutine());
    }

    public void AlienStopHit()
    {
        m_isTouchingPlayer = false;
        StopCoroutine(m_hitPlayerCoroutine);
    }

    private IEnumerator AlienHitsCoroutine()
    {
        while(m_isTouchingPlayer)
        {
            GameManager.instance.SetPoison(m_damage);
            yield return new WaitForSeconds(m_timeForNextHit);
        }
    }

    public void AlienGetsHit(float value)
    {
        if (CheckDeath())
            return;

        if (m_materialGrowth.IsGrowing)
            m_targuetGrowth = m_materialGrowth.CurrentGrowth;


        m_targuetGrowth = m_targuetGrowth - value < 0 ? 0 : m_targuetGrowth - value;

        m_materialGrowth.DoSizeChange(m_targuetGrowth, false);

        if (m_growthCoroutine != null)
            StopCoroutine(m_growthCoroutine);
        if (m_targuetGrowth <= 0)
            m_growthCoroutine = StartCoroutine(AlienRecover(m_deathTimeRecovery));
        else
            m_growthCoroutine = StartCoroutine(AlienRecover(m_timeRecovery));
    }

    private IEnumerator AlienRecover(float time)
    {
        yield return new WaitForSeconds(time);
        m_collider.enabled = true;
        m_targuetGrowth = 1;
        m_materialGrowth.DoSizeChange(m_targuetGrowth, true);
    }

    private bool CheckDeath()
    {
        if (m_targuetGrowth <= 0)
        {
            m_collider.enabled = false;
            return true;
        }

        return false;
    }
}
