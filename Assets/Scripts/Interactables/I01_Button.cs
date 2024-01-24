using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class I01_Button : MonoBehaviour, IInteractable
{
    [SerializeField] private string m_id = "";
    [SerializeField] private string m_prompt = "Press Button";
    [SerializeField] private I02_DoorButton m_doorButton;

    public string InteractionPromt => m_prompt;

    public string ID => m_id;

    public bool Interact(PlayerInteract interactor)
    {
        gameObject.tag = "Untagged";
        m_doorButton.LockDoor(false);
        return true;
    }
}
