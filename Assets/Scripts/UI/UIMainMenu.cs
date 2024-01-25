using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UIMainMenu : MonoBehaviour
{
    [SerializeField] private Button m_StartButton;
    [SerializeField] private Button m_SettingsButton;
    [SerializeField] private Button m_Exit;

    [SerializeField] private TextMeshProUGUI m_title;

    private void Start()
    {
        m_StartButton.onClick.AddListener(Resume);
        m_SettingsButton.onClick.AddListener(OpenSettings);
        m_Exit.onClick.AddListener(Exit);

        StartCoroutine(ShowMenu());
    }

    private IEnumerator ShowMenu()
    {
        m_title.maxVisibleCharacters = 0;
        m_StartButton.image.fillAmount = 0;
        m_SettingsButton.image.fillAmount = 0;
        m_Exit.image.fillAmount = 0;

        TextMeshProUGUI startText = m_StartButton.GetComponentInChildren<TextMeshProUGUI>();
        TextMeshProUGUI settingsText = m_SettingsButton.GetComponentInChildren<TextMeshProUGUI>();
        TextMeshProUGUI exitText = m_Exit.GetComponentInChildren<TextMeshProUGUI>();

        startText.maxVisibleCharacters = 0;
        settingsText.maxVisibleCharacters = 0;
        exitText.maxVisibleCharacters = 0;

        yield return new WaitForSeconds(1f);


        while (m_title.maxVisibleCharacters < m_title.text.Length)
        {
            m_title.maxVisibleCharacters++;
            yield return new WaitForSeconds(0.15f);
        }
        yield return new WaitForSeconds(0.3f);

        StartCoroutine(ShowButton(m_StartButton.image, startText));
        yield return new WaitForSeconds(0.2f);
        StartCoroutine(ShowButton(m_SettingsButton.image, settingsText));
        yield return new WaitForSeconds(0.2f);
        StartCoroutine(ShowButton(m_Exit.image, exitText));




        //m_StartButton.image.fillAmount = 0;
        //m_StartButton.image.DOFillAmount(1, 0.4f).SetEase(Ease.InOutCirc);
        //yield return new WaitForSeconds(0.1f);

        //m_SettingsButton.image.fillAmount = 0;
        //m_SettingsButton.image.DOFillAmount(1, 0.4f).SetEase(Ease.InOutCirc);
        //yield return new WaitForSeconds(0.1f);

        //m_Exit.image.fillAmount = 0;
        //m_Exit.image.DOFillAmount(1, 0.4f).SetEase(Ease.InOutCirc);
    }

    private IEnumerator ShowButton(Image imageButton, TextMeshProUGUI textButton)
    {
        Debug.Log(textButton.text);

        imageButton.DOFillAmount(1, 0.5f).SetEase(Ease.InOutCirc);
        //.OnComplete(() => while (true) { });
        yield return new WaitForSeconds(0.3f);
        textButton.maxVisibleCharacters = 0;
        while (textButton.maxVisibleCharacters < textButton.text.Length)
        {
            textButton.maxVisibleCharacters++;
            yield return new WaitForSeconds(0.01f);
        }
    }




    private void Resume()
    {
        GameManager.instance.Player.GetComponent<InputManager>().CloseMainMenu();
    }

    private void OpenSettings()
    {

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
}
