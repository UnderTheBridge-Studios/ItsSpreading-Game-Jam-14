using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private CharacterController m_controller;
    [SerializeField] private float m_speed = 11f;
    [SerializeField] private float m_gravity = -15f;
    [SerializeField] private LayerMask m_groundMask;

    private Vector2 m_horizontalInput;
    private Vector3 m_horizontalVelocity;
    private Vector3 m_verticalVelocity;
    private bool m_isGrounded;

    private void Update()
    {
        //Is grounded Check
        m_isGrounded = Physics.CheckSphere(transform.position, 0.1f, m_groundMask);
        if (m_isGrounded)
        {
            m_verticalVelocity.y = 0;
        }

        //Horizontal Movement
        m_horizontalVelocity = (transform.right * m_horizontalInput.x + transform.forward * m_horizontalInput.y) * m_speed;
        m_controller.Move(m_horizontalVelocity * Time.deltaTime);

        //Vertical Movement
        m_verticalVelocity.y += m_gravity * Time.deltaTime;
        m_controller.Move(m_verticalVelocity * Time.deltaTime);
    }

    public void ReceiveInput(Vector2 _horizontalInput)
    {
        m_horizontalInput = _horizontalInput;
    }
}
