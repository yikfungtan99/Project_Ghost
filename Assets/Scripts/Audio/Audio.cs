using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class Audio
{
    //! Name allocation for the audioclip being stored
    public string name;

    //! Storage for an audioclip
    public AudioClip audioClip;

    //! Modules that adjust the audio clip variables
    [Range(0f, 1f)]
    public float volume;

    [Range(.1f, 3f)]
    public float pitch;

    public bool loop;

    [HideInInspector]
    public AudioSource audioSource;
}
