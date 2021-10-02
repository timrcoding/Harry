using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class MultipleChoiceQuestion : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] MultipleChoiceQuestionData m_Data;
    [SerializeField] TMP_Dropdown m_Dropdown;
    [SerializeField] TextMeshProUGUI Question;
    private bool CanRunCheck = true;
    [SerializeField] bool CheckedCorrect;
    public bool GetAnswerCorrect { get { return CheckedCorrect; } }
    void Start()
    {
        m_Dropdown.onValueChanged.AddListener(delegate { CheckDropDown(); });
        CheckDropDown();
    }

    public void SetData(MultipleChoiceQuestionData Data)
    {
        m_Data = Data;
        SetDropdown();
    }

    public void SetDropdown()
    {
        //Set up options
        m_Dropdown.ClearOptions();
        m_Dropdown.AddOptions(m_Data.m_Options);
        //Set Question
        Question.text = m_Data.m_Question;
    }

    public void CheckDropDown()
    {
        if (CanRunCheck)
        {
            if (m_Dropdown.value == m_Data.CorrectAnswer)
            {
                CheckedCorrect = true;
            }
            else
            {
                CheckedCorrect = false;
            }
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        CanRunCheck = true;
    }
}
