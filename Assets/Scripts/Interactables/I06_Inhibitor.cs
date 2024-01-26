using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class I06_Inhibitor : MonoBehaviour, IInteractable
{
    [SerializeField] private string m_id = "";
    [SerializeField] private string m_prompt = "Grab Inhibitor";
    [SerializeField] private float m_soundVolume = 0.5f;

    public string ID => m_id;
    public string InteractionPromt => m_prompt;

    public bool Interact(PlayerInteract interactor)
    {
        GameManager.instance.AddInhibitor();
        SoundManager.instance.PlayClip(8, m_soundVolume);
        Destroy(gameObject);
        return true;
    }
}
