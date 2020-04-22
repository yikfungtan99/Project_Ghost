using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsMenu : MonoBehaviour
{
    void OnEnable()
	{
		UpdateAudio(1);
	}
	
	void OnDisable()
	{
		UpdateAudio(1);
	}
	
	void UpdateAudio(int index)
	{
		switch(index)
		{
			case 1:
				AudioManager.instance.ForcePlayAudio("button click");
				break;
		}
	}
}
