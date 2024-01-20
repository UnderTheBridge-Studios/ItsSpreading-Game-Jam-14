using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class I04_DoorKey : I08_Door
{
    [SerializeField] private string m_keyID;

    public override bool Interact(PlayerInteract interactor)
    {
        if (GameManager.instance.HasKey(m_keyID))
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
