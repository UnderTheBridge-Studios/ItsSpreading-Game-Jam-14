using PixelCrushers.SceneStreamer;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

public class UIPauseMenu : MonoBehaviour
{
    [SerializeField] private Button m_ResumeButton;
    //[SerializeField] private Button m_SettingsButton;
    [SerializeField] private Button m_Restart;
    [SerializeField] private Button m_Exit;

    [SerializeField] private TextMeshProUGUI m_title;
    [SerializeField] private GameObject m_credits;
    private TextMeshProUGUI[] m_childs;

    private void Awake()
    {
        m_ResumeButton.onClick.AddListener(Resume);
        //m_SettingsButton.onClick.AddListener(OpenSettings);
        m_Exit.onClick.AddListener(Exit);
        m_Restart.onClick.AddListener(Restart);
    }

    public void OpenPauseMenu()
    { 
        StartCoroutine(ShowMenu());
    }

    public void Resume()
    {
        StartCoroutine(HideMenu());
    }
    
    private void Restart()
    {
        GameManager.instance.ResumeGame();
        GameManager.instance.RestartGame(true);
    }

    private void Exit()
    {
#if UNITY_STANDALONE
        Application.Quit();
#endif
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

    #region Show Menu

    private IEnumerator ShowMenu()
    {
        //HUBManager.instance.isPauseMenuOpening = true;

        //Hide everything
        m_title.alpha = 0;

        m_ResumeButton.image.fillAmount = 0;
        //m_SettingsButton.image.fillAmount = 0;
        m_Restart.image.fillAmount = 0;
        m_Exit.image.fillAmount = 0;

        TextMeshProUGUI startText = m_ResumeButton.GetComponentInChildren<TextMeshProUGUI>();
        //TextMeshProUGUI settingsText = m_SettingsButton.GetComponentInChildren<TextMeshProUGUI>();
        TextMeshProUGUI restartText = m_Restart.GetComponentInChildren<TextMeshProUGUI>();
        TextMeshProUGUI exitText = m_Exit.GetComponentInChildren<TextMeshProUGUI>();

        startText.maxVisibleCharacters = 0;
        //settingsText.maxVisibleCharacters = 0;
        restartText.maxVisibleCharacters = 0;
        exitText.maxVisibleCharacters = 0;

        m_childs = new TextMeshProUGUI[5];
        for (int i = 0; i < 5; i++)
        {
            m_childs[i] = m_credits.transform.GetChild(i).gameObject.GetComponent<TextMeshProUGUI>();
            m_childs[i].alpha = 0;
        }

        //Animations
        //StartCoroutine(ShowsCredits());
        Sequence sequence = DOTween.Sequence();
        sequence.Append(m_childs[0].DOFade(1, 0.5f).SetEase(Ease.InOutCirc))
            .AppendInterval(0.1f)
            .Join(m_childs[1].DOFade(1, 0.5f).SetEase(Ease.InOutCirc))
            .AppendInterval(0.1f)
            .Join(m_childs[2].DOFade(1, 0.5f).SetEase(Ease.InOutCirc))
            .AppendInterval(0.1f)
            .Join(m_childs[3].DOFade(1, 0.5f).SetEase(Ease.InOutCirc))
            .AppendInterval(0.1f)
            .Join(m_childs[4].DOFade(1, 0.5f).SetEase(Ease.InOutCirc))
            .SetUpdate(true);

        Debug.Log("Duration: " + sequence.Duration());


        //credits

        //float a = 0.1f;
        //for (int i = 0; i < 5; i++)
        //{
        //    sequence.Insert(a, m_childs[i].DOFade(1, 0.5f).SetEase(Ease.InOutCirc))
        //        //.AppendInterval(0.1f)
        //        .SetUpdate(true);

        //    a += 0.1f;
        //}

        m_title.DOFade(1f, 0.4f).SetUpdate(true);

        StartCoroutine(ShowButton(m_ResumeButton.image, startText));
        yield return new WaitForSecondsRealtime(0.1f);
        //StartCoroutine(ShowButton(m_SettingsButton.image, settingsText));
        //yield return new WaitForSecondsRealtime(0.1f);
        StartCoroutine(ShowButton(m_Restart.image, restartText));
        yield return new WaitForSecondsRealtime(0.1f);
        StartCoroutine(ShowButton(m_Exit.image, exitText));

        yield return new WaitForSecondsRealtime(0.3f);
        //HUBManager.instance.isPauseMenuOpening = false;
    }

    private IEnumerator ShowButton(Image imageButton, TextMeshProUGUI textButton)
    {
        imageButton.DOFillAmount(1, 0.5f).SetEase(Ease.InOutCirc).SetUpdate(true);
        yield return new WaitForSecondsRealtime(0.3f);
        textButton.maxVisibleCharacters = 0;
        while (textButton.maxVisibleCharacters < textButton.text.Length)
        {
            textButton.maxVisibleCharacters++;
            yield return new WaitForSecondsRealtime(0.01f);
        }
    }

    private IEnumerator ShowsCredits()
    {
        for (int i = 0; i < 5; i++)
        {
            m_childs[i].DOFade(1,0.5f).SetEase(Ease.InOutCirc).SetUpdate(true);
            yield return new WaitForSecondsRealtime(0.1f);
        }
    }

    #endregion

    #region Hide Menu

    private IEnumerator HideMenu()
    {
        HUBManager.instance.isPauseMenuOpening = true;
        //prepare everything
        m_title.alpha = 1;

        m_ResumeButton.image.fillAmount = 1;
        //m_SettingsButton.image.fillAmount = 1;
        m_Restart.image.fillAmount = 1;
        m_Exit.image.fillAmount = 1;

        TextMeshProUGUI startText = m_ResumeButton.GetComponentInChildren<TextMeshProUGUI>();
        //TextMeshProUGUI settingsText = m_SettingsButton.GetComponentInChildren<TextMeshProUGUI>();
        TextMeshProUGUI restartText = m_Restart.GetComponentInChildren<TextMeshProUGUI>();
        TextMeshProUGUI exitText = m_Exit.GetComponentInChildren<TextMeshProUGUI>();

        startText.maxVisibleCharacters = startText.text.Length;
        //settingsText.maxVisibleCharacters = settingsText.text.Length;
        restartText.maxVisibleCharacters = restartText.text.Length;
        exitText.maxVisibleCharacters = exitText.text.Length;


        m_childs = new TextMeshProUGUI[5];
        for (int i = 0; i < 5; i++)
        {
            m_childs[i] = m_credits.transform.GetChild(i).gameObject.GetComponent<TextMeshProUGUI>();
            m_childs[i].alpha = 1;
        }

        //Animations
        StartCoroutine(HideCredits());

        m_title.DOFade(0f, 0.2f).SetUpdate(true);

        StartCoroutine(HideButton(m_ResumeButton.image, startText));
        yield return new WaitForSecondsRealtime(0.05f);
        //StartCoroutine(HideButton(m_SettingsButton.image, settingsText));
        //yield return new WaitForSecondsRealtime(0.05f);
        StartCoroutine(HideButton(m_Restart.image, restartText));
        yield return new WaitForSecondsRealtime(0.05f);
        StartCoroutine(HideButton(m_Exit.image, exitText));

        yield return new WaitForSecondsRealtime(0.3f);
        HUBManager.instance.isPauseMenuOpening = false;
        GameManager.instance.InputManager.RecoverControl();
    }

    private IEnumerator HideButton(Image imageButton, TextMeshProUGUI textButton)
    {
        imageButton.DOFillAmount(0, 0.25f).SetEase(Ease.InOutCirc).SetUpdate(true);
        yield return new WaitForSecondsRealtime(0.15f);
        textButton.maxVisibleCharacters = textButton.text.Length;
        while (textButton.maxVisibleCharacters > 0)
        {
            textButton.maxVisibleCharacters -= 3;
            yield return new WaitForSecondsRealtime(0.01f);
        }
    }

    private IEnumerator HideCredits()
    {
        for (int i = 0; i < 5; i++)
        {
            m_childs[i].DOFade(0, 0.5f).SetEase(Ease.InOutCirc).SetUpdate(true);
            yield return new WaitForSecondsRealtime(0.05f);
        }
    }

#endregion


}
