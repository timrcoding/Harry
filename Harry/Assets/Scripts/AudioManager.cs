using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public FMOD.Studio.EventInstance musicInstance;
    [FMODUnity.EventRef]
    public string evt;
    public bool AudioStarted;
    bool audioResumed = false;

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        
    }

    IEnumerator StartAudio()
    {
        yield return new WaitForSeconds(1);
      //  musicInstance = FMODUnity.RuntimeManager.CreateInstance(evt);
      //  musicInstance.start();
    }



    public void ResumeAudio()
    {
        if (!audioResumed)
        {
            var result = FMODUnity.RuntimeManager.CoreSystem.mixerSuspend();
            Debug.Log(result);
            result = FMODUnity.RuntimeManager.CoreSystem.mixerResume();
            Debug.Log(result);
            audioResumed = true;
        }
        StartCoroutine(StartAudio());
    }
}
