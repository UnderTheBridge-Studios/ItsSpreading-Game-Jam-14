using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class I09_Flashlight : MonoBehaviour, IInteractable
{
    [SerializeField] private string m_id = "";
    [SerializeField] private string m_prompt = "Grab Flashlight";

    [Header("Interaction prompt")]
    [SerializeField] private Sprite m_sprite;
    [SerializeField] private string m_text;
    [SerializeField] float m_time;

    public string ID => m_id;
    public string InteractionPromt => m_prompt;

    public bool Interact(PlayerInteract interactor)
    {
        interactor.GetComponent<FlashLight>().GetFlashlight();
        HUBManager.instance.UseActionPromp(m_sprite, m_text, m_time);
        Destroy(gameObject);
        return true;
    }
}
