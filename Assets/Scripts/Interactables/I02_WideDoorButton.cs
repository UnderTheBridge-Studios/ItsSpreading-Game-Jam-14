using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class I02_WideDoorButton : I08_WideDoor
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
