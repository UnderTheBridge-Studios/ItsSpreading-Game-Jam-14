using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashLightTest : MonoBehaviour
{

    private GameObject m_camera;


    private Vector3 m_cameraAngle;

    [SerializeField] private GameObject m_LightProjectile;
    [SerializeField] private float m_range = 20f;
    [SerializeField] private float m_radius = 0.5f;
    [SerializeField] private float m_velocity = 0.5f;
    [SerializeField] private float m_ProjectileFov;

  
    private Coroutine m_shotingProjectile;


    private void Awake()
    {
        m_camera =  this.gameObject;
    }

    private void Start()
    {
        m_shotingProjectile = StartCoroutine(ShootLightProjectile());
    }

    private IEnumerator ShootLightProjectile() {
        while (true)
        {
            float ProjectileAngle = m_ProjectileFov / 2;
            Quaternion randomRotation = Quaternion.Euler(Random.Range(-ProjectileAngle, ProjectileAngle), Random.Range(-ProjectileAngle, ProjectileAngle), Random.Range(-ProjectileAngle, ProjectileAngle));
            GameObject projectile = Instantiate(m_LightProjectile, transform.position, Quaternion.identity);
            projectile.GetComponent<LightProjectile>().Inicializate(randomRotation * transform.forward, m_range, m_velocity, m_radius);


            ProjectileAngle = m_ProjectileFov / 5;
            randomRotation = Quaternion.Euler(Random.Range(-ProjectileAngle, ProjectileAngle), Random.Range(-ProjectileAngle, ProjectileAngle), Random.Range(-ProjectileAngle, ProjectileAngle));
            GameObject centerProjectile = Instantiate(m_LightProjectile, transform.position, Quaternion.identity);
            centerProjectile.GetComponent<LightProjectile>().Inicializate(randomRotation * transform.forward, m_range, m_velocity, m_radius);

            yield return new WaitForSeconds(0.05f);
        }
    }



    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, transform.forward * m_range);

        //Gizmos.DrawRay(transform.position, Quaternion.Euler(m_ProjectileFov / 2, m_ProjectileFov / 2, m_ProjectileFov / 2) * transform.forward * m_range);
        //Gizmos.DrawRay(transform.position, Quaternion.Euler(m_ProjectileFov / 2, -m_ProjectileFov / 2, m_ProjectileFov / 2) * transform.forward * m_range);
        //Gizmos.DrawRay(transform.position, Quaternion.Euler(-m_ProjectileFov / 2, m_ProjectileFov / 2, m_ProjectileFov / 2) * transform.forward * m_range);
        //Gizmos.DrawRay(transform.position, Quaternion.Euler(-m_ProjectileFov / 2, -m_ProjectileFov / 2, m_ProjectileFov / 2) * transform.forward * m_range);
    }


}
