using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightProjectile : MonoBehaviour
{
    private SphereCollider m_collider;
    private Vector3 m_direcction;

    [SerializeField] private float m_radius = 0.5f;
    [SerializeField] private float m_velocity = 0.5f;
    [SerializeField] private float m_range = 20;

    private void Awake()
    {
        m_collider = GetComponent<SphereCollider>();
        m_collider.radius = m_radius;
        m_direcction = Vector3.zero;
    }

    private IEnumerator Start()
    {
        //velocity*50 is the true velocity, fixed update its called 50times per second
        yield return new WaitForSeconds(m_range/(m_velocity*50)); 
        Destroy(this.gameObject);
    }

    public void Inicializate(Vector3 direction)
    {
        m_direcction = direction.normalized;

    }

    private void FixedUpdate()
    {
        transform.position = transform.position + (m_direcction * m_velocity);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, m_radius);
    }
}
