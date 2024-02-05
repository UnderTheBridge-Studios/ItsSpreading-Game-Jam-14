using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UIMainMenu : MonoBehaviour
{
    //[SerializeField] private Button m_StartButton;
    //[SerializeField] private Button m_SettingsButton;
    //[SerializeField] private Button m_Exit;

    [SerializeField] private TextMeshProUGUI m_title;
    [SerializeField] private GameObject m_credits;
    private TextMeshProUGUI[] m_childs;

    [ContextMenu("OpenMenu")]
    public void OpenMenu()
    {
        StartCoroutine(ShowMenu());
    }

    private IEnumerator ShowMenu()
    {
        GameManager.instance.InputManager.LockMovement(true);
        HUBManager.instance.isPauseMenuOpening = true;

        m_title.maxVisibleCharacters = 0;

        m_title.alpha = 1;

        m_childs = new TextMeshProUGUI[5];
        for (int i = 0; i < 5; i++)
        {
            m_childs[i] = m_credits.transform.GetChild(i).gameObject.GetComponent<TextMeshProUGUI>();
            m_childs[i].gameObject.SetActive(false);
            m_childs[i].alpha = 1;
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
            m_childs[i].maxVisibleCharacters = 0;
            m_childs[i].gameObject.SetActive(true);
            yield return new WaitForSecondsRealtime(0.4f);
            while (m_childs[i].maxVisibleCharacters < m_childs[i].text.Length)
            {
                m_childs[i].GetComponent<TextMeshProUGUI>().maxVisibleCharacters++;
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
        //m_childs[0].transform.parent.GetComponent<VerticalLayoutGroup>().enabled = false; 
        //for (int i = 0; i < 5; i++)
        //{
        //    m_childs[i].transform.DOLocalMoveX(300f , 0.3f);
        //    m_childs[i].DOFade(0f, 0.2f);

        //    yield return new WaitForSecondsRealtime(0.03f);
        //}

        for (int i = 0; i < 5; i++)
        {
            m_childs[i].DOFade(0f, 0.2f);

            yield return new WaitForSecondsRealtime(0.03f);
        }

        m_title.DOFade(0f, 0.4f)
            .OnComplete(() => {
                GameManager.instance.InputManager.LockMovement(false);
                HUBManager.instance.isPauseMenuOpening = false;
            });
        

    }
}
