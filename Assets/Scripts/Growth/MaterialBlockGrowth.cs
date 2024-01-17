using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialBlockGrowth : MonoBehaviour
{
    [Range(0.0f, 1.0f)]
    [SerializeField] private float m_currentPosition = 1;
    [SerializeField] private float m_growthSpeed = 1;

    //Material instance properties
    public MaterialPropertyBlock m_materialBlock;
    private MeshRenderer m_meshRenderer;

    private bool m_isChangingSize = false;
    private float m_speedModifier = 1f;
    private float m_interpolation = 0.0f;
    private float m_initialPosition = 0;
    private float m_targuetPosition;

    public float CurrentGrowth => m_currentPosition;
    public bool IsChangingSize => m_isChangingSize;


    void Awake()
    {
        m_materialBlock = new MaterialPropertyBlock();
        m_meshRenderer = GetComponent<MeshRenderer>();
    }


    void Update()
    {
        if (!m_isChangingSize)
            return;

        ChangeSize();
        Debug.Log("Is Growing?: " + m_isChangingSize);
    }

    public void DoSizeChange(float targuetPosition)
    {
        m_initialPosition = m_currentPosition;
        m_targuetPosition = targuetPosition;
        m_isChangingSize = true;
    }

    private void ChangeSize()
    {
        m_currentPosition = Mathf.Lerp(m_initialPosition, m_targuetPosition, m_interpolation);
        ChangePropertyBlock(m_currentPosition);
        m_interpolation += (m_growthSpeed*m_speedModifier) * Time.deltaTime;
        
        if (m_currentPosition == m_targuetPosition)
        {
            m_isChangingSize = false;
            m_interpolation = 0.0f;
        }
    } 

    private void ChangePropertyBlock(float amount)
    {
        m_materialBlock.SetFloat("_Growth", amount);
        m_meshRenderer.SetPropertyBlock(m_materialBlock);
    }
}
