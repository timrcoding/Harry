using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Continue : MonoBehaviour
{
    private Button m_Button;
    void OnEnable()
    {
        m_Button = GetComponent<Button>();
        m_Button.onClick.AddListener(FadeOut);
        GetComponent<CanvasGroup>().alpha = 0;
        FadeIn();
    }

    void FadeIn()
    {
        m_Button.interactable = true;
        LeanTween.value(gameObject, 0, 1, 2).setOnUpdate((value) =>
           {
               GetComponent<CanvasGroup>().alpha = value;
           });
    }

    void FadeOut()
    {
        m_Button.interactable = false;
        LeanTween.value(gameObject, 1, 0, 2).setOnUpdate((value) =>
        {
            GetComponent<CanvasGroup>().alpha = value;
        });
    }

    private void OnDisable()
    {
        GetComponent<CanvasGroup>().alpha = 0;
    }
}
