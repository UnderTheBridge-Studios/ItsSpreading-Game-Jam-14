using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIPauseMenu : MonoBehaviour
{
    [SerializeField] private Button m_ResumeButton;
    [SerializeField] private Button m_SettingsButton;
    [SerializeField] private Button m_Exit;

    private void Start()
    {
        m_ResumeButton.onClick.AddListener(Resume);
        m_SettingsButton.onClick.AddListener(OpenSettings);
        m_Exit.onClick.AddListener(Exit);
    }

    private void Resume()
    {
        GameManager.instance.Player.GetComponent<InputManager>().ClosePauseMenu();
    }
    
    private void OpenSettings()
    {

    }

    private void Exit()
    {
        //Go to main menu
    }
}
