using System.Collections;
using UnityEngine.Audio;
using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{
    [Header("Debug")]
    [Tooltip("Adjust audio -while playing- through the inspector editor")]
    public bool adjustIn_GameAudioThruInspector = false;

    public AudioMixerGroup audioMixer;

    [Header("Audio")]
    public Audio[] audioList;

    [Header("Editor Panel")]
    public bool inverseSpaceTimeContinuum;
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
        //DontDestroyOnLoad(gameObject);

        //! Assign audio variables to a new AudioSource component for each registered audioclip
        foreach (Audio a in audioList)
        {
            a.audioSource = transform.GetChild(0).gameObject.AddComponent<AudioSource>();

            a.audioSource.outputAudioMixerGroup = audioMixer;
            a.audioSource.clip = a.audioClip;
            a.audioSource.volume = a.volume;
            a.audioSource.pitch = a.pitch;
            a.audioSource.loop = a.loop;

            //! default settings
            a.isPlaying = false;
            a.isFading = false;
            a.isPaused = false;
            a.triggerOnce = false;
            ForceStopAudio(a.name);
        }
    }

    //! Only use this when an audioclip is to be played at the start of game (i.e. BGM theme)
    void Start()
    {
        //! Fade in this ambience when player starts in bridal bedroom (code may change later)
        FadeInAudio("disquiet ambience", 0);

    }

    private void Update()
    {
        //! Update audio variables
        foreach (Audio a in audioList)
        {
            //! Check if any audioclip has naturally finished playing
            if(!a.audioSource.isPlaying && !a.isPaused && !a.triggerOnce)
            {
                Debug.Log("Audio: " + a.name + " has finished playing its clip!");
                a.isPlaying = false;
                a.triggerOnce = true;
            }

            //! barrier to update all other variables
            if (adjustIn_GameAudioThruInspector)
            {
                if(a.isFading)
                {
                    continue;
                }

                a.audioSource.clip = a.audioClip;
                a.audioSource.volume = a.volume;
                a.audioSource.pitch = a.pitch;
                a.audioSource.loop = a.loop;
            }
        }
    }

    public void PlayAudio(string searchName)
    {
        //! Searching for equivalent audio name to identify and play
        Audio a = Array.Find(audioList, audio => audio.name == searchName);

        //! In case audio name cannot be identified
        if(a == null)
        {
            Debug.LogWarning("Audio: " + searchName + " cannot be found to Play!");
            return;
        }

        if(a.isPlaying)
        {
            Debug.LogWarning("Audio: " + searchName + " is still being played! Stop Spamming!");
            return;
        }
        if(a.isPaused)
        {
            Debug.LogWarning("Audio: " + searchName + " is paused! Please unpause first!");
            return;
        }

        Debug.Log("Now Playing Audio: " + searchName + "!");
        a.triggerOnce = false;
        a.isPlaying = true;
        a.audioSource.volume = 1f;
        a.audioSource.Play();
    }

    public void StopAudio(string searchName)
    {
        //! Searching for equivalent audio name to identify and play
        Audio a = Array.Find(audioList, audio => audio.name == searchName);

        //! In case audio name cannot be identified
        if (a == null)
        {
            Debug.LogWarning("Audio: " + searchName + " cannot be found to Stop!");
            return;
        }

        if(!a.isPlaying)
        {
            Debug.LogWarning("Audio: " + searchName + " is not playing! Nice try.");
            return;
        }

        if(!a.isPaused)
        {
            Debug.Log("Stopped Playing Audio: " + searchName + "!");
        }
        else
        {
            Debug.Log("Stopped Paused Audio: " + searchName + "!");
            a.isPaused = false;
        }
        a.isPlaying = false;
        a.audioSource.Stop();
    }

    public void ForcePlayAudio(string searchName)
    {
        //! Searching for equivalent audio name to identify and play
        Audio a = Array.Find(audioList, audio => audio.name == searchName);

        //! In case audio name cannot be identified
        if (a == null)
        {
            Debug.LogWarning("Audio: " + searchName + " cannot be found to Play!");
            return;
        }

        Debug.Log("Now Playing Audio: " + searchName + "!");
        a.triggerOnce = false;
        a.isPlaying = true;
        a.audioSource.volume = 1f;
        a.audioSource.Play();
    }

    public void ForceStopAudio(string searchName)
    {
        //! Searching for equivalent audio name to identify and play
        Audio a = Array.Find(audioList, audio => audio.name == searchName);

        //! In case audio name cannot be identified
        if (a == null)
        {
            Debug.LogWarning("Audio: " + searchName + " cannot be found to Stop!");
            return;
        }

        a.isPaused = false;
        a.isPlaying = false;
        a.audioSource.UnPause();
        a.audioSource.Stop();
    }

    public void FadeOutAudio(string searchName, float customFadeAmount)
    {
        //! Searching for equivalent audio name to identify
        Audio a = Array.Find(audioList, audio => audio.name == searchName);

        if (a == null)
        {
            if(searchName == null)
            {
                Debug.LogWarning("Audio: TYPE:NULL cannot be found to Fade Out!");
            }
            else
            {
                Debug.LogWarning("Audio: " + searchName + " cannot be found to Fade Out!");
            }
            return;
        }

        if (!a.isPlaying)
        {
            Debug.LogWarning("Audio: " + searchName + " is not playing! Nice try.");
            return;
        }
        if (a.isFading)
        {
            Debug.LogWarning("Audio: " + searchName + " is still fading! Stop Spamming!");
            return;
        }
        if(a.isPaused)
        {
            Debug.LogWarning("Audio: " + searchName + " is paused! Please unpause first!");
            return;
        }

        //! if fadeAmount is set to null, use the default fade in amount from the audio clip
        if (customFadeAmount == -1 || customFadeAmount == 0)
        {
            customFadeAmount = a.defFadeOutSeconds;
        }

        StartCoroutine(FadeOutSound(a, customFadeAmount));
    }

    public void FadeInAudio(string searchName, float customFadeAmount)
    {
        //! Searching for equivalent audio name to identify
        Audio a = Array.Find(audioList, audio => audio.name == searchName);

        if (a == null)
        {
            if (searchName == null)
            {
                Debug.LogWarning("Audio: TYPE:NULL cannot be found to Fade In!");
            }
            else
            {
                Debug.LogWarning("Audio: " + searchName + " cannot be found to Fade In!");
            }
            return;
        }

        if (a.isPlaying)
        {
            Debug.LogWarning("Audio: " + searchName + " is still being played! Stop Spamming!");
            return;
        }
        if (a.isFading)
        {
            Debug.LogWarning("Audio: " + searchName + " is still fading! Stop Spamming!");
            return;
        }
        if (a.isPaused)
        {
            Debug.LogWarning("Audio: " + searchName + " is paused! Please unpause first!");
            return;
        }

        //! if fadeAmount is set to null, use the default fade in amount from the audio clip
        if (customFadeAmount == -1 || customFadeAmount == 0)
        {
            customFadeAmount = a.defFadeInSeconds;
        }

        StartCoroutine(FadeInSound(a, customFadeAmount));
    }

    IEnumerator FadeOutSound(Audio a, float fadeAmount)
    {
        Debug.Log("Now Fading Out Audio: " + a.name + " within " + fadeAmount + " seconds!");
        a.isFading = true;

        //! decrease volume by fadeAmount until it is 0
        while (a.audioSource.volume > 0.01f)
        {
            if(!a.isPlaying)
            {
                break;
            }

            a.audioSource.volume -= Time.deltaTime / fadeAmount;
            yield return null;
        }

        Debug.Log("Stopped Playing Audio: " + a.name + "!");
        a.isPlaying = false;
        a.isFading = false;
        a.audioSource.volume = 0;
        a.audioSource.Stop();
    }

    IEnumerator FadeInSound(Audio a, float fadeAmount)
    {
        Debug.Log("Now Fading In Audio: " + a.name + " within " + fadeAmount + " seconds!");
        a.isFading = true;

        Debug.Log("Audio: Starting up " + a.name + " for fade in!");
        PlayAudio(a.name);
        a.audioSource.volume = 0;
        
        //! increase volume by fadeAmount until it is 0
        while (a.audioSource.volume < 0.99f)
        {
            if (!a.isPlaying)
            {
                break;
            }

            a.audioSource.volume += Time.deltaTime / fadeAmount;
            yield return null;
        }

        Debug.Log("Audio: Fade in for " + a.name + " is complete!");
        
        a.isFading = false;
        a.audioSource.volume = 1f;
    }

    public void Pause(string searchName)
    {
        Audio a = Array.Find(audioList, audio => audio.name == searchName);

        if (a == null)
        {
            Debug.LogWarning("Audio: " + searchName + " cannot be found to Pause!");
            return;
        }

        if(a.isFading)
        {
            Debug.LogWarning("Audio: " + searchName + " is still in a fading transition! Try again!");
            return;
        }
        if(!a.isPlaying)
        {
            Debug.LogWarning("Audio: You cannot trick the great AudioManager! " + searchName + " is not playing!");
            return;
        }
        if(a.isPaused)
        {
            Debug.LogWarning("Audio: " + searchName + " is already Paused!");
            return;
        }

        Debug.Log("Audio: " + searchName + " is now Paused!");
        a.isPaused = true;
        a.audioSource.Pause();
    }

    public void Unpause(string searchName)
    {
        Audio a = Array.Find(audioList, audio => audio.name == searchName);

        if (a == null)
        {
            Debug.LogWarning("Audio: " + searchName + " cannot be found to Pause!");
            return;
        }

        if (a.isFading)
        {
            Debug.LogWarning("Audio: " + searchName + " is still in a fading transition! Try again!");
            return;
        }
        if (!a.isPlaying)
        {
            Debug.LogWarning("Audio: You cannot trick the great AudioManager!" + searchName + " is not playing!");
            return;
        }
        if (!a.isPaused)
        {
            Debug.LogWarning("Audio: " + searchName + " is already Unpaused!");
            return;
        }

        Debug.Log("Audio: " + searchName + " is now Unpaused!");
        a.isPaused = false;
        a.audioSource.UnPause();
    }
}
