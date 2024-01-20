using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class I03_Key : MonoBehaviour, IInteractable
{
    [SerializeField] private string m_id;
    [SerializeField] private string m_prompt = "Grab Key";

    public string ID => m_id;
    public string InteractionPromt => m_prompt;

    public bool Interact(PlayerInteract interactor)
    {
        //pseudocode for when we have inventory
        //GameManager.Inventory.AddKey(m_key.ID);
        Debug.Log("Key Grabbed");
        Destroy(this.gameObject);
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
