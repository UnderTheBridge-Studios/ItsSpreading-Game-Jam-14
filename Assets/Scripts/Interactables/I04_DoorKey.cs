using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class I04_DoorKey : I08_Door
{
    [SerializeField] private I03_Key m_key;

    public override bool Interact(PlayerInteract interactor)
    {
        if (GameManager.instance.HasKey(m_key.ID))
        {
            OpenDoor();
            return true;
        }
        else
        {
            Debug.Log("Key is missing");
            return false;
        }
    }
}
