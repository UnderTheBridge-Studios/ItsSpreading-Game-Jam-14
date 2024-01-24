using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class I08_Door : MonoBehaviour, IInteractable
{
    [SerializeField] private string m_id = "";
    [SerializeField] private string m_prompt = "Open Door";
    [SerializeField] private Material m_material;

    public string ID => m_id;
    public string InteractionPromt => m_prompt;

    private BoxCollider m_collider;

    private void Awake()
    {
        m_collider = GetComponent<BoxCollider>();
    }
    public virtual bool Interact(PlayerInteract interactor)
    {
        OpenDoor();
        return true;
    }
    public virtual void OpenDoor()
    {
        m_collider.enabled = false;
    }

    public virtual void CloseDoor()
    {
        gameObject.tag = "Untagged";
        m_collider.enabled = true;
    }
}
