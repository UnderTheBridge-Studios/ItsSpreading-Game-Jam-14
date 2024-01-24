using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UINoteDisplay : MonoBehaviour
{
    [SerializeField] private Button m_BackgroundButton;

    private void Start()
    {
        m_BackgroundButton.Select();
        m_BackgroundButton.onClick.AddListener(CloseNote);
    }

    private void CloseNote()
    {
        GameManager.instance.Player.GetComponent<InputManager>().CloseNotePopUp();
    }
}
