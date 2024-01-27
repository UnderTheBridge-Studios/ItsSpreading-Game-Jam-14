using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightProjectile : MonoBehaviour
{
    private SphereCollider m_collider;
    private Vector3 m_direcction;

    private float m_radius = 0.5f;
    private float m_velocity = 0.5f;
    private float m_range = 20;
    [SerializeField] private float m_damage = 0.1f;

    private void Awake()
    {
        m_collider = GetComponent<SphereCollider>();
        m_direcction = Vector3.zero;
        this.gameObject.SetActive(false);
    }

    private IEnumerator Start()
    {
        m_collider.radius = m_radius;

        //velocity*50 is the true velocity, fixed update its called 50times per second
        yield return new WaitForSeconds(m_range/(m_velocity*50)); 
        Destroy(this.gameObject);
    }

    public void Inicializate(Vector3 direction, float range, float velocity, float radius)
    {
        m_direcction = direction.normalized;
        m_velocity = velocity;
        m_range = range;
        m_radius = radius;

        this.gameObject.SetActive(true);
    }

    private void FixedUpdate()
    {
        transform.position = transform.position + (m_direcction * m_velocity);
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.tag == "Alien")
        {
            try
            {
                collision.GetComponent<AlienController>().AlienGetsHit(m_damage);
            }
            catch(NullReferenceException)
            {
                collision.GetComponent<AlienTest>().damage(m_damage);
            }
            Destroy(this.gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(this.gameObject);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, m_radius);
    }
}
