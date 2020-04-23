using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine;

public class VolumeSlider : MonoBehaviour
{
    public AudioMixer audioMixer;
	
	void Awake()
	{
		if(audioMixer != null && PlayerPrefs.HasKey("MasterSliderVolume"))
		{
			float savedVolume;
			
			if(PlayerPrefs.GetFloat("MasterSliderVolume", 1f) != 0)
			{
				savedVolume = Mathf.Log10(PlayerPrefs.GetFloat("MasterSliderVolume", 1f)) * 20;
			}
			else
			{
				savedVolume = -80;
			}
			
			GetComponent<Slider>().value = PlayerPrefs.GetFloat("MasterSliderVolume", 1f);
			audioMixer.SetFloat("ExposedMaster", savedVolume);
		}
	}
	
    public void SetLevel(float sliderValue)
    {
		float newVolume;
		
		if(sliderValue != 0)
		{
			newVolume = Mathf.Log10(sliderValue) * 20;
		}
		else
		{
			newVolume = -80;
		}
		
        audioMixer.SetFloat("ExposedMaster", newVolume);
		
		PlayerPrefs.SetFloat("MasterSliderVolume", sliderValue);
    }
}
