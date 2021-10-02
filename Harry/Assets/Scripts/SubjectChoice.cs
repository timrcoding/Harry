using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SubjectChoice : MonoBehaviour
{
    public Subject m_Subject;
    private Button m_Button;
    [SerializeField] private TextMeshProUGUI m_Text;
    void Start()
    {
        m_Button = GetComponent<Button>();
        m_Button.onClick.AddListener(CallSubject);
        m_Text.text = StringEnum.GetStringValue(m_Subject);
    }

    void CallSubject()
    {
        GameManager.instance.StartDialogue(m_Subject);
        GetComponent<ButtonTransitions>().FadeOut();
    }
}
