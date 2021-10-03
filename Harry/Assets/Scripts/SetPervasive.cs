using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SetPervasive : MonoBehaviour
{
    public Subject m_Subject;

    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(SetSubjectAndLoad);
    }

    public void SetSubjectAndLoad()
    {
        SubjectManager.instance.SetSubject(m_Subject);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
}
