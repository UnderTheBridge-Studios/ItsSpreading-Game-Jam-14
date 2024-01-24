using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class I04_WideDoorKey : I08_WideDoor
{
    [SerializeField] private string m_keyID;

    [Header("Failed interaction prompt")]
    [SerializeField] private Sprite m_sprite;
    [SerializeField] private string m_text;
    [SerializeField] float m_time;

    public override bool Interact(PlayerInteract interactor)
    {
        if (GameManager.instance.HasKey(m_keyID))
        {
            OpenDoor();
            return true;
        }
        else
        {
            HUBManager.instance.UseActionPromp(m_sprite, m_text, m_time);
            return false;
        }
    }
}
