using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class Audio
{
    //! Name allocation for the audioclip being stored
    public string name;

    //! Description of what the audioclip is being used for
    public string description;

    //! Storage for an audioclip
    public AudioClip audioClip;

    //! Modules that adjust the audio clip variables
    [Range(0f, 1f)]
    public float volume;

    [Range(.1f, 3f)]
    public float pitch;

    public bool loop;

    public float defFadeInSeconds;
    public float defFadeOutSeconds;

    [HideInInspector]
    public bool isPlaying;
    [HideInInspector]
    public bool isFading;
    [HideInInspector]
    public bool isPaused;
    [HideInInspector]
    public bool triggerOnce;

    [HideInInspector]
    public AudioSource audioSource;
}
