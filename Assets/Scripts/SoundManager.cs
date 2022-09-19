using System.Collections;
using System.Collections.Generic;
using System.Runtime.Versioning;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioClip popSound, sparkle;
    static AudioSource AudioSrc;

    void Start()
    {
        // popSound = Resources.Load<AudioClip>("pop");
        // sparkle= Resources.Load<AudioClip>("sparkle");
    }

    
    void Update()
    {
        
    }
}
