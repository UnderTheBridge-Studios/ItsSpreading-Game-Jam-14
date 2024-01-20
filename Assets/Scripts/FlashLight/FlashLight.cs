using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class FlashLight : MonoBehaviour
{
    [SerializeField] private Transform m_playerInteractPoint;
    [SerializeField] private Light m_light;
    [SerializeField] private MeshRenderer m_lightMesh;

    [SerializeField] private GameObject m_LightProjectile;
    [SerializeField] private float m_range = 20f;
    [SerializeField] private float m_radius = 1f;
    [SerializeField] private float m_velocity = 2f;

    [Range(1f, 100f)]
    [SerializeField] private float m_projectileFov = 30f;
    [Range(0.01f, 1f)]
    [SerializeField] private float m_fireRate = 0.05f;
    [Tooltip("When the flashlight is flickering, will flicker on a random time between 0 and fickerRandTime")]
    [SerializeField] private float m_fickerRandTime = 0.5f;

    private Coroutine m_flickering;

    private bool m_isFlickering;
    private bool m_isFlashlightEnabled;

    private void Awake()
    {
        m_light.spotAngle = m_projectileFov;
        m_isFlickering = false;
        m_isFlashlightEnabled = false;
    }

    public void ToggleFlashlight()
    {
        if (!m_isFlashlightEnabled && GameManager.instance.batery > 0 && !GameManager.instance.isCharging)
        {

            m_isFlashlightEnabled = true;
            StartCoroutine(ShootLightProjectile());
        }
        else
            m_isFlashlightEnabled = false;
    }

    private IEnumerator ShootLightProjectile() {

        m_light.enabled = true;
        m_lightMesh.enabled = true;
        GameManager.instance.SetFlashlightActive(true);

        while (m_isFlashlightEnabled && !GameManager.instance.isCharging && GameManager.instance.batery > 0)
        { 
            if (GameManager.instance.isFlickering && !m_isFlickering)
                StartFlickering();

            float projectileAngle = m_projectileFov / 2;

            Random.Range(projectileAngle, -projectileAngle);
            Quaternion randomRotation = Quaternion.AngleAxis(Random.Range(projectileAngle, -projectileAngle), m_playerInteractPoint.up) * Quaternion.AngleAxis(Random.Range(projectileAngle, -projectileAngle), m_playerInteractPoint.transform.right);
            GameObject projectile = Instantiate(m_LightProjectile, m_playerInteractPoint.position, Quaternion.identity);
            projectile.GetComponent<LightProjectile>().Inicializate(randomRotation * m_playerInteractPoint.forward, m_range, m_velocity, m_radius);

            projectileAngle = m_projectileFov / 5;
            randomRotation = Quaternion.AngleAxis(Random.Range(projectileAngle, -projectileAngle), m_playerInteractPoint.up) * Quaternion.AngleAxis(Random.Range(projectileAngle, -projectileAngle), m_playerInteractPoint.transform.right);
            GameObject centerProjectile = Instantiate(m_LightProjectile, m_playerInteractPoint.position, Quaternion.identity);
            centerProjectile.GetComponent<LightProjectile>().Inicializate(randomRotation * m_playerInteractPoint.forward, m_range, m_velocity, m_radius);

            yield return new WaitForSeconds(m_fireRate);
        }

        if (GameManager.instance.batery <= 0)
            HUBManager.instance.RechargePromptActive(true);

        if (m_isFlickering)
            StopFlickering();

        m_isFlashlightEnabled = false;
        m_light.enabled = false;
        m_lightMesh.enabled = false;
        GameManager.instance.SetFlashlightActive(false);
    }

    private void StartFlickering()
    {
        m_isFlickering = true;
        m_flickering = StartCoroutine(Flickering());
    }

    private void StopFlickering()
    {
        if (m_flickering == null)
            return;

        m_isFlickering = false;
        StopCoroutine(m_flickering);
    }

    private IEnumerator Flickering()
    {
        while(true)
        {
            yield return new WaitForSeconds(Random.Range(0, m_fickerRandTime));
            m_light.enabled = false;
            m_lightMesh.enabled = false;
            yield return new WaitForSeconds(0.1f);
            m_light.enabled = true;
            m_lightMesh.enabled = true;
        }
    }

    //Draw the angle and range of the flashlight
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        //Edge lines
        Vector3 line1 = Quaternion.AngleAxis(m_projectileFov / 2, m_playerInteractPoint.up) * Quaternion.AngleAxis(m_projectileFov / 2, m_playerInteractPoint.right) * m_playerInteractPoint.forward;
        Vector3 line2 = Quaternion.AngleAxis(m_projectileFov / 2, m_playerInteractPoint.up) * Quaternion.AngleAxis(-m_projectileFov / 2, m_playerInteractPoint.right) * m_playerInteractPoint.forward;
        Vector3 line3 = Quaternion.AngleAxis(-m_projectileFov / 2, m_playerInteractPoint.up) * Quaternion.AngleAxis(m_projectileFov / 2, m_playerInteractPoint.right) * m_playerInteractPoint.forward;
        Vector3 line4 = Quaternion.AngleAxis(-m_projectileFov / 2, m_playerInteractPoint.up) * Quaternion.AngleAxis(-m_projectileFov / 2, m_playerInteractPoint.right) * m_playerInteractPoint.forward;

        Gizmos.DrawRay(m_playerInteractPoint.position, line1 * m_range);
        Gizmos.DrawRay(m_playerInteractPoint.position, line2 * m_range);
        Gizmos.DrawRay(m_playerInteractPoint.position, line3 * m_range);
        Gizmos.DrawRay(m_playerInteractPoint.position, line4 * m_range);

        //Botom lines
        Vector3[] points = new Vector3[4]
        {
            m_playerInteractPoint.position + line1 * m_range,
            m_playerInteractPoint.position + line2 * m_range,
            m_playerInteractPoint.position + line4 * m_range,
            m_playerInteractPoint.position + line3 * m_range
        };

        Gizmos.DrawLineStrip(points, true);
    }
}
