using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    [SerializeField] private Texture2D m_cursor;

    private GameObject m_canvas;
    private HUDManager m_HUD;
    private UINoteDisplay m_note;


    private void Awake()
    {
        Cursor.SetCursor(m_cursor, Vector2.zero, CursorMode.Auto);

        m_canvas = GameObject.Find("Canvas");
        m_HUD = m_canvas.GetComponentInChildren<HUDManager>();
        m_note = m_canvas.GetComponentInChildren<UINoteDisplay>();

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
        m_note.ShowNote(noteContent);
        InteractPromptActive(false);
    }

    public void HideNote()
    {
        m_note.HideNote();
    }
    #endregion
}
