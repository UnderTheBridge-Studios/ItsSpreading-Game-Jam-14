using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMainMenu : MonoBehaviour
{
    [SerializeField] private Button m_StartButton;
    [SerializeField] private Button m_SettingsButton;
    [SerializeField] private Button m_Exit;

    private void Start()
    {
        m_StartButton.onClick.AddListener(Resume);
        m_SettingsButton.onClick.AddListener(OpenSettings);
        m_Exit.onClick.AddListener(Exit);
    }

    private void Resume()
    {
        GameManager.instance.Player.GetComponent<InputManager>().CloseMainMenu();
    }

    private void OpenSettings()
    {

    }

    private void Exit()
    {
#if UNITY_STANDALONE
        Application.Quit();
#endif
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
