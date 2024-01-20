using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class I05_Note : MonoBehaviour, IInteractable
{
    [SerializeField] private string m_id;
    [SerializeField] private string m_prompt = "Read Note";

    public string InteractionPromt => m_prompt;

    public string ID => m_id;

    public bool Interact(PlayerInteract interactor)
    {
        Debug.Log("Show Note");
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
