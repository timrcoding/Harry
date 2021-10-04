using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TextIntroducer : MonoBehaviour
{
    public bool CanSet;
    [FMODUnity.EventRef]
    public string chime;
    public CanvasGroup m_CanvasGroup;
    public Button m_Button;
  
    public FadeTransitions FingerFade;
    void Start()
    {
        m_CanvasGroup = GetComponent<CanvasGroup>();
        m_Button.onClick.AddListener(FadeCanvas);
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }
        CanSet = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && CanSet)
        {
            SetNewText();
            FMODUnity.RuntimeManager.PlayOneShot(chime);
            if (FingerFade.gameObject.activeInHierarchy)
            {
                FingerFade.FadeOut();
            }
        }
    }

    IEnumerator SetupText()
    {
        yield return new WaitForSeconds(Time.deltaTime);
        SetNewText();
        FMODUnity.RuntimeManager.PlayOneShot(chime);
        if (FingerFade.gameObject.activeInHierarchy)
        {
            FingerFade.FadeOut();
        }
        
    }

    void SetNewText()
    {
        foreach(Transform child in transform)
        {
            if (!child.gameObject.activeInHierarchy)
            {
                child.gameObject.SetActive(true);
                return;
            }
        }
        FadeCanvas();
    }

    void FadeCanvas()
    {
        LeanTween.value(gameObject, 1, 0, 4).setOnUpdate((value) =>
           {
               m_CanvasGroup.alpha = value;
           }).setOnComplete(LoadNextScene);
    }

    void LoadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
