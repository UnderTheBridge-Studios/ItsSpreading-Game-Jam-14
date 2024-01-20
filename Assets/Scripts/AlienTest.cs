using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AlienTest : MonoBehaviour
{
    [SerializeField] private RectTransform m_bar;

    [Range(0.0f, 10.0f)]
    [SerializeField] private float m_life;


    void Update()
    {
        m_bar.sizeDelta = new Vector2(m_life/10, 0.15f);
    }

    public void damage(float value)
    {
        m_life -= value;
    }
}
