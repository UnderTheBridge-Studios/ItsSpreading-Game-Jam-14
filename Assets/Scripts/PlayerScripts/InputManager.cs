using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    [SerializeField] private PlayerMovement m_playerMovement;
    [SerializeField] private PlayerLook m_playerLook;
    [SerializeField] private PlayerInteract m_playerInteract;
    [SerializeField] private FlashLight m_flashlight;

    private PlayerControls m_controls;
    private PlayerControls.GamePlayActions m_gamePlay;
    private PlayerControls.MenuNavigationActions m_menuNavigation;
    private PlayerControls.NotesPopUpActions m_notesPopUp;

    private Camera m_camera;

    private void Awake()
    {
        m_controls = new PlayerControls();
        m_gamePlay = m_controls.GamePlay;
        m_menuNavigation = m_controls.MenuNavigation;
        m_notesPopUp = m_controls.NotesPopUp;

        m_camera = Camera.main;

        //Gameplay Inputs
        m_gamePlay.HorizontalMovement.performed += ctx => HorizontalMovement(ctx);
        m_gamePlay.Look.performed += ctx => Look(ctx);

        m_gamePlay.Interact.performed += _ => m_playerInteract.OnInteractPressed();
        m_gamePlay.Flashlight.performed += _ => m_flashlight.ToggleFlashlight();
        m_gamePlay.Recharge.performed += _ => GameManager.instance.ChargeBattery();
        m_gamePlay.Recharge.canceled += _ => GameManager.instance.StopChargingBattery();
        m_gamePlay.Pause.performed += _ => OpenPauseMenu();

        //Menu Navigation Inputs
        m_menuNavigation.Resume.performed += _ => ClosePauseMenu();

        //Notes PopUp Inputs
        m_notesPopUp.CloseNote.performed += _ => CloseNotePopUp();
        m_notesPopUp.CloseNoteMouse.performed += _ => CloseWithMouse();
    }
    private void OnEnable()
    {
        m_controls.Enable();
        m_menuNavigation.Disable();
        m_notesPopUp.Disable();
    }

    private void OnDisable()
    {
        m_controls.Disable();
    }

    private void Look(InputAction.CallbackContext context)
    {
        Vector2 input = context.ReadValue<Vector2>();
        m_playerLook.ReceiveInput(input);
    }

    private void HorizontalMovement(InputAction.CallbackContext context)
    {
        Vector2 input = context.ReadValue<Vector2>();
        m_playerMovement.ReceiveInput(input);
    }

    public void OpenPauseMenu()
    {
        GameManager.instance.PauseGame();
        HUBManager.instance.ShowPauseMenu();

        m_gamePlay.Disable();
        m_menuNavigation.Enable();
        m_notesPopUp.Disable();
    }

    public void ClosePauseMenu()
    {
        GameManager.instance.PauseGame();
        HUBManager.instance.HidePauseMenu();

        m_gamePlay.Enable();
        m_menuNavigation.Disable();
        m_notesPopUp.Disable();
    }

    public void OpenNotePopUp(string content)
    {
        GameManager.instance.PauseGame();
        HUBManager.instance.ShowNote(content);

        m_gamePlay.Disable();
        m_menuNavigation.Disable();
        m_notesPopUp.Enable();
    }

    public void CloseNotePopUp()
    {
        GameManager.instance.PauseGame();
        HUBManager.instance.HideNote();

        m_gamePlay.Enable();
        m_menuNavigation.Disable();
        m_notesPopUp.Disable();
    }

    private void CloseWithMouse()
    {
        RaycastHit2D rayHit = Physics2D.GetRayIntersection(m_camera.ScreenPointToRay(Mouse.current.position.ReadValue()));
        if (!rayHit.collider)
            CloseNotePopUp();
    }
}
