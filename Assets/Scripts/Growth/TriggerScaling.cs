using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerScaling : MonoBehaviour
{
    private float m_currPos = 0;
    private MaterialBlockGrowth m_materialGrowth;
    private Transform m_parentTransform;

    private void Awake()
    {
        m_materialGrowth = GetComponentInParent<MaterialBlockGrowth>();
        m_parentTransform = GetComponentInParent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!m_materialGrowth.IsChangingSize)
            return;

        m_currPos = m_materialGrowth.CurrentGrowth;

        // Use Mathf.SmoothStep for smoother transitions
        float smoothPos = Mathf.SmoothStep(0f, 1f, m_currPos);

        m_parentTransform.transform.localScale = Vector3.Lerp(new Vector3(0.0f, 0.0f, 0.0f), new Vector3(1f, 1f, 1f), smoothPos);
    }
}
