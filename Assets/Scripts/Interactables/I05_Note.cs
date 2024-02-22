using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class I05_Note : MonoBehaviour, IInteractable
{
    [SerializeField] private string m_id = "";
    [SerializeField] private string m_prompt = "Read Note";
    [TextArea(10,7)]
    [SerializeField] private string m_content = "";
    [SerializeField] private float m_soundVolume = 0.5f;

    private InputManager m_inputManager;
    public string InteractionPromt => m_prompt;
    public string ID => m_id;

    public bool Interact(PlayerInteract interactor)
    {
        GameManager.instance.CanvasManager.ShowNote(m_content);
        SoundManager.instance.PlayClip(5, m_soundVolume);

        return true;
    }
}
