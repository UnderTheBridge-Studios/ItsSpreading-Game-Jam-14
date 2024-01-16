using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField] private PlayerMovement m_playerMovement;
    [SerializeField] private PlayerLook m_playerLook;
    [SerializeField] private PlayerInteract m_playerInteract;

    private PlayerControls m_controls;
    private PlayerControls.GroundMovementActions m_groundMovement;

    private Vector2 m_horizontalInput;
    private Vector2 m_lookInput;

    private void Awake()
    {
        m_controls = new PlayerControls();
        m_groundMovement = m_controls.GroundMovement;

        // groundMovement.[action].performed += context => do something
        m_groundMovement.HorizontalMovement.performed += ctx => m_horizontalInput = ctx.ReadValue<Vector2>();
        
        m_groundMovement.LookX.performed += ctx => m_lookInput.x = ctx.ReadValue<float>();
        m_groundMovement.LookY.performed += ctx => m_lookInput.y = ctx.ReadValue<float>();

        m_groundMovement.Interact.performed += _ => m_playerInteract.OnInteractPressed();
    }

    private void Update()
    {
        m_playerMovement.ReceiveInput(m_horizontalInput);
        m_playerLook.ReceiveInput(m_lookInput);
    }

    private void OnEnable()
    {
        m_controls.Enable();
    }

    private void OnDisable()
    {
        m_controls.Disable();
    }
}
