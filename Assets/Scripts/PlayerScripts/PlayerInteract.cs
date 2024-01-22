using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    [SerializeField] Transform m_playerInteractPoint;
    [SerializeField] private float m_interactionDistance = 3f;

    private IInteractable m_currentInteractable;

    private void Update()
    {
        InteractionCheck();
    }

    private void InteractionCheck()
    {
        if (Physics.Raycast(m_playerInteractPoint.position, m_playerInteractPoint.transform.forward, out RaycastHit hit, m_interactionDistance))
        {
            if (hit.collider.gameObject.CompareTag("Interactable") && (m_currentInteractable == null || hit.collider.gameObject.GetComponent<IInteractable>().ID != m_currentInteractable.ID))
            {
                hit.collider.TryGetComponent(out m_currentInteractable);
                m_currentInteractable.OnFocus();
                HUBManager.instance.InteractPromptActive(true);
            }
            else if (!hit.collider.gameObject.CompareTag("Interactable") && m_currentInteractable != null)
            {
                m_currentInteractable.OnLoseFocus();
                HUBManager.instance.InteractPromptActive(false);

                m_currentInteractable = null;
            }
        }
        else if (m_currentInteractable != null)
        {
            m_currentInteractable.OnLoseFocus();
            HUBManager.instance.InteractPromptActive(false);
            m_currentInteractable = null;
        }
    }

    public void OnInteractPressed()
    {
        if (m_currentInteractable != null)
        {
            if (m_currentInteractable.Interact(this))
            {
                return;
            }
            else
            {
                return;
            }
        }
    }
}
