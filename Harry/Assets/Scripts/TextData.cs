using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class TextData : MonoBehaviour
{
    public static TextData instance;

    [SerializeField] private TextAsset m_TextAsset;
    [SerializeField] private List<string> m_TextParsed;
    public List<SubjectAndDialogue> SubjectsAndDialogues;
    public Dictionary<Subject, SubjectAndDialogue> SubjectAndDialogueDictionary;

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        SubjectsAndDialogues.Clear();
        m_TextParsed = new List<string>(m_TextAsset.text.Split('\n'));
        int i = new int();
        foreach(var s in m_TextParsed)
        { 
            SubjectsAndDialogues.Add(new SubjectAndDialogue((Subject)i, s));
            i++;
        }
        SubjectAndDialogueDictionary = new Dictionary<Subject, SubjectAndDialogue>();
        foreach(var subAndDial in SubjectsAndDialogues)
        {
            SubjectAndDialogueDictionary.Add(subAndDial.m_Subject, subAndDial);
        }
    }
}

[System.Serializable]
public class SubjectAndDialogue
{
    public SubjectAndDialogue(Subject Subject,string Total)
    {
        m_Subject = Subject;
        m_TotalDialogue = Total;
        SetList();
    }

    void SetList()
    {
        m_SubjectDialog = new List<string>(m_TotalDialogue.Split(';'));
        m_SubjectDialog.RemoveAt(0);
        for(int i = m_SubjectDialog.Count-1; i > 0; --i)
        {
            m_SubjectDialog[i].TrimEnd();
            if(m_SubjectDialog[i] == "//")
            {
                m_SubjectDialog.RemoveAt(i);
            }
        }
    }

    public SubjectAndDialogue()
    {}

    public Subject m_Subject;
    public string m_TotalDialogue;
    public List<string> m_SubjectDialog;
}
