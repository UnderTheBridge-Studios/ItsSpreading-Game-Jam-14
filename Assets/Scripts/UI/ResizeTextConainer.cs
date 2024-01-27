using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResizeTextConainer : MonoBehaviour
{
    [SerializeField] private int m_preferredWidth;
    [SerializeField] private LayoutElement m_layoutElement;
    [SerializeField] private TextMeshProUGUI m_text;

    public void Initialize(string text)
    {
        m_layoutElement.preferredHeight = -1;
        if (m_text.preferredWidth > m_preferredWidth)
        {
            m_layoutElement.preferredWidth = m_preferredWidth;
        }
        else
            m_layoutElement.preferredWidth = -1;

        m_text.text = text;
        StartCoroutine(ShowText());
    }

    private IEnumerator ShowText()
    {
        m_text.maxVisibleCharacters = 0;
        while (m_text.maxVisibleCharacters < m_text.text.Length)
        {
            m_text.maxVisibleCharacters++;
            yield return new WaitForSeconds(0.02f);
        }
    }
}
