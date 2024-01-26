using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIActionPrompt : MonoBehaviour
{
    [SerializeField] private Image m_icon;
    [SerializeField] private GameObject m_textContainer;

    private Coroutine m_coroutine;

    public void UseActionPrompt(Sprite sprite, string text, float time)
    {
        if (m_coroutine != null)
            StopCoroutine(m_coroutine);

        m_icon.sprite = sprite;
        gameObject.SetActive(true);
        m_textContainer.GetComponent<ResizeTextConainer>().Initialize(text);
        m_coroutine = StartCoroutine(HidePrompt(time));
    }

    private IEnumerator HidePrompt(float time)
    {
        yield return new WaitForSeconds(time);

        gameObject.SetActive(false);
        m_coroutine = null;
    }
}
