using DG.Tweening;
using System.Collections;
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
    private PlayerControls.MainMenuNavigationActions m_mainMenuNavigation;
    private PlayerControls.PauseNavigationActions m_pauseNavigation;
    private PlayerControls.NotesPopUpActions m_notesPopUp;
    private PlayerControls.EndScreaActions m_endScrean;

    private Camera m_camera;

    private void Awake()
    {
        m_controls = new PlayerControls();
        m_gamePlay = m_controls.GamePlay;
        m_mainMenuNavigation = m_controls.MainMenuNavigation;
        m_pauseNavigation = m_controls.PauseNavigation;
        m_notesPopUp = m_controls.NotesPopUp;
        m_endScrean = m_controls.EndScrea;

        //Gameplay Inputs
        m_gamePlay.HorizontalMovement.performed += ctx => HorizontalMovement(ctx);
        m_gamePlay.Look.performed += ctx => Look(ctx);

        m_gamePlay.Interact.performed += _ => m_playerInteract.OnInteractPressed();
        m_gamePlay.Flashlight.performed += _ => m_flashlight.ToggleFlashlight();
        m_gamePlay.Recharge.performed += _ => GameManager.instance.ChargeBattery();
        m_gamePlay.Recharge.canceled += _ => GameManager.instance.StopChargingBattery();
        m_gamePlay.Inhibitor.performed += _ => GameManager.instance.UseInhibitor();
        m_gamePlay.Pause.performed += _ => OpenPauseMenu();

        //Menu Navigation Inputs
        m_pauseNavigation.Cancel.performed += _ => ClosePauseMenu();

        //Notes PopUp Inputs
        m_notesPopUp.CloseNote.performed += _ => CloseNotePopUp();

        //EndgameScrean
        m_endScrean.Restart.performed += _ => CloseGame();

    }
    private void OnEnable()
    {
        m_controls.Enable();
        m_mainMenuNavigation.Disable();
        m_pauseNavigation.Disable();
        m_notesPopUp.Disable();
        m_endScrean.Disable();
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

    public void SetGameplayInputs()
    {
        m_gamePlay.Enable();
        m_mainMenuNavigation.Disable();
        m_pauseNavigation.Disable();
        m_notesPopUp.Disable();
        m_endScrean.Disable();
    }

    public void SetCinematicInputs()
    {
        m_gamePlay.Disable();
        m_mainMenuNavigation.Disable();
        m_pauseNavigation.Disable();
        m_notesPopUp.Disable();
        m_endScrean.Disable();
    }

    public void LockMovement(bool value)
    {
        m_gamePlay.Enable();
        m_mainMenuNavigation.Disable();
        m_pauseNavigation.Disable();
        m_notesPopUp.Disable();
        m_endScrean.Disable();

        if (value)
            m_gamePlay.HorizontalMovement.Disable();
        else
            m_gamePlay.HorizontalMovement.Enable();
    }

    #region Menus

    public void OpenPauseMenu()
    {
        if (HUBManager.instance.isPauseMenuOpening)
            return;

        GameManager.instance.PauseGame();
        HUBManager.instance.PauseMenuActive();

        m_gamePlay.Disable();
        m_mainMenuNavigation.Disable();
        m_pauseNavigation.Enable();
        m_endScrean.Disable();
        m_notesPopUp.Disable();
    }

    public void ClosePauseMenu()
    {
        if (HUBManager.instance.isPauseMenuOpening)
            return;

        HUBManager.instance.ResumeGame();
    }

    public void OpenNotePopUp(string content)
    {
        GameManager.instance.PauseGame();
        HUBManager.instance.ShowNote(content);

        m_gamePlay.Disable();
        m_mainMenuNavigation.Disable();
        m_pauseNavigation.Disable();
        m_endScrean.Disable();
        m_notesPopUp.Enable();
    }

    public void CloseNotePopUp()
    {
        HUBManager.instance.HideNote();
    }

    public void RecoverControl()
    {
        SetGameplayInputs();
        GameManager.instance.PauseGame();
    }

    public void EnableEndScrean()
    {
        m_gamePlay.Disable();
        m_mainMenuNavigation.Disable();
        m_pauseNavigation.Disable();
        m_notesPopUp.Disable();
        m_endScrean.Enable();
    }

    private void CloseGame()
    {
#if UNITY_STANDALONE
        Application.Quit();
#endif
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

    #endregion
}
