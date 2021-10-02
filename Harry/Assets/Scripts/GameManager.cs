using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameObject DialoguePanel;
    public List<Subject> NewlyAvailableSubjects;
    public GameObject SubjectPrefab;
    public Transform SubjectParent;
    [SerializeField] private GameObject ContinueButton;

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
        musicInstance.start();
        CloseDialoguePanel();
    }

    public void DisplayAvailableSubjects()
    {
        foreach(var subject in NewlyAvailableSubjects)
        {
            GameObject sub = Instantiate(SubjectPrefab);
            sub.transform.SetParent(SubjectParent);
            sub.transform.localScale = Vector3.one;
            sub.GetComponent<SubjectChoice>().m_Subject = subject;
        }
    }

    public void CloseDialoguePanel()
    {
        DisplayAvailableSubjects();
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
    }
}
