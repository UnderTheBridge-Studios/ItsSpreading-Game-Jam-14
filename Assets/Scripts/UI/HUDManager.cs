using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{
    [Header("Pointer")]
    [SerializeField] private GameObject m_pointerPrompt;

    [Header("Health")]
    [SerializeField] private GameObject m_healthBar;
    [SerializeField] private RectTransform m_poisonBar;
    [SerializeField] private RectTransform m_poisonBarRate;
    private float m_healthLearpSpeed;
    private float m_poisonMaxWidth;

    [Header("Inhibitors")]
    [SerializeField] private GameObject m_inhibitors; //parent object
    [SerializeField] private GameObject m_inhibitorImage;
    [SerializeField] private GameObject m_noInhibitorImage;
    [SerializeField] private TextMeshProUGUI m_inhibitorNumbers;
    private bool m_isInhibitorsFound; //if the player have picked up inhibitors for the first time

    [Header("Interact Prompt")]
    [SerializeField] private GameObject m_interactPrompt;

    [Header("Action Prompt")]
    [SerializeField] private GameObject m_actionPrompt;

    [Header("Battery")]
    [SerializeField] private GameObject m_rechargePrompt;
    [SerializeField] private Image m_rechargingBar;
    [SerializeField] private Sprite m_rechargeKey;
    float m_batteryTimeElapsed;

    [Header("Material References")]
    [SerializeField] private Material m_oclusionMaterial;  //dying effect

    [HideInInspector]
    public bool isPauseMenuOpening; //or closing

    private void Awake()
    {
        m_poisonMaxWidth = m_healthBar.GetComponent<RectTransform>().rect.width - 10;
    }

    private void Update()
    {
        //HealthBar
        m_healthLearpSpeed = 5f * Time.deltaTime;
        m_poisonBar.sizeDelta = new Vector2(Mathf.Clamp(Mathf.Lerp(m_poisonBar.rect.width, GameManager.instance.Poison / 100 * m_poisonMaxWidth, m_healthLearpSpeed), 0f, m_poisonMaxWidth), m_poisonBar.rect.height);
        //in case the poison its full poisonRate hides
        if (GameManager.instance.Poison == 100f)
            m_poisonBarRate.sizeDelta = new Vector2(Mathf.Lerp(m_poisonBarRate.rect.width, 0, m_healthLearpSpeed), m_poisonBarRate.rect.height);
        else
        {
            //prevents the bar from coming out on the bar container
            if (m_poisonBarRate.rect.width < m_poisonBar.rect.width)
                m_poisonBarRate.sizeDelta = new Vector2(Mathf.Clamp(Mathf.Lerp(m_poisonBarRate.rect.width, GameManager.instance.PoisonRate * 1000, m_healthLearpSpeed), 0f, m_poisonMaxWidth), m_poisonBarRate.rect.height);
            else
                m_poisonBarRate.sizeDelta = new Vector2(m_poisonBar.rect.width, m_poisonBarRate.rect.height);
        }

        //Dying effect
        if (GameManager.instance.IsDead)
            m_oclusionMaterial.SetFloat("_VignetteRadius", Mathf.Lerp(m_oclusionMaterial.GetFloat("_VignetteRadius"), -1.2f, Time.deltaTime * 0.5f));
        else
            m_oclusionMaterial.SetFloat("_VignetteRadius", Mathf.Lerp(m_oclusionMaterial.GetFloat("_VignetteRadius"), 1 - (GameManager.instance.Poison / 100), m_healthLearpSpeed));


        //Battery charging bar
        if (GameManager.instance.IsCharging)
        {
            if (m_batteryTimeElapsed < GameManager.instance.ChargeBatteryDuration)
            {
                m_rechargingBar.fillAmount = Mathf.Lerp(0.0f, 1.0f, m_batteryTimeElapsed / GameManager.instance.ChargeBatteryDuration);
                m_batteryTimeElapsed += Time.deltaTime;
            }
        }
    }

    public void ResetHUD()
    {
        //pointer
        m_pointerPrompt.SetActive(true);
        
        //Health bar
        m_healthBar.SetActive(false);
        m_poisonBar.sizeDelta = new Vector2(0f, m_poisonBar.rect.height); //reset poison bar
        m_poisonBarRate.sizeDelta = new Vector2(0f, m_poisonBarRate.rect.height); //reset poison rate bar

        //Inhibitors
        m_isInhibitorsFound = false;
        HideInhibitors();

        //Interaction Prompt
        m_interactPrompt.SetActive(false);

        //Action prompt
        m_actionPrompt.SetActive(false);

        //Battery
        m_rechargePrompt.SetActive(false);
        m_rechargingBar.gameObject.SetActive(false);

        //dying effect
        if (!GameManager.instance.IsDead)
            m_oclusionMaterial.SetFloat("_VignetteRadius", 1);
    }

    public void PointerActive(bool value)
    {
        m_pointerPrompt.SetActive(value);
    }

    #region HealthBar
    public void ShowHealthBar()
    {
        if (!m_healthBar.activeSelf)
        {
            m_healthBar.GetComponent<Image>().fillAmount = 0;
            m_healthBar.GetComponent<Image>().DOFillAmount(1, 1f)
                .OnComplete(() => m_healthBar.SetActive(true));
        }

        m_healthBar.SetActive(true);
    }

    public void HideHealthBar()
    {
        m_healthBar.SetActive(false);
    }

    #endregion

    #region Inhibitors
    public void UpdateInhibitors()
    {
        //The inhibitors are hide before the payer found the first one
        if (!m_isInhibitorsFound)
        {
            m_isInhibitorsFound = true;
            ShowInhibitors();
        }

        if (GameManager.instance.Inhibitors <= 0)
        {
            m_inhibitorImage.SetActive(false);
            m_noInhibitorImage.SetActive(true);
        }
        else
        {
            m_inhibitorImage.SetActive(true);
            m_noInhibitorImage.SetActive(false);
        }

        m_inhibitorNumbers.text = GameManager.instance.Inhibitors.ToString();
    }

    public void ShowInhibitors()
    {
        float xPosition = m_inhibitors.transform.position.x;
        m_inhibitors.transform.position = new Vector3(-200, m_inhibitors.transform.position.y, 0);

        m_inhibitors.SetActive(true);
        m_inhibitors.transform.DOMoveX(xPosition, 0.3f).SetEase(Ease.OutBack);

        ShowHealthBar();
    }

    public void HideInhibitors()
    {
        m_inhibitors.SetActive(false);
    }

    #endregion

    public void InteractPromptActive(bool value, string prompt = "")
    {
        m_interactPrompt.SetActive(value);

        if (value)
            m_interactPrompt.transform.GetChild(1).GetComponent<ResizeTextConainer>().Initialize(prompt);
    }

    public void UseActionPrompt(Sprite sprite, string text, float time)
    {
        m_actionPrompt.GetComponent<UIActionPrompt>().UseActionPrompt(sprite, text, time);
    }

    #region Battery
    public void RechargePromptActive(bool value)
    {
        //m_rechargePrompt.SetActive(value);
        if (value)
            m_actionPrompt.GetComponent<UIActionPrompt>().UseActionPrompt(m_rechargeKey, "Need to Recharge", 10f);
    }

    public void RechargingPromptActive(bool value)
    {
        m_rechargingBar.gameObject.SetActive(value);
        m_rechargingBar.fillAmount = 0;
        m_batteryTimeElapsed = 0;
    }

    #endregion

}
