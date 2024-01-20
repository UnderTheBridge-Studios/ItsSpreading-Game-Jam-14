using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class I06_Inhibitor : MonoBehaviour, IInteractable
{
    [SerializeField] private string m_id = "";
    [SerializeField] private string m_prompt = "Grab Inhibitor";

    public string ID => m_id;
    public string InteractionPromt => m_prompt;

    public bool Interact(PlayerInteract interactor)
    {
        GameManager.instance.AddInhibitor();
        Debug.Log("Inhibitor Grabbed");
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
