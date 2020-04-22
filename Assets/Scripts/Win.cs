using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Win : MonoBehaviour
{
    void Start()
	{
		AudioManager.instance.ForceStopAudio("disquiet ambience");
	}
	
    public void Return()
    {
		UpdateAudio(1);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 4);

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
