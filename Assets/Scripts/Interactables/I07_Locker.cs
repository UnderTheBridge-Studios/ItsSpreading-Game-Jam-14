using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class I07_Locker : MonoBehaviour, IInteractable
{
    [SerializeField] private string m_id = "";
    [SerializeField] private string m_prompt = "Open Locker";

    [Header("Animation values")]
    [SerializeField] private Transform m_cover;
    [SerializeField] private float m_finalPosition = 80f;
    [SerializeField] private float m_duration = 1f;

    private BoxCollider m_collider;

    public string ID => m_id;
    public string InteractionPromt => m_prompt;

    private void Awake()
    {
        m_collider = GetComponent<BoxCollider>();
    }
    public bool Interact(PlayerInteract interactor)
    {
        m_collider.enabled = false;
        m_cover.DOLocalRotate(new Vector3(m_finalPosition, 0, 0), m_duration);
        return true;
    }
}
