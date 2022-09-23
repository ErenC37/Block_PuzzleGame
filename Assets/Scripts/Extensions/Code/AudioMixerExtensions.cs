using UnityEngine;
using UnityEngine.Audio;

public static class AudioMixerExtensions
{
    /* ------------------------------------------ */
    
    /// <summary>
    /// An extension method for making setting float with ease and smooth
    /// </summary>
    /// <param name="mixer"></param>
    /// <param name="exposedName">The name of 'The Exposed to Script' variable</param>
    /// <param name="value">value must be between 0 and 1</param>
    public static void SetVolume(this AudioMixer mixer, string exposedName, float value)
    {
        mixer.SetFloat(exposedName, Mathf.Lerp(-80.0f, 0.0f, Mathf.Clamp01(value)));
    }

    /// <summary>
    /// An extension method for making setting float with ease and smooth
    /// </summary>
    /// <param name="exposedName">The name of 'The Exposed to Script' variable</param>
    /// <param name="value">value must be between 0 and 1</param>
    public static void SetVolume(this AudioMixer mixer, string exposedName, float minValue, float maxValue, float value)
    {
        mixer.SetFloat(exposedName, Mathf.Lerp(minValue, maxValue, Mathf.Clamp01(value)));
    }
    
    /* ------------------------------------------ */
}