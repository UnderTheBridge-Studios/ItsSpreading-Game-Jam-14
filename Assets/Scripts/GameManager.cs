using PixelCrushers.SceneStreamer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }

    [SerializeField] private bool m_useMainMenu;
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

    [Header("Battery Values")]
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
    private bool m_isPoisoned;
 
    private List<string> m_keyIDs = new List<string>();
    private float m_inhibitors;

    private GameObject m_player;

    public GameObject Player => m_player;
    public bool IsPaused => m_isPaused;
    public float Poison => m_poison;
    public float PoisonRate => m_poisonRate;
    public float Batery => m_battery;
    public bool IsFlickering => m_isFlickering;
    public bool IsCharging => m_isCharging;
    public bool IsPoisoned => m_isPoisoned;
    public float Inhibitors => m_inhibitors;


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
        m_player = GameObject.FindGameObjectWithTag("Player");

        StartCoroutine(SceneLoadedStart());
    }

    public void ResetValues()
    {
        m_isPaused = false;
        m_isFlickering = false;
        m_isCharging = false;
        m_isPoisoned = false;
        m_battery = m_batteryTimeDuration;
        m_batteryRate = 5f / m_batteryTimeDuration;
        m_poison = 0;
        m_poisonRate = 0;
    }

    private void Update()
    {
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

    private IEnumerator SceneLoadedStart()
    {
        yield return new WaitUntil(() => SceneStreamer.IsSceneLoaded(m_firstSceneName) == true);

        if (m_useMainMenu)
            m_player.GetComponent<InputManager>().OpenMainMenu();
        else
            HUBManager.instance.MainMenuActive(false);
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
    }

    public void UseInhibitor()
    {
        if (m_inhibitors == 0)
            return;

        m_inhibitors -= 1;
    }
    #endregion

    private void CheckDeath()
    {
        if (m_poison < 100)
            return;

        RestartGame(false);
    }
    public void SetFlashlightActive(bool value)
    {
        m_isFlashlightActive = value;
    }

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

    public void RestartGame(bool fromPauseMenu)
    {
        SceneStreamer.SetCurrentScene(m_firstSceneName);

        StartCoroutine(SceneLoaded());
        ResetValues();
        HUBManager.instance.ResetHUB();

        if (fromPauseMenu)
        {
            m_isPaused = true;
            m_player.GetComponent<InputManager>().ClosePauseMenu();
            m_player.GetComponent<InputManager>().OpenMainMenu();
        }
    }

    private IEnumerator SceneLoaded()
    {
        yield return new WaitUntil(()=> SceneStreamer.IsSceneLoaded(m_firstSceneName) == true);

        m_player.transform.position = Vector3.zero;
        m_player.transform.rotation = Quaternion.identity;
        m_player.GetComponentInChildren<Camera>().transform.rotation = Quaternion.identity;
    }
}
