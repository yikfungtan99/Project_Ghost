using UnityEngine.Audio;
using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{
    public Audio[] audioList;

    public static AudioManager instance;

    void Awake()
    {
        //! singleton design pattern to avoid duplicate audio managers
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        //! Retain audio manager upon loading new scene
        DontDestroyOnLoad(gameObject);

        //! Assign audio variables to a new AudioSource component for each registered audioclip
        foreach (Audio a in audioList)
        {
            a.audioSource = gameObject.AddComponent<AudioSource>();

            a.audioSource.clip = a.audioClip;
            a.audioSource.volume = a.volume;
            a.audioSource.pitch = a.pitch;
            a.audioSource.loop = a.loop;
        }
    }

    //! Only use this when an audioclip is to be played at the start of game (i.e. BGM theme)
    void Start()
    {
        PlayAudio("Test Theme");
    }

    public void PlayAudio(string searchName)
    {
        //! Searching for equivalent audio name to identify and play
        Audio a = Array.Find(audioList, audio => audio.name == searchName);

        //! In case audio name cannot be identified
        if(a == null)
        {
            Debug.LogWarning("Audio: " + searchName + " is not found!");
            return;
        }

        a.audioSource.Play();
    }
}
