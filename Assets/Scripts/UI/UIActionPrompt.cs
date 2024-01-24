using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIActionPrompt : MonoBehaviour
{
    [SerializeField] private Image m_icon;
    [SerializeField] private TextMeshProUGUI m_content;

    private Coroutine m_coroutine;

    public void UseActionPrompt(Sprite sprite, string text, float time)
    {
        if (m_coroutine != null)
            return;

        m_icon.sprite = sprite;
        m_content.text = text;
        gameObject.SetActive(true);

        m_coroutine = StartCoroutine(ShowPrompt(time));
    }

    private IEnumerator ShowPrompt(float time)
    {
        yield return new WaitForSeconds(time);
        gameObject.SetActive(false);
        m_coroutine = null;
    }
}
