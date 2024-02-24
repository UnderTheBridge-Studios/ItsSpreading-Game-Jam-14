using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    [SerializeField] private Texture2D m_cursor;

    [SerializeField] private GameObject m_canvas;
    private HUDManager m_HUD;
    private UINoteDisplay m_note;
    private UIPauseMenu m_pauseMenu;
    private UIMainMenu m_mainMenu;


    private void Awake()
    {
        Cursor.SetCursor(m_cursor, Vector2.zero, CursorMode.Auto);

        m_HUD = m_canvas.GetComponentInChildren<HUDManager>(true); //include inactive objects
        m_note = m_canvas.GetComponentInChildren<UINoteDisplay>(true);
        m_pauseMenu = m_canvas.GetComponentInChildren<UIPauseMenu>(true);
        m_mainMenu = m_canvas.GetComponentInChildren<UIMainMenu>(true);
    }

    private void Start()
    {
        ResetHUD();
        m_pauseMenu.gameObject.SetActive(false);
    }

    #region HUD
    public void HideHUD()
    {
        m_HUD.gameObject.SetActive(false);
    }

    public void ShowHUD()
    {
        m_HUD.gameObject.SetActive(true);
    }

    public void ResetHUD()
    {
        m_HUD.ResetHUD();
    }

    public void PointerActive(bool value)
    {
        m_HUD.PointerActive(value);
    }

    public void ShowHealth()
    {
        m_HUD.ShowHealthBar();
    }

    public void UpdateInhibitors()
    {
        m_HUD.UpdateInhibitors();
    }

    public void InteractPromptActive(bool value, string prompt = "")
    {
        m_HUD.InteractPromptActive(value, prompt);
    }

    public void UseActionPrompt(Sprite sprite, string text, float time)
    {
        m_HUD.UseActionPrompt(sprite, text, time);
    }

    public void RechargingBar(bool value)
    {
        if (GameManager.instance.hasFlashlight)
            m_HUD.RechargingBarActive(value);
        else
            m_HUD.RechargingBarActive(false);
    }

    public void NeedToReharge(bool value)
    {
        m_HUD.RechargePromptActive(value);
    }

    #endregion

    #region Note
    public void ShowNote(string noteContent)
    {
        GameManager.instance.PauseGame();
        GameManager.instance.InputManager.RemoveAllControls();
        HideHUD();
        InteractPromptActive(false);

        float animationTime = m_note.ShowNote(noteContent);
        StartCoroutine(EnterNote(animationTime));
    }

    private IEnumerator EnterNote(float time)
    {
        yield return new WaitForSecondsRealtime(time);
        GameManager.instance.InputManager.SetNoteInput();
    }

    public void HideNote()
    {
        GameManager.instance.InputManager.RemoveAllControls();
        float animationTime = m_note.HideNote();
        StartCoroutine(ExitNote(animationTime));
    }

    private IEnumerator ExitNote(float time)
    {
        yield return new WaitForSecondsRealtime(time);
        ShowHUD();
        GameManager.instance.ResumeGame();
        GameManager.instance.InputManager.SetGameplayInput();
    }

    #endregion

    #region PauseMenu
    public void OpenPauseMenu()
    {
        GameManager.instance.PauseGame();
        GameManager.instance.InputManager.RemoveAllControls();
        HideHUD();
        InteractPromptActive(false);

        m_pauseMenu.gameObject.SetActive(true);
        float animationTime = m_pauseMenu.ShowMenu();
        StartCoroutine(EnterPauseMenu(animationTime));
    }

    private IEnumerator EnterPauseMenu(float time)
    {
        yield return new WaitForSecondsRealtime(time);
        GameManager.instance.InputManager.SetPauseInput();
    }

    public void ResumeGame()
    {
        GameManager.instance.InputManager.RemoveAllControls();
        float animationDuration = m_pauseMenu.HideMenu();
        StartCoroutine(ExitPauseMenu(animationDuration));
    }

    private IEnumerator ExitPauseMenu(float time)
    {
        yield return new WaitForSecondsRealtime(time);

        ShowHUD();
        GameManager.instance.ResumeGame();
        GameManager.instance.InputManager.SetGameplayInput();
        m_pauseMenu.gameObject.SetActive(false);
    }

    #endregion

    #region MainMenu
    public void ShowMainMenu()
    {
        HideHUD();
        GameManager.instance.InputManager.SetGameplayInput(true);
        m_mainMenu.gameObject.SetActive(true);
        m_mainMenu.OpenMenu();
    }

    #endregion
}
