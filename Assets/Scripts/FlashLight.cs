using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashLight : MonoBehaviour
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
            float projectileAngle = m_ProjectileFov / 2;

            Random.Range(projectileAngle, -projectileAngle);
            Quaternion randomRotation = Quaternion.AngleAxis(Random.Range(projectileAngle, -projectileAngle), transform.up) * Quaternion.AngleAxis(Random.Range(projectileAngle, -projectileAngle), transform.right);
            GameObject projectile = Instantiate(m_LightProjectile, transform.position, Quaternion.identity);
            projectile.GetComponent<LightProjectile>().Inicializate(randomRotation * transform.forward, m_range, m_velocity, m_radius);

            projectileAngle = m_ProjectileFov / 5;
            randomRotation = Quaternion.AngleAxis(Random.Range(projectileAngle, -projectileAngle), transform.up) * Quaternion.AngleAxis(Random.Range(projectileAngle, -projectileAngle), transform.right);
            GameObject centerProjectile = Instantiate(m_LightProjectile, transform.position, Quaternion.identity);
            centerProjectile.GetComponent<LightProjectile>().Inicializate(randomRotation * transform.forward, m_range, m_velocity, m_radius);

            yield return new WaitForSeconds(0.05f);
        }
    }


    //Draw the angle and range of the flashlight
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        //Edge lines
        Vector3 line1 = Quaternion.AngleAxis(m_ProjectileFov / 2, transform.up) * Quaternion.AngleAxis(m_ProjectileFov / 2, transform.right) * transform.forward;
        Vector3 line2 = Quaternion.AngleAxis(m_ProjectileFov / 2, transform.up) * Quaternion.AngleAxis(-m_ProjectileFov / 2, transform.right) * transform.forward;
        Vector3 line3 = Quaternion.AngleAxis(-m_ProjectileFov / 2, transform.up) * Quaternion.AngleAxis(m_ProjectileFov / 2, transform.right) * transform.forward;
        Vector3 line4 = Quaternion.AngleAxis(-m_ProjectileFov / 2, transform.up) * Quaternion.AngleAxis(-m_ProjectileFov / 2, transform.right) * transform.forward;

        Gizmos.DrawRay(transform.position, line1 * m_range);
        Gizmos.DrawRay(transform.position, line2 * m_range);
        Gizmos.DrawRay(transform.position, line3 * m_range);
        Gizmos.DrawRay(transform.position, line4 * m_range);


        //Botom lines
        Vector3[] points = new Vector3[4]
        {
            transform.position + line1 * m_range,
            transform.position + line2 * m_range,
            transform.position + line4 * m_range,
            transform.position + line3 * m_range
        };

        Gizmos.DrawLineStrip(points, true);
    }
}
