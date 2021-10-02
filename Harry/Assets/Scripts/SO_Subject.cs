using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Subject", menuName = "ScriptableObjects/Text/Subject")]

public class SO_Subject : ScriptableObject
{

    public List<Character> m_Character;
    public List<MultipleChoiceQuestionData> m_MultChoiceData;
    public List<Subject> m_SubjectsMadeAvailable;
    public bool HasQuestions;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

public enum Character
{
    Invalid,
    Therapist,
    Computer
}

[System.Serializable]
public struct MultipleChoiceQuestionData
{
    public string m_Question;
    public List<string> m_Options;
    public int CorrectAnswer;
}
