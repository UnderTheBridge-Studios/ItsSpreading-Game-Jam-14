using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class I04_DoorKey : I08_Door
{
    [SerializeField] private string m_keyID;

    [Header("Failed interaction prompt")]
    [SerializeField] private Sprite m_sprite;
    [SerializeField] private string m_text;
    [SerializeField] float m_time;

    private void Start()
    {
        LockDoor(true);
    }

    public override bool Interact(PlayerInteract interactor)
    {
        if (GameManager.instance.HasKey(m_keyID))
        {
            LockDoor(false);
            OpenDoor();
            return true;
        }
        else
        {
            GameManager.instance.CanvasManager.UseActionPrompt(m_sprite, m_text, m_time);
            return false;
        }
    }
}
