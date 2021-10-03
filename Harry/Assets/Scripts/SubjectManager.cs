using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubjectManager : MonoBehaviour
{
    public static SubjectManager instance;
    public Subject m_Subject;
    void Start()
    {
        instance = this;
    }

    public void SetSubject(Subject subject)
    {
        m_Subject = subject;
    }

}
