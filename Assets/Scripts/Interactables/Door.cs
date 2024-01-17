using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour, IInteractable
{
    [SerializeField] private string m_id;
    [SerializeField] private string m_prompt;

    public string ID => m_id;
    public string InteractionPromt => m_prompt;

    public bool Interact(PlayerInteract interactor)
    {
        Debug.Log("Door Opened");
        return true;
    }

    public void OnFocus()
    {
        Debug.Log("Focus on " + ID);
    }

    public void OnLoseFocus()
    {
        Debug.Log("Lose Focus on " + ID);
    }
}
