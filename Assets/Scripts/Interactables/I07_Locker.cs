using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class I07_Locker : MonoBehaviour, IInteractable
{
    [SerializeField] private string m_id = "";
    [SerializeField] private string m_prompt = "Open Locker";

    private BoxCollider m_collider;

    public string ID => m_id;
    public string InteractionPromt => m_prompt;

    private void Awake()
    {
        m_collider = GetComponent<BoxCollider>();
    }
    public bool Interact(PlayerInteract interactor)
    {
        m_collider.enabled = false;
        Debug.Log("The locker " + ID + " has been opened.");
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
