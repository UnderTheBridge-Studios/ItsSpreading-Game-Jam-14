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
    [SerializeField] private float m_timeToStartBounce = 0.1f;
    [SerializeField] private float m_walkBounce = 2.7f;
    [SerializeField] private float m_walkBounceTime = 0.25f;
    [SerializeField] private float m_slowBounce = 2.6f;
    [SerializeField] private float m_slowBounceTime = 0.5f;

    [Header("References")]
    [SerializeField] private CharacterController m_controller;
    [SerializeField] private PlayerLook m_playerLook;

    [Header("Initial Position")]
    [SerializeField] private Vector3 m_initialPosition;
    [SerializeField] private Quaternion m_initialRotation;
    [SerializeField] private Quaternion m_initialCameraRotation;

    private GameObject m_currentAlien;
    private AlienController m_alienController;

    private Vector2 m_horizontalInput;
    private Vector3 m_horizontalVelocity;
    private Vector3 m_verticalVelocity;

    private float m_currentSpeed;
    private float m_currentBounce;
    private float m_currentBounceTime;

    private bool m_isGrounded;
    private bool m_isMoving;

    private float m_cameraTimer = 0;

    public bool IsGrounded => m_isGrounded;

    private void Awake()
    {
        m_currentSpeed = m_walkSpeed;
        m_currentBounce = m_walkBounce;
        m_currentBounceTime = m_walkBounceTime;
    }

    private void Update()
    {
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
        IsMovingCheck();

        //Camera Bounce
        if (m_isMoving)
            m_cameraTimer += Time.deltaTime;
        else
            m_cameraTimer = 0;

        if (m_cameraTimer >= m_timeToStartBounce)
            m_playerLook.CameraBounce(m_currentBounce, m_currentBounceTime);
        else
            m_playerLook.StopCameraBounce();

        //Alien exit via death check
        CheckAlienDeath();
    }

    public void ResetPosition()
    {
        gameObject.transform.position = m_initialPosition;
        gameObject.transform.rotation = m_initialRotation;
        m_playerLook.SetCameraRotation(m_initialCameraRotation);
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

    public void SetSlowSpeed()
    {
        m_currentSpeed = m_slowSpeed;
        m_currentBounce = m_slowBounce;
        m_currentBounceTime = m_slowBounceTime;
    }

    public void SetWalkingSpeed()
    {
        m_currentSpeed = m_walkSpeed;
        m_currentBounce = m_walkBounce;
        m_currentBounceTime = m_walkBounceTime;
    }

    private void CheckAlienDeath()
    {
        if (m_currentAlien == null)
            return;

        if (!m_currentAlien.GetComponent<AlienController>().CheckDeath())
            return;
            
        m_currentAlien.GetComponent<AlienController>().AlienStopHit();
        SetWalkingSpeed();
        m_currentAlien = null;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag != "Alien")
            return;

        if (!GameManager.instance.IsPoisoned)
        {
            HUBManager.instance.HealthBarActive(true);
            GameManager.instance.SetPoisonRate();
        }

        if (m_currentAlien != null && !m_alienController.StacksDamage)
        {
            m_alienController.AlienStopHit();
        }

        m_currentAlien = other.gameObject;
        m_alienController = m_currentAlien.GetComponent<AlienController>();
        m_alienController.AlienHits();

        SetSlowSpeed();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag != "Alien")
            return;

        if (other.gameObject.GetComponent<AlienController>().StacksDamage)
            other.gameObject.GetComponent<AlienController>().AlienStopHit();

        if (m_currentAlien != other.gameObject)
            return;

        m_alienController.AlienStopHit();
        m_currentAlien = null;

        SetWalkingSpeed();
    }
}
