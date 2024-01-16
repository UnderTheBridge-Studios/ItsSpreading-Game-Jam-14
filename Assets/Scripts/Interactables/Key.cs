using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour, IInteractable
{
    [SerializeField] private string id;
    [SerializeField] private string prompt;

    public string ID => id;
    public string InteractionPromt => prompt;

    public bool Interact(PlayerInteract interactor)
    {
        Debug.Log("Grabbing Key");
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
