using System.Collections;
using System.Collections.Generic;
using System.Runtime.Versioning;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static AudioClip popSound, sparkle;
    static AudioSource AudioSrc;

    void Start()
    {
        popSound = ResourcesScope.Load<AudioClip>("pop");
        sparkle= ResourcesScope.Load<AudioClip>("sparkle");
    }

    
    void Update()
    {
        
    }
}
