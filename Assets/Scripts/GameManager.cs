using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }

    [SerializeField] private float m_maxPoisonRate;
    [Range(0, 0.1f)]
    [SerializeField] private float m_poisonRate;
    [Range(0,100)]
    [SerializeField] private float m_poison;
    [SerializeField] private float m_batteryTimeDuration;
    [Tooltip("When the flashlight start flickering")]
    [SerializeField] private float m_batteryTimeFlicker;
    [SerializeField] private float m_chargeBatteryDuration;
    private float m_battery;
    private float m_batteryRate;
    private Coroutine m_batteryCharging;

    private bool m_isPaused;
    private bool m_isFlickering;
    private bool m_isFlashlightActive;
    private bool m_isCharging;

    public float poison => m_poison;
    public float poisonRate => m_poisonRate;
    public float batery => m_battery;
    public bool isFlickering => m_isFlickering;
    public bool isCharging => m_isCharging;


    private void Awake()
    {
        if (instance != null && instance != this)
            Destroy(this);
        else
            instance = this;
    }

    private void Start()
    {
        ResetValues();
    }

    public void ResetValues()
    {
        m_isPaused = false;
        m_isFlickering = false;
        m_isCharging = false;
        m_battery = m_batteryTimeDuration;
        m_batteryRate = 5f / m_batteryTimeDuration;
        m_poison = 0;
        m_poisonRate = 0;
    }

    private void Update()
    {
        //*time.deltaTime
        m_poison = Mathf.Clamp(m_poisonRate + m_poison, 0f, 100f);

        if (m_isFlashlightActive)
        {
            m_battery -= Time.deltaTime;
            if (m_battery < m_batteryTimeFlicker)
                m_isFlickering = true;
        }
    }

    public void SetPoison(float newPoison)
    {
        m_poison = Mathf.Clamp(newPoison, 0f, 100f); 
    }

    public void SetPoisonRate(float newPoisonRate)
    {
        m_poisonRate = Mathf.Clamp(newPoisonRate, 0f, 100f);
    }

    public void SetFlashlightActive(bool value)
    {
        m_isFlashlightActive = value;
    }

 #region Battery
    public void ChargeBattery()
    {
        m_isCharging = true;
        HUBManager.instance.RechargingPromptActive(true);
        m_batteryCharging = StartCoroutine(ChargingBattery());
    }

    public void StopChargingBattery()
    {
        if (m_batteryCharging == null)
            return;

        StopCoroutine(m_batteryCharging);
        HUBManager.instance.RechargingPromptActive(false);
        m_isCharging = false;
    }

    private IEnumerator ChargingBattery()
    {
        yield return new WaitForSeconds(m_chargeBatteryDuration);
        m_isFlickering = false;
        m_isCharging = false;
        m_battery = m_batteryTimeDuration;
        HUBManager.instance.RechargePromptActive(false);
        HUBManager.instance.RechargingPromptActive(false);
    }
#endregion

    public void PauseGame()
    {
        if (!m_isPaused)
        {
            Time.timeScale = 0;
            m_isPaused = true;
            HUBManager.instance.pause();
        }   
        else
        {
            Time.timeScale = 1;
            m_isPaused = false;
            HUBManager.instance.resume();
        }
    }
}
