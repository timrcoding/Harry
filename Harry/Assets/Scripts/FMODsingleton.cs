using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FMODsingleton : MonoBehaviour
{
    public static FMODsingleton instance;
    void Start()
    {
        instance = this;
        if(instance != this)
        {
            Destroy(instance.gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }
}
