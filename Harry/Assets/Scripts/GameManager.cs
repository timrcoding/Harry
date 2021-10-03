using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameObject DialoguePanel;
    public List<Subject> NewlyAvailableSubjects;
    public GameObject SubjectPrefab;
    public Transform SubjectParent;
    [SerializeField] private GameObject ContinueButton;
    public Subject m_OverrideSubject;

    public FMOD.Studio.EventInstance musicInstance;
    [FMODUnity.EventRef]
    public string Music;

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        ContinueButton.gameObject.SetActive(false);
        musicInstance = FMODUnity.RuntimeManager.CreateInstance(Music);
        NewlyAvailableSubjects.Add(SubjectManager.instance.m_Subject);
        DialoguePanel.SetActive(false);
    }

    public void DisplayAvailableSubjects()
    {
        if (NewlyAvailableSubjects.Contains(Subject.Ending))
        {
            LoadEnding();
            
        }
        else if (NewlyAvailableSubjects.Contains(Subject.Finish))
        {
            TransitionManager.instance.ObjCanvasGroup.gameObject.SetActive(false);
            TransitionManager.instance.TotalCanvasGroup.gameObject.SetActive(true);
            LeanTween.value(gameObject, 0, 1, 6).setOnUpdate((value) =>
            {
                TransitionManager.instance.TotalCanvasGroup.alpha = value;
            });
        }
        else
        {
            foreach (var subject in NewlyAvailableSubjects)
            {
                GameObject sub = Instantiate(SubjectPrefab);
                sub.transform.SetParent(SubjectParent);
                sub.transform.localScale = Vector3.one;
                sub.GetComponent<SubjectChoice>().m_Subject = subject;
            }
        }
    }

    public void LoadEnding()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void CloseDialoguePanel()
    {
        DisplayAvailableSubjects();
        LeanTween.value(gameObject, 1, 0, 2).setOnUpdate((value) =>
        {
            DialoguePanel.GetComponent<CanvasGroup>().alpha = value;
        }).setOnComplete(TurnOffDialoguePanel);
    }

    void TurnOffDialoguePanel()
    {
        DialoguePanel.SetActive(false);
    }

    public void StartDialogue(Subject subject)
    {
        FadeInDialoguePanel();
        TextAndChoiceManager.instance.SetSelectedSubject(subject);
        TextAndChoiceManager.instance.StartSubject();
    }

    void FadeInDialoguePanel()
    {
        DialoguePanel.SetActive(true);
        LeanTween.value(gameObject, 0, 1, 1).setOnUpdate((value) =>
        {
            DialoguePanel.GetComponent<CanvasGroup>().alpha = value;
        });
    }
}
