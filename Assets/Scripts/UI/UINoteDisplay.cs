using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UINoteDisplay : MonoBehaviour
{
    [SerializeField] private Button m_BackgroundButton;
    [SerializeField] private RectTransform m_note;
    [SerializeField] private TextMeshProUGUI m_noteText;
    [SerializeField] private GameObject m_closeInfo;
    [SerializeField] private GameObject m_closeIcon;

    private bool isOpening;
    private bool isClosing;

    private void Start()
    {
        m_BackgroundButton.Select();
        m_BackgroundButton.onClick.AddListener(HideNote);
    }

    public void ShowNote(string noteContent)
    {
        gameObject.SetActive(true);

        m_closeInfo.SetActive(false);
        m_closeIcon.SetActive(false);
        m_noteText.text = noteContent;

        m_note.sizeDelta = new Vector2(0, 0);
        m_noteText.alpha = 0;
        isOpening = true;

        DOTween.Sequence()
            .Append(m_note.DOSizeDelta(new Vector2(50, 50), 0.2f).SetEase(Ease.InOutQuad))
            .Append(m_note.DOSizeDelta(new Vector2(900, 50), 0.5f).SetEase(Ease.InOutQuad))
            .Append(m_note.DOSizeDelta(new Vector2(900, 700), 0.5f).SetEase(Ease.InOutQuad))
            .Append(m_noteText.DOFade(1, 0.1f))
            .AppendInterval(0.3f)
            .Append(m_noteText.DOFade(0, 0.03f))
            .Append(m_noteText.DOFade(1, 0.05f))
            .AppendInterval(0.1f)
            .Append(m_noteText.DOFade(0, 0.03f))
            .Append(m_noteText.DOFade(1, 0.05f))
            .OnComplete(() =>
            {
                m_closeInfo.SetActive(true);
                m_closeIcon.SetActive(true);
                isOpening = false;
            })
            .SetUpdate(true);
    }

    public void HideNote()
    {
        if (isOpening || isClosing)
            return;

        isClosing = true;
        m_closeInfo.SetActive(false);
        m_closeIcon.SetActive(false);

        m_note.sizeDelta = new Vector2(900, 700);
        m_noteText.alpha = 0;

        Sequence mySequence = DOTween.Sequence()
            .Append(m_note.DOSizeDelta(new Vector2(900, 50), 0.2f).SetEase(Ease.InOutQuad))
            .Append(m_note.DOSizeDelta(new Vector2(50, 50), 0.2f).SetEase(Ease.InOutQuad))
            .Append(m_note.DOSizeDelta(new Vector2(0, 0), 0.05f).SetEase(Ease.InOutQuad))
            .SetUpdate(true)
            .OnComplete(() => { 
                gameObject.SetActive(false);
                isClosing = false;
                GameManager.instance.Player.GetComponent<InputManager>().RecoverControl();
            });
    }
}
