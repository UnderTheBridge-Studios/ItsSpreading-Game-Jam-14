using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class I08_Door : MonoBehaviour, IInteractable
{
    [SerializeField] private string m_id = "";
    [SerializeField] private string m_prompt = "Open Door";

    private BoxCollider m_collider;

    public string ID => m_id;
    public string InteractionPromt => m_prompt;

    private void Awake()
    {
        m_collider = GetComponent<BoxCollider>();
    }
    public virtual bool Interact(PlayerInteract interactor)
    {
        OpenDoor();
        return true;
    }
    public void OpenDoor()
    {
        m_collider.enabled = false;
        Debug.Log("Door: The door " + ID + " has been opened.");
    }
}
