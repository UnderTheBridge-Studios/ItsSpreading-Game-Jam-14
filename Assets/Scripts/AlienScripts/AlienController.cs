using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienController : MonoBehaviour
{
    [SerializeField] private MaterialBlockGrowth m_materialGrowth;
    [Tooltip("Wait time to recover sice last hit")]
    [SerializeField] private float m_timeRecovery = 5f;
    [Tooltip("Wait time to recover sice death")]
    [SerializeField] private float m_deathTimeRecovery = 15f;

    private BoxCollider m_collider;

    private float m_targuetGrowth;

    private Coroutine m_GrowthCoroutine;

    private void Awake()
    {
        m_targuetGrowth = m_materialGrowth.CurrentGrowth;
        m_collider = GetComponent<BoxCollider>();
    }

    public void AlienGrowth (float value)
    {
        if (CheckDeath())
            return;

        if (m_targuetGrowth > 1)
            return;

        m_targuetGrowth += value;
        m_materialGrowth.DoSizeChange(m_targuetGrowth, true);
    }

    public void AlienGetsHit(float value)
    {
        if (CheckDeath())
            return;

        if (m_materialGrowth.IsGrowing)
            m_targuetGrowth = m_materialGrowth.CurrentGrowth;


        m_targuetGrowth = m_targuetGrowth - value < 0 ? 0 : m_targuetGrowth - value;

        m_materialGrowth.DoSizeChange(m_targuetGrowth, false);

        if (m_GrowthCoroutine != null)
            StopCoroutine(m_GrowthCoroutine);
        if (m_targuetGrowth <= 0)
            m_GrowthCoroutine = StartCoroutine(AlienRecover(m_deathTimeRecovery));
        else
            m_GrowthCoroutine = StartCoroutine(AlienRecover(m_timeRecovery));
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
