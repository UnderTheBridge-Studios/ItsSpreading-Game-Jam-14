using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienGroup : MonoBehaviour
{
    [SerializeField] private float m_spreadSpeed;
    [SerializeField] private float m_growSpeed;

    private Transform m_spreadingCenter;

    SortedList<float, GameObject> m_aliens;

    void Start()
    {
        m_aliens = new SortedList<float, GameObject>();
        m_spreadingCenter = gameObject.transform.GetChild(0).transform;

        //skip first child, the spreading center, and the second one, the trigger
        for (int i = 2; i < gameObject.transform.childCount; i++)
        {
            GameObject child = gameObject.transform.GetChild(i).gameObject;
            child.SetActive(false);
            m_aliens.Add(Vector3.Distance(m_spreadingCenter.position, child.transform.position), child);
        }
    }

    public void StartSpreading()
    {
        foreach (var child in m_aliens)
        {
            StartCoroutine(child.Value.GetComponent<MaterialBlockGrowth>().Inicialize(child.Key * 10 / m_spreadSpeed, m_growSpeed));
        }
    }
}
