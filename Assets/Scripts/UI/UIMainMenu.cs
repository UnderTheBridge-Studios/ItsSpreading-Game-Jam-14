using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UIMainMenu : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI m_title;
    [SerializeField] private GameObject m_credits;
    private TextMeshProUGUI[] m_creditsNames;

    public void OpenMenu()
    {
        StartCoroutine(ShowMenu());
    }

    private IEnumerator ShowMenu()
    {
        m_title.maxVisibleCharacters = 0;

        m_title.alpha = 1;

        m_creditsNames = new TextMeshProUGUI[5];
        for (int i = 0; i < 5; i++)
        {
            m_creditsNames[i] = m_credits.transform.GetChild(i).gameObject.GetComponent<TextMeshProUGUI>();
            m_creditsNames[i].gameObject.SetActive(false);
            m_creditsNames[i].alpha = 1;
        }

        yield return new WaitForSecondsRealtime(1f);


        while (m_title.maxVisibleCharacters < m_title.text.Length)
        {
            m_title.maxVisibleCharacters++;
            yield return new WaitForSecondsRealtime(0.1f);
        }
        yield return new WaitForSecondsRealtime(0.3f);


        yield return new WaitForSecondsRealtime(0.2f);

        StartCoroutine(ShowsCredits());
    }

    private IEnumerator ShowsCredits()
    {
        for (int i = 0; i < 5; i++)
        {
            m_creditsNames[i].maxVisibleCharacters = 0;
            m_creditsNames[i].gameObject.SetActive(true);
            yield return new WaitForSecondsRealtime(0.4f);
            while (m_creditsNames[i].maxVisibleCharacters < m_creditsNames[i].text.Length)
            {
                m_creditsNames[i].GetComponent<TextMeshProUGUI>().maxVisibleCharacters++;
                yield return new WaitForSecondsRealtime(0.05f);
            }
            yield return new WaitForSecondsRealtime(0.2f);
        }
        yield return new WaitForSecondsRealtime(3f);

        StartCoroutine(HideMenu());
    }

    [ContextMenu("HideMenu")]

    private IEnumerator HideMenu()
    {
        for (int i = 0; i < 5; i++)
        {
            m_creditsNames[i].DOFade(0f, 0.2f);

            yield return new WaitForSecondsRealtime(0.03f);
        }

        m_title.DOFade(0f, 0.4f)
            .OnComplete(() => {
                GameManager.instance.InputManager.SetGameplayInput(false);
                GameManager.instance.CanvasManager.ShowHUD();
            });
    }
}
