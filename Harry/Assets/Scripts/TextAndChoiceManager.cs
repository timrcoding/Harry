using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TextAndChoiceManager : MonoBehaviour
{
    public static TextAndChoiceManager instance;

    public Subject SelectedSubject;
    [SerializeField] private Button m_CheckButton;
    [SerializeField] private Button m_ContinueButton;
    [SerializeField] private Transform MultChoiceParent;
    [SerializeField] private GameObject MultChoicePrefab;
    private bool CanPrint = true;
    [SerializeField] private List<SubjectToScriptableObject> SubjectToScriptableObjects;
    private Dictionary<Subject, SO_Subject> SubjectToScriptableObjectDictionary;

    //Screen Objects
    [SerializeField] private TextMeshProUGUI DialogText;
    [SerializeField] private Subject m_SubjectOverride;
    [SerializeField] private Scrollbar m_Scrollbar;

    //Sound
    [FMODUnity.EventRef]
    public string KeySound;

    [FMODUnity.EventRef]
    public string CompSound;

    [SerializeField] private int PrintCount;
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        m_CheckButton.gameObject.SetActive(false);
        SubjectToScriptableObjectDictionary = new Dictionary<Subject, SO_Subject>();
        foreach(var s in SubjectToScriptableObjects)
        {
            SubjectToScriptableObjectDictionary.Add(s.m_Subject, s.m_Scriptable);
        }
        m_CheckButton.onClick.AddListener(SubmitAnswers);
        m_ContinueButton.onClick.AddListener(Continue);
        m_ContinueButton.onClick.AddListener(ScrollToBottom);
        DialogText.text = "";
        MultChoiceParent.gameObject.SetActive(false);
    }

    public void StartSubject()
    {
        DialogText.text = "";
        StartCoroutine(PrintText());
        MultChoiceParent.gameObject.SetActive(false);
        m_CheckButton.gameObject.SetActive(false);
    }

    public void Continue()
    {
        StartCoroutine(PrintText());
    }

    public void SetSelectedSubject(Subject subject)
    {
        SelectedSubject = subject;
    }

    IEnumerator PrintText()
    {
        m_ContinueButton.gameObject.SetActive(false);
        if (CanPrint)
        {
            CanPrint = false;
            var Scriptable = SubjectToScriptableObjectDictionary[SelectedSubject];
            int prevCharacters = DialogText.text.Length;
            List<string> TextList = TextData.instance.SubjectAndDialogueDictionary[SelectedSubject].m_SubjectDialog;
            if (!TextList[PrintCount].StartsWith("/"))
            {
                DialogText.text += "\n\n";
                if (PrintCount % 2 == 0)
                {
                    DialogText.text += TextList[PrintCount];
                }
                else
                {
                    DialogText.text += $"{TextList[PrintCount]}";
                }
                

                //Show new characters
                for (int i = prevCharacters; i <= DialogText.text.Length; ++i)
                {
                    DialogText.maxVisibleCharacters = i;
                    if (PrintCount % 2 == 0)
                    {
                        FMODUnity.RuntimeManager.PlayOneShot(KeySound);
                    }
                    else
                    {
                        FMODUnity.RuntimeManager.PlayOneShot(CompSound);
                    }
                    yield return new WaitForSeconds(Random.Range(0.02f,0.05f));
                    
                    if (i > 0)
                    {
                        if (DialogText.text[i - 1] == '.' || DialogText.text[i - 1] == ',')
                        {
                            yield return new WaitForSeconds(Random.Range(0.7f, 1.0f));
                        }
                    }
                }
                PrintCount++;
                CanPrint = true;
                m_ContinueButton.gameObject.SetActive(true);
            }
            else
            {
                PrintCount = 0;
                if (Scriptable.m_MultChoiceData.Count > 0)
                {
                    CreateMultipleChoice(SelectedSubject);
                }
                else
                {
                    CloseAndAdd();
                }
                CanPrint = true;
            }
            
        }
    }

    void ScrollToBottom()
    {
        LeanTween.value(gameObject, m_Scrollbar.value, 0, 5).setOnUpdate((value) =>
        {
            m_Scrollbar.value = value;
        });
    }

    void CreateMultipleChoice(Subject subject)
    {
        MultChoiceParent.gameObject.SetActive(true);
        m_CheckButton.gameObject.SetActive(true);
        DestroyChoices();
        //Select Correct Scriptable Object
        var Scriptable = SubjectToScriptableObjectDictionary[subject];
        for(int i = 0; i < Scriptable.m_MultChoiceData.Count; i++)
        {
            GameObject newChoice = Instantiate(MultChoicePrefab);
            newChoice.transform.SetParent(MultChoiceParent);
            newChoice.transform.localScale = Vector3.one;
            newChoice.GetComponent<MultipleChoiceQuestion>().SetData(Scriptable.m_MultChoiceData[i]);
        }
        
    }

    void DestroyChoices()
    {
        foreach (Transform child in MultChoiceParent)
        {
            Destroy(child.gameObject);
        }
    }

    void SubmitAnswers()
    {
        if (CheckAllAnswers())
        {
            CloseAndAdd();
        }
        else
        {
            Debug.Log("Incorrect");
        }
    }

    void CloseAndAdd()
    {
        var Scriptable = SubjectToScriptableObjectDictionary[SelectedSubject];
        GameManager.instance.NewlyAvailableSubjects.Clear();
        foreach (var available in Scriptable.m_SubjectsMadeAvailable)
        {
            if (!GameManager.instance.NewlyAvailableSubjects.Contains(SelectedSubject))
            {
                GameManager.instance.NewlyAvailableSubjects.Add(available);
            }
        }
        GameManager.instance.CloseDialoguePanel();
    }

    bool CheckAllAnswers()
    {
        for(int i = 0; i < MultChoiceParent.childCount; i++)
        {
            if (MultChoiceParent.GetChild(i).GetComponent<MultipleChoiceQuestion>().GetAnswerCorrect)
            {
            }
            else
            {
                return false;
            }
            
        }
        return true;
    }

}

public enum Subject
{
    [StringValue("Introduction")]
    Introduction,
    [StringValue("Childhood")]
    Childhood,
    [StringValue("Mother")]
    Mother,
    [StringValue("Religion")]
    Synagogue,
    [StringValue("The War")]
    War
}
[System.Serializable]
public struct SubjectToScriptableObject
{
    public Subject m_Subject;
    public SO_Subject m_Scriptable;
}
