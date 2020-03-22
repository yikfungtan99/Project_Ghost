using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;

public class VolumeSlider : MonoBehaviour
{
    public AudioMixer audioMixer;

    public void SetLevel(float sliderValue)
    {
        audioMixer.SetFloat("ExposedMaster", Mathf.Log10(sliderValue) * 20);
    }
}
