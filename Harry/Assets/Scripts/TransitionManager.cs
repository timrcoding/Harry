using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TransitionManager : MonoBehaviour
{
    public static TransitionManager instance;
    public CanvasGroup TotalCanvasGroup;
    public CanvasGroup ObjCanvasGroup;
    public Button LoadButton;
    public Slider LoadSlider;

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        LoadButton.onClick.AddListener(FillSlider);
        FadeInObjects();
    }

    void FadeInObjects()
    {
        TotalCanvasGroup.alpha = 1;
        LeanTween.value(gameObject, 0, 1, 4).setOnUpdate((value) =>
        {
            ObjCanvasGroup.alpha = value;
        });
    }

    void FillSlider()
    {
        LoadButton.interactable = false;
        LeanTween.value(gameObject, 0, 1, 4).setOnUpdate((value) =>
        {
            LoadSlider.value = value;
        }).setOnComplete(FadeCanvas);
    }

    void FadeCanvas()
    {
        LeanTween.value(gameObject, 1, 0, 3).setOnUpdate((value) =>
        {
            TotalCanvasGroup.alpha = value;
            if(value <= 0)
            {
                TurnOffCanvas();
            }
        }).setOnComplete(GameManager.instance.DisplayAvailableSubjects);
    }

    void TurnOffCanvas()
    {
        TotalCanvasGroup.gameObject.SetActive(false);
    }
}
