using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class I05_Note : MonoBehaviour, IInteractable
{
    [SerializeField] private string m_id;
    [SerializeField] private string m_prompt = "Read Note";
    [SerializeField] private string m_content = "";

    private InputManager m_inputManager;
    public string InteractionPromt => m_prompt;
    public string ID => m_id;

    public bool Interact(PlayerInteract interactor)
    {
        HUBManager.instance.ShowNote(m_content);

        m_inputManager = interactor.GetComponent<InputManager>();
        m_inputManager.Pause();

        return true;
    }

    public void OnFocus()
    {
        return;
    }

    public void OnLoseFocus()
    {
        return;
    }
}
