using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Values")]
    [SerializeField] private float m_walkSpeed = 8f;
    [SerializeField] private float m_slowSpeed = 4f;
    [SerializeField] private float m_gravity = -15f;
    [SerializeField] private LayerMask m_groundMask;

    [Header("Camera Bounce Values")]
    [SerializeField] private float m_walkBounce = 3f;
    [SerializeField] private float m_walkBounceTime = 0.25f;
    [SerializeField] private float m_slowBounce = 2.7f;
    [SerializeField] private float m_slowBounceTime = 0.5f;

    [Header("References")]
    [SerializeField] private CharacterController m_controller;
    [SerializeField] private PlayerLook m_playerLook;

    private Vector2 m_horizontalInput;
    private Vector3 m_horizontalVelocity;
    private Vector3 m_verticalVelocity;

    private float m_currentSpeed;
    private float m_currentBounce;
    private float m_currentBounceTime;

    private bool m_isGrounded;
    private bool m_isMoving;
    private bool m_isPreviouslyMoving;

    public bool IsGrounded => m_isGrounded;

    private void Awake()
    {
        m_currentSpeed = m_walkSpeed;
        m_currentBounce = m_walkBounce;
        m_currentBounceTime = m_walkBounceTime;
    }

    private void Update()
    {
        //Debug.Log("IsTouchingAlien" + m_isTouchingAlien);

        //Is grounded Check
        m_isGrounded = Physics.CheckSphere(transform.position, 0.1f, m_groundMask);
        if (m_isGrounded)
            m_verticalVelocity.y = 0;

        //Horizontal Movement
        m_horizontalVelocity = (transform.right * m_horizontalInput.x + transform.forward * m_horizontalInput.y) * m_currentSpeed;
        m_controller.Move(m_horizontalVelocity * Time.deltaTime);

        //Vertical Movement
        m_verticalVelocity.y += m_gravity * Time.deltaTime;
        m_controller.Move(m_verticalVelocity * Time.deltaTime);

        //Movement Check
        m_isPreviouslyMoving = m_isMoving;
        IsMovingCheck();

        //Camera Bounce
        if (!m_isPreviouslyMoving && m_isMoving)
            m_playerLook.CameraBounce(m_currentBounce, m_currentBounceTime);
        else if(m_isPreviouslyMoving && !m_isMoving)
            m_playerLook.StopCameraBounce();
    }

    private void IsMovingCheck()
    {
        if(m_horizontalVelocity != Vector3.zero)
            m_isMoving = true;
        else
            m_isMoving = false;
    }

    public void ReceiveInput(Vector2 _horizontalInput)
    {
        m_horizontalInput = _horizontalInput;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag != "Alien")
            return;

        m_currentSpeed = m_slowSpeed;
        m_currentBounce = m_slowBounce;
        m_currentBounceTime = m_slowBounceTime;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag != "Alien")
            return;

        m_currentSpeed = m_walkSpeed;
        m_currentBounce = m_walkBounce;
        m_currentBounceTime = m_walkBounceTime;
    }
}
