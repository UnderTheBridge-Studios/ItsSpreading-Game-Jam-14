using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class I02_DoorButton : I08_Door
{
    public override bool Interact(PlayerInteract interactor)
    {
        Debug.Log("Button must be pressed");
        return false;
    }
}
