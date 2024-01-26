using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class I08_Door : MonoBehaviour, IInteractable
{
    [SerializeField] private bool m_canReOpen;
    [SerializeField] private string m_id = "";
    [SerializeField] private string m_prompt = "Open Door";
    [SerializeField] private SimpleDoorAnimation m_animation;
    [SerializeField] private MaterialPropertyBlock m_materialBlock;
    [SerializeField] private MeshRenderer m_meshRenderer;

    private bool m_isLocked = false;
    private BoxCollider m_collider;

    public bool IsLocked => m_isLocked;
    public bool CanReOpen => m_canReOpen;
    public string ID => m_id;
    public string InteractionPromt => m_prompt;

    private void Awake()
    {
        m_collider = GetComponent<BoxCollider>();
        m_materialBlock = new MaterialPropertyBlock();
        
        LockDoor(m_isLocked);
    }

    public virtual bool Interact(PlayerInteract interactor)
    {
        OpenDoor();
        return true;
    }

    public virtual void OpenDoor()
    {
        m_collider.enabled = false;
        m_animation.OpenDoorAnimation();
    }

    public virtual void CloseDoor()
    {
        m_collider.enabled = true;
        m_animation.CloseDoorAnimation();
    }

    public void LockDoor(bool value)
    {
        m_isLocked = value;

        if (value)
            ChangePropertyBlock(1);
        else
            ChangePropertyBlock(0);
    }

    private void ChangePropertyBlock(float amount)
    {
        m_materialBlock.SetFloat("_Blocked", amount);
        m_meshRenderer.SetPropertyBlock(m_materialBlock);
    }
}
