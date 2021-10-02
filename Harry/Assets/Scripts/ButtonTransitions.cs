using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonTransitions : MonoBehaviour
{
    private CanvasGroup m_CanvasGroup;
    void OnEnable()
    {
        if(GetComponent<CanvasGroup>() == null)
        {
            m_CanvasGroup = gameObject.AddComponent<CanvasGroup>();
        }
        else
        {
            m_CanvasGroup = GetComponent<CanvasGroup>();
        }
        m_CanvasGroup.alpha = 0;
        FadeIn();
    }

    public void FadeIn()
    {
        LeanTween.value(gameObject, 0, 1, 2).setOnUpdate((value) =>
        {
            GetComponent<CanvasGroup>().alpha = value;
        });
    }

    public void FadeOut()
    {
        LeanTween.value(gameObject, 1, 0, 2).setOnUpdate((value) =>
        {
            GetComponent<CanvasGroup>().alpha = value;
        }).setOnComplete(DeActivate);
    }

    void DeActivate()
    {
        gameObject.SetActive(false);
    }
}
