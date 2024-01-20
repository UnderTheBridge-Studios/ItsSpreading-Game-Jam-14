using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class I01_Key : MonoBehaviour, IInteractable
{
    [SerializeField] private string m_id;
    [SerializeField] private string m_prompt = "Grab Key";

    public string ID => m_id;
    public string InteractionPromt => m_prompt;

    public bool Interact(PlayerInteract interactor)
    {
        Debug.Log("Key Grabbed");
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
