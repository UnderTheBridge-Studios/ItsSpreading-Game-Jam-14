using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    [SerializeField] Camera playerCamera;
    [SerializeField] private float interactionDistance = 3f;

    private IInteractable currentInteractable;

    private void Update()
    {
        InteractionCheck();
    }

    private void InteractionCheck()
    {
        //Debug.DrawRay(playerCamera.transform.position, playerCamera.transform.forward, Color.red, interactionDistance);
        if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out RaycastHit hit, interactionDistance))
        {
            //currentInteractable == null || currentInteractable.ID != hit.collider.gameObject.GetComponent<IInteractable>().ID)
            if (hit.collider.gameObject.CompareTag("Interactable") && (currentInteractable == null || hit.collider.gameObject.GetComponent<IInteractable>().ID != currentInteractable.ID))
            {
                hit.collider.TryGetComponent(out currentInteractable);
                currentInteractable.OnFocus();
                Debug.Log(currentInteractable.InteractionPromt);
            }
            else if (!hit.collider.gameObject.CompareTag("Interactable") && currentInteractable != null)
            {
                currentInteractable.OnLoseFocus();
                Debug.Log("Lose Focus de " + currentInteractable.ID);
                currentInteractable = null;
            }
        }
        else if (currentInteractable != null)
        {
            Debug.Log("Lose Focus de " + currentInteractable.ID);
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
