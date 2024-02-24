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
    [SerializeField] private Button m_RestartButton;
    [SerializeField] private Button m_ExitButton;

    [SerializeField] private TextMeshProUGUI m_title;
    [SerializeField] private GameObject m_credits;
    private TextMeshProUGUI[] m_creditsNames;

    private TextMeshProUGUI m_startText;
    private TextMeshProUGUI m_restartText;
    private TextMeshProUGUI m_exitText;


    private void Awake()
    {
        m_ResumeButton.onClick.AddListener(GameManager.instance.CanvasManager.ResumeGame);
        m_ExitButton.onClick.AddListener(Exit);
        m_RestartButton.onClick.AddListener(Restart);

        m_startText = m_ResumeButton.GetComponentInChildren<TextMeshProUGUI>();
        m_restartText = m_RestartButton.GetComponentInChildren<TextMeshProUGUI>();
        m_exitText = m_ExitButton.GetComponentInChildren<TextMeshProUGUI>();
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

    public float ShowMenu()
    {
        //Hide everything
        m_title.alpha = 0;

        //buttons
        m_ResumeButton.image.fillAmount = 0;
        m_RestartButton.image.fillAmount = 0;
        m_ExitButton.image.fillAmount = 0;

        m_startText.alpha = 0;
        m_restartText.alpha = 0;
        m_exitText.alpha = 0;

        //credits
        m_creditsNames = new TextMeshProUGUI[5];
        for (int i = 0; i < 5; i++)
        {
            m_creditsNames[i] = m_credits.transform.GetChild(i).gameObject.GetComponent<TextMeshProUGUI>();
            m_creditsNames[i].alpha = 0;
        }

        //Animations
        Sequence sequence = DOTween.Sequence();

        //Credits
        //Time delay between when each name of the credit appears
        float creditDelay = 0.1f;
        for (int i = 0; i < 5; i++)
            sequence.Insert(i* creditDelay, m_creditsNames[i].DOFade(1, 0.5f).SetEase(Ease.InOutCirc));

        //Title
        sequence.Insert(0, m_title.DOFade(1f, 0.4f).SetUpdate(true));

        //Buttons
        sequence
            .Insert(0, m_ResumeButton.image.DOFillAmount(1, 0.5f).SetEase(Ease.InOutCirc))
            .Insert(0.1f, m_RestartButton.image.DOFillAmount(1, 0.5f).SetEase(Ease.InOutCirc))
            .Insert(0.2f, m_ExitButton.image.DOFillAmount(1, 0.5f).SetEase(Ease.InOutCirc));

        sequence
            .Insert(0.3f, m_startText.DOFade(1, 0.2f))
            .Insert(0.4f, m_restartText.DOFade(1, 0.2f))
            .Insert(0.5f, m_exitText.DOFade(1, 0.2f));

        //input delay to avoid problems
        sequence.AppendInterval(0.1f);
        sequence.SetUpdate(true);
        
        return sequence.Duration();
    }

    public float HideMenu()
    {
        //prepare everything
        m_title.alpha = 1;

        m_ResumeButton.image.fillAmount = 1;
        m_RestartButton.image.fillAmount = 1;
        m_ExitButton.image.fillAmount = 1;

        TextMeshProUGUI startText = m_ResumeButton.GetComponentInChildren<TextMeshProUGUI>();
        TextMeshProUGUI restartText = m_RestartButton.GetComponentInChildren<TextMeshProUGUI>();
        TextMeshProUGUI exitText = m_ExitButton.GetComponentInChildren<TextMeshProUGUI>();

        startText.maxVisibleCharacters = startText.text.Length;
        restartText.maxVisibleCharacters = restartText.text.Length;
        exitText.maxVisibleCharacters = exitText.text.Length;


        m_creditsNames = new TextMeshProUGUI[5];
        for (int i = 0; i < 5; i++)
        {
            m_creditsNames[i] = m_credits.transform.GetChild(i).gameObject.GetComponent<TextMeshProUGUI>();
            m_creditsNames[i].alpha = 1;
        }

        //Animations
        //StartCoroutine(ShowsCredits());
        Sequence sequence = DOTween.Sequence();

        //Credits
        //Time delay between when each name of the credit appears
        float creditDelay = 0.05f;
        for (int i = 0; i < 5; i++)
            sequence.Insert(i * creditDelay, m_creditsNames[i].DOFade(0, 0.25f).SetEase(Ease.InOutCirc));

        //Title
        sequence.Insert(0, m_title.DOFade(0f, 0.2f));

        //Buttons
        sequence
            .Insert(0, m_ResumeButton.image.DOFillAmount(0, 0.25f).SetEase(Ease.InOutCirc))
            .Insert(0.05f, m_RestartButton.image.DOFillAmount(0, 0.25f).SetEase(Ease.InOutCirc))
            .Insert(0.1f, m_ExitButton.image.DOFillAmount(0, 0.25f).SetEase(Ease.InOutCirc));

        //Buttons Text
        sequence
            .Insert(0.15f, startText.DOFade(0, 0.1f))
            .Insert(0.2f, restartText.DOFade(0, 0.1f))
            .Insert(0.25f, exitText.DOFade(0, 0.1f));

        //input delay to avoid problems
        sequence.AppendInterval(0.1f);
        sequence.SetUpdate(true);

        return sequence.Duration();
    }
}
