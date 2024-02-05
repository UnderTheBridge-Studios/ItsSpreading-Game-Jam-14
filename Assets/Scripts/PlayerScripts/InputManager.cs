using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private PlayerControls m_controls;
    private PlayerControls.GamePlayActions m_gamePlay;
    private PlayerControls.MainMenuNavigationActions m_mainMenuNavigation;
    private PlayerControls.PauseNavigationActions m_pauseNavigation;
    private PlayerControls.NotesPopUpActions m_notesPopUp;
    private PlayerControls.EndScreaActions m_endScrean;

    private void Awake()
    {
        //Input Maps
        m_controls = new PlayerControls();
        m_gamePlay = m_controls.GamePlay;
        m_mainMenuNavigation = m_controls.MainMenuNavigation;
        m_pauseNavigation = m_controls.PauseNavigation;
        m_notesPopUp = m_controls.NotesPopUp;
        m_endScrean = m_controls.EndScrea;

        //Gameplay Inputs
        m_gamePlay.HorizontalMovement.performed += ctx => HorizontalMovement(ctx);
        m_gamePlay.Look.performed += ctx => Look(ctx);
        m_gamePlay.Interact.performed += _ => Interact();
        m_gamePlay.Flashlight.performed += _ => ToggleFlashLight();

        m_gamePlay.Recharge.performed += _ => GameManager.instance.ChargeBattery();
        m_gamePlay.Recharge.canceled += _ => GameManager.instance.StopChargingBattery();
        m_gamePlay.Inhibitor.performed += _ => GameManager.instance.UseInhibitor();
        m_gamePlay.Pause.performed += _ => GameManager.instance.OpenPauseMenu();

        //Menu Navigation Inputs
        m_pauseNavigation.Cancel.performed += _ => HUBManager.instance.ResumeGame();

        //Notes PopUp Inputs
        m_notesPopUp.CloseNote.performed += _ => HUBManager.instance.HideNote();

        //EndgameScrean
        m_endScrean.Restart.performed += _ => GameManager.instance.CloseGame();

    }

    private void OnEnable()
    {
        m_controls.Enable();
        SetGameplayInput(false);

        /*m_mainMenuNavigation.Disable();
        m_pauseNavigation.Disable();
        m_notesPopUp.Disable();
        m_endScrean.Disable();*/
    }

    private void OnDisable()
    {
        m_controls.Disable();
    }

    private void Look(InputAction.CallbackContext context)
    {
        Vector2 input = context.ReadValue<Vector2>();
        GameManager.instance.PlayerLook.ReceiveInput(input);
    }

    private void HorizontalMovement(InputAction.CallbackContext context)
    {
        Vector2 input = context.ReadValue<Vector2>();
        GameManager.instance.PlayerMovement.ReceiveInput(input);
    }

    private void Interact()
    {
        GameManager.instance.PlayerInteract.OnInteractPressed();
    }

    private void ToggleFlashLight()
    {
        GameManager.instance.Flashlight.ToggleFlashlight();
    }

    public void RecoverControl()
    {
        SetGameplayInput(false);
        GameManager.instance.PauseGame();
    }

    public void SetGameplayInput(bool lockHorizontalMovement)
    {
        m_gamePlay.Enable();
        m_mainMenuNavigation.Disable();
        m_pauseNavigation.Disable();
        m_notesPopUp.Disable();
        m_endScrean.Disable();

        if (lockHorizontalMovement)
            m_gamePlay.HorizontalMovement.Disable();
        else
            m_gamePlay.HorizontalMovement.Enable();
    }

    public void SetPauseInput()
    {
        m_gamePlay.Disable();
        m_mainMenuNavigation.Disable();
        m_pauseNavigation.Enable();
        m_endScrean.Disable();
        m_notesPopUp.Disable();
    }

    public void SetMainMenuInput()
    {
        m_gamePlay.Disable();
        m_mainMenuNavigation.Enable();
        m_pauseNavigation.Disable();
        m_endScrean.Disable();
        m_notesPopUp.Disable();
    }

    public void SetEndScreenInput()
    {
        m_gamePlay.Disable();
        m_mainMenuNavigation.Disable();
        m_pauseNavigation.Disable();
        m_notesPopUp.Disable();
        m_endScrean.Enable();
    }

    public void SetNoteInput()
    {
        m_gamePlay.Disable();
        m_mainMenuNavigation.Disable();
        m_pauseNavigation.Disable();
        m_endScrean.Disable();
        m_notesPopUp.Enable();
    }

    /*public void OpenPauseMenu()
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
    }*/

    /*public void ClosePauseMenu()
    {
        if (HUBManager.instance.isPauseMenuOpening)
            return;

        HUBManager.instance.ResumeGame();
    }*/

    /*public void OpenNotePopUp(string content)
    {
        GameManager.instance.PauseGame();
        HUBManager.instance.ShowNote(content);

        m_gamePlay.Disable();
        m_mainMenuNavigation.Disable();
        m_pauseNavigation.Disable();
        m_endScrean.Disable();
        m_notesPopUp.Enable();
    }*/

    /*public void CloseNotePopUp()
    {
        HUBManager.instance.HideNote();
    }*/
}
