using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }

    [SerializeField]
    private float m_maxPoisonRate;
    private float m_poisonRate;
    private float m_poison;

    public float poison => m_poison;
    public float poisonRate => m_poisonRate;


    private void Awake()
    {
        if (instance != null && instance != this)
            Destroy(this);
        else
            instance = this;
    }

    public void ResetValues()
    {
        m_poison = 0;
        m_poisonRate = 0;
    }

    private void Update()
    {
        m_poison += m_poisonRate;
    }

    public void SetPoison(float newPoison)
    {
        m_poison = Mathf.Clamp(newPoison, 0f, 100f); 
    }

    public void SetPoisonRate(float newPoisonRate)
    {
        m_poisonRate = Mathf.Clamp(newPoisonRate, 0f, 100f);
    }
}
