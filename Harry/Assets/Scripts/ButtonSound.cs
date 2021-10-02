using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSound : MonoBehaviour
{
    [FMODUnity.EventRef]
    public string m_ButtonSound;
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(PlaySound);
    }

    void PlaySound()
    {
        if (m_ButtonSound != null)
        {
            FMODUnity.RuntimeManager.PlayOneShot(m_ButtonSound);
        }
    }

}
