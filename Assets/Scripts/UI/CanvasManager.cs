using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    [SerializeField] private Texture2D m_cursor;

    [SerializeField] private GameObject m_canvas;
    private HUDManager m_HUD;
    private UINoteDisplay m_note;
    private UIPauseMenu m_pause;


    private void Awake()
    {
        Cursor.SetCursor(m_cursor, Vector2.zero, CursorMode.Auto);

        m_HUD = m_canvas.GetComponentInChildren<HUDManager>(true); //include inactive objects
        m_note = m_canvas.GetComponentInChildren<UINoteDisplay>(true);
        m_pause = m_canvas.GetComponentInChildren<UIPauseMenu>(true);
    }

    private void Start()
    {
        ResetHUD();
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

    #endregion

    #region Note
    public void ShowNote(string noteContent)
    {
        GameManager.instance.PauseGame();
        GameManager.instance.InputManager.RemoveAllControls();
        HideHUD();
        InteractPromptActive(false);


        float animationTime = m_note.ShowNote(noteContent);
        StartCoroutine(EnableNoteControl(animationTime));
    }

    private IEnumerator EnableNoteControl(float time)
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

    public void OpenPauseMenu()
    {
        GameManager.instance.PauseGame();

        m_pause.gameObject.SetActive(true);
        m_pause.OpenPauseMenu();

        GameManager.instance.InputManager.SetPauseInput();
    }

}
