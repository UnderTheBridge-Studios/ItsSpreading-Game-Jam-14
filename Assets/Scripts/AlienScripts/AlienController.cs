using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienController : MonoBehaviour
{
    [Range(0.0f, 1.0f)]
    [SerializeField] private float m_size;
    private MaterialPropertyBlock m_materialBlock;
    private MeshRenderer m_meshRenderer;
    private BoxCollider m_collider;

    private void Awake()
    {
        m_materialBlock = new MaterialPropertyBlock();
        m_meshRenderer = GetComponent<MeshRenderer>();
        m_collider = GetComponent<BoxCollider>();
        SetSize(m_size);
    }

    public void AlienGrowth (float value)
    {
        m_size += value;
        SetSize(m_size);
    }

    public void AlienDamage(float value)
    {
        m_size -= value;
        SetSize(m_size);

        Debug.Log("Damaged");
    }

    private void SetSize(float newSize)
    {
        if (newSize >= 0 && newSize <= 1)
        {
            ChangePropertyBlock(newSize);
            m_collider.size = new Vector3(newSize, 0.5f, newSize);
        }
        else
        {
            m_collider.enabled = false;
            Debug.Log("Alien death");
        }
    }
    private void ChangePropertyBlock(float amount)
    {
        m_materialBlock.SetFloat("_Growth", amount);
        m_meshRenderer.SetPropertyBlock(m_materialBlock);
    }
}
