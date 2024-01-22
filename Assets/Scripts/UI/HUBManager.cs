using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUBManager : MonoBehaviour
{
    public static HUBManager instance { get; private set; }

    [SerializeField] private GameObject m_interactPrompt;
    [SerializeField] private GameObject m_healthBar;
    [SerializeField] private RectTransform m_poisonBar;
    [SerializeField] private RectTransform m_poisonBarRate;
    [SerializeField] private GameObject m_pauseMenu;
    [SerializeField] private GameObject m_rechargePrompt;
    [SerializeField] private GameObject m_rechargingPrompt;
    [SerializeField] private GameObject m_noteDisplay;

    private TextMeshProUGUI m_noteContent;
    private float m_learpSpeed;
    private float m_poisonMaxWidth;

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
        m_healthBar.SetActive(false);
        m_pauseMenu.SetActive(false);
        m_rechargePrompt.SetActive(false);
        m_rechargingPrompt.SetActive(false);
        m_noteDisplay.SetActive(false);

        m_poisonMaxWidth = m_poisonBar.rect.width;
        m_poisonBar.sizeDelta = new Vector2(0f, m_poisonBar.rect.height);
        m_poisonBarRate.sizeDelta = new Vector2(0f, m_poisonBarRate.rect.height);
        m_noteContent = m_noteDisplay.GetComponentInChildren<TextMeshProUGUI>();
    }

    private void Update()
    {
        //HealthBar
        m_learpSpeed = 5f * Time.deltaTime;
        m_poisonBar.sizeDelta = new Vector2(Mathf.Clamp(Mathf.Lerp(m_poisonBar.rect.width, GameManager.instance.Poison / 100 * m_poisonMaxWidth, m_learpSpeed), 0f, m_poisonMaxWidth), m_poisonBar.rect.height);

        //in case the poison its full poisonRate hides
        if (GameManager.instance.Poison == 100f)
            m_poisonBarRate.sizeDelta = new Vector2(Mathf.Lerp(m_poisonBarRate.rect.width, 0, m_learpSpeed), m_poisonBarRate.rect.height);
        else
        {
            //prevents the bar from coming out on the bar container
            if (m_poisonBarRate.rect.width < m_poisonBar.rect.width)
                m_poisonBarRate.sizeDelta = new Vector2(Mathf.Clamp(Mathf.Lerp(m_poisonBarRate.rect.width, GameManager.instance.PoisonRate * 1000, m_learpSpeed), 0f, m_poisonMaxWidth), m_poisonBarRate.rect.height);
            else
                m_poisonBarRate.sizeDelta = new Vector2(m_poisonBar.rect.width, m_poisonBarRate.rect.height);
        }
    }

    public void InteractPromptActive(bool value)
    {
        m_interactPrompt.SetActive(value);
    }

    public void HealthBarActive(bool value)
    {
        m_healthBar.SetActive(value);
    }

    public void RechargePromptActive(bool value)
    {
        m_rechargePrompt.SetActive(value);
    }

    public void RechargingPromptActive(bool value)
    {
        m_rechargingPrompt.SetActive(value);
    }

    public void ShowPauseMenu()
    {
        m_pauseMenu.SetActive(true);
    }

    public void HidePauseMenu()
    {
        m_pauseMenu.SetActive(false);
    }

    public void ShowNote(string noteContent)
    {
        m_noteDisplay.SetActive(true);
        m_noteContent.text = noteContent;
    }

    public void HideNote()
    {
        m_noteDisplay.SetActive(false);
    }
}
