using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    [SerializeField] private PlayerMovement m_playerMovement;
    [SerializeField] private PlayerLook m_playerLook;
    [SerializeField] private PlayerInteract m_playerInteract;
    [SerializeField] private FlashLight m_flashlight;

    private PlayerControls m_controls;
    private PlayerControls.GroundMovementActions m_groundMovement;
    private PlayerControls.MenuNavigationActions m_menuMovement;

    private void Awake()
    {
        m_controls = new PlayerControls();
        m_groundMovement = m_controls.GroundMovement;
        m_menuMovement = m_controls.MenuNavigation;

        m_groundMovement.HorizontalMovement.performed += ctx => HorizontalMovement(ctx);
        m_groundMovement.Look.performed += ctx => Look(ctx);

        m_groundMovement.Interact.performed += _ => m_playerInteract.OnInteractPressed();
        m_groundMovement.Flashlight.performed += _ => m_flashlight.EnableFlashlight();
        m_groundMovement.Flashlight.canceled += _ => m_flashlight.DisableFlashlight();
        m_groundMovement.Recharge.performed += _ => GameManager.instance.ChargeBattery();
        m_groundMovement.Recharge.canceled += _ => GameManager.instance.StopChargingBattery();
        m_groundMovement.Pause.performed += _ => Pause();
        m_menuMovement.Resume.performed += _ => Resume();
    }

    private void Look(InputAction.CallbackContext context)
    {
        var input = context.ReadValue<Vector2>();
        m_playerLook.ReceiveInput(input);
    }

    private void HorizontalMovement(InputAction.CallbackContext context)
    {
        var input = context.ReadValue<Vector2>();
        m_playerMovement.ReceiveInput(input);
    }

    private void OnEnable()
    {
        m_controls.Enable();
        m_menuMovement.Disable();
    }

    private void OnDisable()
    {
        m_controls.Disable();
    }

    private void Pause()
    {
        GameManager.instance.PauseGame();
        m_groundMovement.Disable();
        m_menuMovement.Enable();
    }

    private void Resume()
    {
        GameManager.instance.PauseGame();
        m_groundMovement.Enable();
        m_menuMovement.Disable();
    }
}
