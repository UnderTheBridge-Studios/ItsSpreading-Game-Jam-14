using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
    public string InteractionPromt { get; }
    public string ID { get; }

    public bool Interact(PlayerInteract interactor);

    public void OnFocus()
    {
        HUBManager.instance.InteractPromptActive(true, InteractionPromt);
    }
    public void OnLoseFocus()
    {
        HUBManager.instance.InteractPromptActive(false, "");
    }
}
