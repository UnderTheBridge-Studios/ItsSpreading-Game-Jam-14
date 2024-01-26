using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class I03_Key : MonoBehaviour, IInteractable
{
    [SerializeField] private string m_id = "";
    [SerializeField] private string m_prompt = "Grab Key";

    public string ID => m_id;
    public string InteractionPromt => m_prompt;

    public bool Interact(PlayerInteract interactor)
    {
        GameManager.instance.AddKey(m_id);
        SoundManager.instance.PlayClip(8, 1);
        Destroy(gameObject);
        return true;
    }
}
