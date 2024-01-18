using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialBlockGrowth : MonoBehaviour
{
    [Range(0.0f, 1.0f)]
    [SerializeField] private float m_currentPosition = 1;
    [SerializeField] private float m_growthSpeed = 1;
    [SerializeField] private float m_speedGrowthModifier = 0.3f;
    [SerializeField] private float m_speedShrinkModifier = 1f;

    //Material instance properties
    public MaterialPropertyBlock m_materialBlock;
    private MeshRenderer m_meshRenderer;

    private bool m_isChangingSize = false;
    private bool m_isGrowing = false;
    private float m_speedModifier;
    private float m_interpolation = 0.0f;
    private float m_initialPosition = 0;
    private float m_targuetPosition;

    public float CurrentGrowth => m_currentPosition;
    public bool IsChangingSize => m_isChangingSize;
    public bool IsGrowing => m_isGrowing;


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
    }

    public void DoSizeChange(float targuetPosition, bool grow)
    {
        m_initialPosition = m_currentPosition;
        m_targuetPosition = targuetPosition;
        m_speedModifier = grow ? m_speedGrowthModifier : m_speedShrinkModifier;
        m_isGrowing = grow ? true : false;
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
            m_isGrowing = false;
            m_interpolation = 0.0f;
        }
    } 

    private void ChangePropertyBlock(float amount)
    {
        m_materialBlock.SetFloat("_Growth", amount);
        m_meshRenderer.SetPropertyBlock(m_materialBlock);
    }
}
