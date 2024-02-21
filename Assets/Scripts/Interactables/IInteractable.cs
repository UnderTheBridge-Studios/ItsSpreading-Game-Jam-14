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
        GameManager.instance.CanvasManager.InteractPromptActive(true, InteractionPromt);
    }
    public void OnLoseFocus()
    {
        GameManager.instance.CanvasManager.InteractPromptActive(false);
    }
}
