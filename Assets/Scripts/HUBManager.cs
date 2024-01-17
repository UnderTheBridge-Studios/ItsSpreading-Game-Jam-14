using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUBManager : MonoBehaviour
{
    public static HUBManager instance { get; private set; }

    [SerializeField] private GameObject m_interactPrompt;

    private void Awake()
    {
        if (instance != null && instance != this)
            Destroy(this);
        else
            instance = this;
    }

    private void Start()
    {
        m_interactPrompt.SetActive(false);
    }

    public void InteractPromptActive(bool value)
    {
        m_interactPrompt.SetActive(value);
    }

}
