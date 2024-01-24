using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class I02_DoorButton : I08_Door
{
    [Header("Failed interaction prompt")]
    [SerializeField] private Sprite m_sprite;
    [SerializeField] private string m_text;
    [SerializeField] float m_time;
    public override bool Interact(PlayerInteract interactor)
    {
        HUBManager.instance.UseActionPromp(m_sprite, m_text, m_time);
        return false;
    }
}
