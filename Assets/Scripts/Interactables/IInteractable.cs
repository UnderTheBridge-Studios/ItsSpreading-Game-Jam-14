using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
    public string InteractionPromt { get; }
    public string ID { get; }

    public bool Interact(PlayerInteract interactor);

    public void OnFocus();
    public void OnLoseFocus();
}
