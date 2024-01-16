using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    [SerializeField] private Transform m_playerCamera;
    [SerializeField] private float m_sensitivityX = 8f;
    [SerializeField] private float m_sensitivityY = 0.5f;
    private float m_xClamp = 85f;

    private float m_xRotation = 0f;
    private float m_lookX;
    private float m_lookY;

    private void Start()
    {
        //Confine and hide cursor
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    private void Update()
    {
        transform.Rotate(Vector3.up, m_lookX * Time.deltaTime);

        m_xRotation -= m_lookY;
        m_xRotation = Mathf.Clamp(m_xRotation, -m_xClamp, m_xClamp);

        Vector3 targetRotation = transform.eulerAngles;
        targetRotation.x = m_xRotation;
        m_playerCamera.eulerAngles = targetRotation;
    }
    public void ReceiveInput(Vector2 cameraInput)
    {
        m_lookX = cameraInput.x * m_sensitivityX;
        m_lookY = cameraInput.y * m_sensitivityY;
    }
}
