using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class I04_DoorKey : MonoBehaviour, IInteractable
{
    [SerializeField] private string m_id;
    [SerializeField] private string m_prompt = "Open Door";

    private BoxCollider m_collider;

    public string ID => m_id;
    public string InteractionPromt => m_prompt;

    private void Awake()
    {
        m_collider = GetComponent<BoxCollider>();
    }
    public bool Interact(PlayerInteract interactor)
    {
        //pseudocode for when we have inventory
        /*if (GameManager.Inventory.HasKey(m_key.ID))
         *{
         *  OpenDoor();
         *  return true;
         *}
         *else
         *{
         *  Debug.Log("Key is missing");
         *  return false;
         *}*/
        return false;
    }

    public void OnFocus()
    {
        return;
    }

    public void OnLoseFocus()
    {
        return;
    }

    public void OpenDoor()
    {
        m_collider.enabled = false;
        Debug.Log("Door: The door " + ID + " has been opened.");
    }
}
