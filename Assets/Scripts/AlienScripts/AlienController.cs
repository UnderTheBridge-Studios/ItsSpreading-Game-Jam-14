using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienController : MonoBehaviour
{
    [SerializeField] private MaterialBlockGrowth m_materialGrowth;

    private BoxCollider m_collider;

    private float m_targuetGrowth;

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
        m_materialGrowth.DoSizeChange(m_targuetGrowth);
    }

    public void AlienDamage(float value)
    {
        if (CheckDeath())
            return;

        m_targuetGrowth -= value;
        m_materialGrowth.DoSizeChange(m_targuetGrowth);
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
