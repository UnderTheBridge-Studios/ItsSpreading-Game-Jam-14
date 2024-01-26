using PixelCrushers.SceneStreamer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }

    [Header("Testing Checks")]
    public bool UseMainMenu;
    public bool InitialFlashlight;
    public bool UseCinematics;

    [Header("Scene Loader")]
    [SerializeField] private string m_firstSceneName;

    [Header("Poison Values")]
    [Range(0, 0.1f)]
    [Tooltip("The poison rate you get when first poisoned")]
    [SerializeField] private float m_firstPoisonRate = 0.01f;
    [Range(0, 0.1f)]
    [Tooltip("The current poison rate")]
    [SerializeField] private float m_poisonRate = 0f;
    [Range(0,100)]
    [Tooltip("The current poison")]
    [SerializeField] private float m_poison = 0f;
    [Tooltip("How much poison the inhibitor heal, from 0 to 100")]
    [SerializeField] private float m_inhibitorHealth;
    [SerializeField] private float m_inhibitorVolume = 0.5f;
    private bool m_isPoisoned;
    private float m_inhibitors;

    [Header("Battery Values")]
    [SerializeField] private float m_batteryTimeDuration;
    [Tooltip("The time left before the flashlight start flickering")]
    [SerializeField] private float m_batteryTimeFlicker;
    [Tooltip("Recharge battery duration")]
    [SerializeField] private float m_chargeBatteryDuration;
    private float m_battery;
    private Coroutine m_batteryCharging;
    private bool m_isFlickering;
    private bool m_isCharging;
    private bool m_isFlashlightActive;
    private bool m_isDead;

    private GameObject m_player;
    private bool m_isPaused;

    private List<string> m_keyIDs = new List<string>();

    public GameObject Player => m_player;

    public bool IsPaused => m_isPaused;

    //Health
    public float Poison => m_poison;
    public float PoisonRate => m_poisonRate;
    public bool IsPoisoned => m_isPoisoned;
    public float Inhibitors => m_inhibitors;
    public bool IsDead => m_isDead;
    
    //Battery
    public float Battery => m_battery;
    public float ChargeBatteryDuration => m_chargeBatteryDuration;
    public bool IsFlickering => m_isFlickering;
    public bool IsCharging => m_isCharging;


    private void Awake()
    {
        if (instance != null && instance != this)
            Destroy(this);
        else
            instance = this;
    }

    private void Start()
    {
        m_player = GameObject.FindGameObjectWithTag("Player");

        StartCoroutine(SceneLoaded(UseMainMenu));
    }

    private void Update()
    {
        if (!m_isDead)
            CheckDeath();

        //*time.deltaTime
        m_poison = Mathf.Clamp(m_poisonRate + m_poison, 0f, 100f);

        if (m_isFlashlightActive)
        {
            m_battery -= Time.deltaTime;
            if (m_battery < m_batteryTimeFlicker)
                m_isFlickering = true;
        }
    }

    #region Restart

    public void ResetValues()
    {
        m_isPaused = false;
        m_isFlickering = false;
        m_isCharging = false;
        m_isPoisoned = false;
        m_isDead = false;
        m_battery = m_batteryTimeDuration;
        m_poison = 0;
        m_poisonRate = 0;
    }

    public void RestartGame(bool showMainMenu)
    {
        SceneStreamer.UnloadAll();
        SceneStreamer.SetCurrentScene(m_firstSceneName);
        StartCoroutine(SceneLoaded(showMainMenu));
    }

    private IEnumerator SceneLoaded(bool showMainMenu)
    {
        if (m_isPaused)
            m_player.GetComponent<InputManager>().ClosePauseMenu();

        ResetValues();
        HUBManager.instance.ResetHUB();
        m_player.GetComponent<FlashLight>().ResetValues();
        m_player.GetComponent<PlayerMovement>().SetWalkingSpeed();

        yield return new WaitUntil(() => SceneStreamer.IsSceneLoaded(m_firstSceneName) == true);

        m_player.GetComponent<PlayerMovement>().ResetPosition();


        if (showMainMenu)
        {
            HUBManager.instance.MainMenuActive();
            m_player.GetComponent<InputManager>().LockMovement(true);
        }
        else
            m_player.GetComponent<InputManager>().LockMovement(false);

    }

    private void CheckDeath()
    {
        if (m_poison < 100)
            return;

        m_isDead = true;
        m_player.GetComponent<PlayerCinematics>().LaunchDeathCinematic();
    }

    #endregion

    public void PauseGame()
    {
        if (!m_isPaused)
        {
            Time.timeScale = 0;
            m_isPaused = true;

            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Time.timeScale = 1;
            m_isPaused = false;

            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    #region Poison
    public void SetPoison(float newPoison)
    {
        m_poison = Mathf.Clamp(m_poison + newPoison, 0f, 100f);
    }

    public void SetPoisonRate()
    {
        m_poisonRate = Mathf.Clamp(m_firstPoisonRate, 0f, 100f);
        m_isPoisoned = true;
    }
    #endregion

    #region Flashlight
    public void SetFlashlightActive(bool value)
    {
        m_isFlashlightActive = value;
    }

    public void ChargeBattery()
    {
        m_isCharging = true;
        m_player.GetComponent<PlayerMovement>().SetSlowSpeed();
        HUBManager.instance.RechargingPromptActive(true);
        SoundManager.instance.PlayReload();
        m_batteryCharging = StartCoroutine(ChargingBattery());
    }

    public void StopChargingBattery()
    {
        if (m_batteryCharging == null)
            return;

        StopCoroutine(m_batteryCharging);
        HUBManager.instance.RechargingPromptActive(false);
        SoundManager.instance.StopReload();
        m_isCharging = false;
        m_player.GetComponent<PlayerMovement>().SetWalkingSpeed();
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

    #region Inventory
    public void AddKey(string keyID)
    {
        m_keyIDs.Add(keyID);
    }

    public bool HasKey(string keyID)
    {
        foreach (string id in m_keyIDs)
        {
            if (id == keyID)
                return true;
        }

        return false;
    }

    public void AddInhibitor()
    {
        m_inhibitors += 1;

        HUBManager.instance.UpdateInhibitors();
    }

    public void UseInhibitor()
    {
        if (m_inhibitors <= 0)
            return;

        m_poison = Mathf.Clamp(m_poison - m_inhibitorHealth, 0f, 100f);
        m_inhibitors -= 1;

        HUBManager.instance.UpdateInhibitors();
        SoundManager.instance.PlayClip(3, m_inhibitorVolume);
    }

    #endregion

}
