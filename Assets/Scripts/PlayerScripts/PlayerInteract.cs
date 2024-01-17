using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    [SerializeField] Transform m_playerInteractPoint;
    [SerializeField] private float m_interactionDistance = 3f;

    private IInteractable currentInteractable;

    private void Update()
    {
        InteractionCheck();
    }

    private void InteractionCheck()
    {
        if (Physics.Raycast(m_playerInteractPoint.position, m_playerInteractPoint.transform.forward, out RaycastHit hit, m_interactionDistance))
        {
            if (hit.collider.gameObject.CompareTag("Interactable") && (currentInteractable == null || hit.collider.gameObject.GetComponent<IInteractable>().ID != currentInteractable.ID))
            {
                hit.collider.TryGetComponent(out currentInteractable);
                currentInteractable.OnFocus();
                HUBManager.instance.InteractPromptActive(true);
                Debug.Log(currentInteractable.InteractionPromt);
            }
            else if (!hit.collider.gameObject.CompareTag("Interactable") && currentInteractable != null)
            {
                currentInteractable.OnLoseFocus();
                HUBManager.instance.InteractPromptActive(false);

                Debug.Log("Lose Focus de " + currentInteractable.ID);
                currentInteractable = null;
            }
        }
        else if (currentInteractable != null)
        {
            Debug.Log("Lose Focus de " + currentInteractable.ID);
            currentInteractable.OnLoseFocus();
            HUBManager.instance.InteractPromptActive(false);
            currentInteractable = null;
        }
    }

    public void OnInteractPressed()
    {
        if (currentInteractable != null)
        {
            if (currentInteractable.Interact(this))
            {
                Debug.Log("Interaction succesfull");
            }
            else
            {
                Debug.Log("Interaction failed");
            }
        }
    }
}
