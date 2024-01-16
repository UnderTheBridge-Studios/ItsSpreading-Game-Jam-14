using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashLight : MonoBehaviour
{

    [SerializeField] private GameObject m_camera;


    private Vector3 m_cameraAngle;

    [SerializeField] private GameObject m_LightProjectile;
    [SerializeField] private float m_range = 20f;
    [SerializeField] private float m_radius = 1f;
    [SerializeField] private float m_velocity = 2f;
    [SerializeField] private float m_ProjectileFov = 30f;

  
    private Coroutine m_shotingProjectile;

    public void EnableFlashlight()
    {
        m_shotingProjectile = StartCoroutine(ShootLightProjectile());
    }
    public void DisableFlashlight()
    {
        StopCoroutine(m_shotingProjectile);
    }


    private IEnumerator ShootLightProjectile() {
        while (true)
        {
            float projectileAngle = m_ProjectileFov / 2;

            Random.Range(projectileAngle, -projectileAngle);
            Quaternion randomRotation = Quaternion.AngleAxis(Random.Range(projectileAngle, -projectileAngle), m_camera.transform.up) * Quaternion.AngleAxis(Random.Range(projectileAngle, -projectileAngle), m_camera.transform.right);
            GameObject projectile = Instantiate(m_LightProjectile, m_camera.transform.position, Quaternion.identity);
            projectile.GetComponent<LightProjectile>().Inicializate(randomRotation * m_camera.transform.forward, m_range, m_velocity, m_radius);

            projectileAngle = m_ProjectileFov / 5;
            randomRotation = Quaternion.AngleAxis(Random.Range(projectileAngle, -projectileAngle), m_camera.transform.up) * Quaternion.AngleAxis(Random.Range(projectileAngle, -projectileAngle), m_camera.transform.right);
            GameObject centerProjectile = Instantiate(m_LightProjectile, m_camera.transform.position, Quaternion.identity);
            centerProjectile.GetComponent<LightProjectile>().Inicializate(randomRotation * m_camera.transform.forward, m_range, m_velocity, m_radius);

            yield return new WaitForSeconds(0.05f);
        }
    }


    //Draw the angle and range of the flashlight
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        //Edge lines
        Vector3 line1 = Quaternion.AngleAxis(m_ProjectileFov / 2, m_camera.transform.up) * Quaternion.AngleAxis(m_ProjectileFov / 2, m_camera.transform.right) * m_camera.transform.forward;
        Vector3 line2 = Quaternion.AngleAxis(m_ProjectileFov / 2, m_camera.transform.up) * Quaternion.AngleAxis(-m_ProjectileFov / 2, m_camera.transform.right) * m_camera.transform.forward;
        Vector3 line3 = Quaternion.AngleAxis(-m_ProjectileFov / 2, m_camera.transform.up) * Quaternion.AngleAxis(m_ProjectileFov / 2, m_camera.transform.right) * m_camera.transform.forward;
        Vector3 line4 = Quaternion.AngleAxis(-m_ProjectileFov / 2, m_camera.transform.up) * Quaternion.AngleAxis(-m_ProjectileFov / 2, m_camera.transform.right) * m_camera.transform.forward;

        Gizmos.DrawRay(m_camera.transform.position, line1 * m_range);
        Gizmos.DrawRay(m_camera.transform.position, line2 * m_range);
        Gizmos.DrawRay(m_camera.transform.position, line3 * m_range);
        Gizmos.DrawRay(m_camera.transform.position, line4 * m_range);


        //Botom lines
        Vector3[] points = new Vector3[4]
        {
            m_camera.transform.position + line1 * m_range,
            m_camera.transform.position + line2 * m_range,
            m_camera.transform.position + line4 * m_range,
            m_camera.transform.position + line3 * m_range
        };

        Gizmos.DrawLineStrip(points, true);
    }
}
