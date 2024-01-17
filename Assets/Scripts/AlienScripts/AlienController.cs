using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienController : MonoBehaviour
{
    [Range(0.0f, 1.0f)]
    [SerializeField] private float m_growth;
    [SerializeField] private float m_smoothness = 0.5f;
    [SerializeField] private MeshRenderer m_meshRenderer;
    [SerializeField] private GameObject m_visualCollider;

    private float m_life;
    private MaterialPropertyBlock m_materialBlock;
    private BoxCollider m_collider;

    private void Awake()
    {
        m_materialBlock = new MaterialPropertyBlock();
        m_collider = GetComponent<BoxCollider>();
        m_life = m_growth;
        SetSize(m_growth);
    }

    public void AlienGrowth (float value)
    {
        m_life += value;
        m_growth = Mathf.SmoothStep(m_growth, m_life, m_smoothness);
        SetSize(m_growth);
    }

    public void AlienDamage(float value)
    {
        m_life -= value;
        m_growth = Mathf.SmoothStep(m_growth, m_life, m_smoothness);
        SetSize(m_growth);
    }

    private void SetSize(float newSize)
    {
        if (newSize >= 0 && newSize <= 1)
        {
            if (m_collider.enabled == false)
                m_collider.enabled = true;

            ChangePropertyBlock(newSize);
            m_collider.size = new Vector3(newSize, 0.5f, newSize);
        }
        else
        {
            m_collider.enabled = false;
            Debug.Log("Alien death");
        }

        m_visualCollider.transform.localScale = m_collider.size;
    }
    private void ChangePropertyBlock(float amount)
    {
        m_materialBlock.SetFloat("_Growth", amount);
        m_meshRenderer.SetPropertyBlock(m_materialBlock);
    }
}
