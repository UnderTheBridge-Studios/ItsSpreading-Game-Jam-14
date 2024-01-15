using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashLightTest : MonoBehaviour
{

    private GameObject m_camera;


    private Vector3 m_cameraAngle;

    [SerializeField] private float m_range = 20f;
    [SerializeField] private GameObject m_LightProjectile;


    private void Awake()
    {
        m_camera =  this.gameObject;
    }

    void Update()
    {
        //m_cameraAngle = m_camera.transform.eulerAngles;
        //m_cameraAngle = transform.forward + new Vector3(Random.RandomRange()); 


        GameObject projectile = Instantiate(m_LightProjectile, transform.position, Quaternion.identity);
        projectile.GetComponent<LightProjectile>().Inicializate(transform.forward);

    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        //Gizmos.DrawRay(transform.position, m_camera.transform.forward*5);
        Gizmos.DrawRay(transform.position, transform.forward * m_range);
    }


}
