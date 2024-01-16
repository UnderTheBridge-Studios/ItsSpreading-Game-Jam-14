using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private CharacterController m_controller;
    [SerializeField] private PlayerLook m_playerLook;
    [SerializeField] private float m_speed = 8f;
    [SerializeField] private float m_gravity = -15f;
    [SerializeField] private LayerMask m_groundMask;

    private Vector2 m_horizontalInput;
    private Vector3 m_horizontalVelocity;
    private Vector3 m_verticalVelocity;
    private bool m_isGrounded;
    private bool m_isSprinting = false;
    private bool m_isMoving;
    private bool m_isPreviouslyMoving;

    public bool IsGrounded => m_isGrounded;
    public bool IsSprinting => m_isSprinting;

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

        //Movement Check
        m_isPreviouslyMoving = m_isMoving;
        IsMovingCheck();

        //Camera Bounce
        if (!m_isPreviouslyMoving && m_isMoving)
        {
            m_playerLook.CameraBounce();
        }
        else if(m_isPreviouslyMoving && !m_isMoving)
        {
            m_playerLook.StopCameraBounce();
        }
    }

    private void IsMovingCheck()
    {
        if(m_horizontalVelocity != Vector3.zero)
        {
            m_isMoving = true;
        }
        else
        {
            m_isMoving = false;
        }
    }

    public void ReceiveInput(Vector2 _horizontalInput)
    {
        m_horizontalInput = _horizontalInput;
    }
}
