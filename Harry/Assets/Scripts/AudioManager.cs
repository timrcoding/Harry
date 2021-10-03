using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public AudioSource m_AudioSource;
    public AudioClip mouseClick;
    public AudioClip compType;
    public AudioClip personType;
    public AudioClip Bell;
    public AudioClip Wrong;
    void Start()
    {
        m_AudioSource = GetComponent<AudioSource>();
    }

}
