using UnityEngine.Audio;
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
