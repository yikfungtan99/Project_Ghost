using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mainMenu : MonoBehaviour
{
	void Awake()
	{
		Time.timeScale = 1f;
	}
	
    public void PlayButton()
    {
		UpdateAudio(1);
		
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        //SceneManager.LoadScene(1);
    }

    public void QuitButton()
    {
		UpdateAudio(1);
		
        Debug.Log("Quit game");
        Application.Quit();
    }
	
	void UpdateAudio(int index)
	{
		switch(index)
		{
			case 1: //! Click any button
				AudioManager.instance.ForcePlayAudio("button click");
				break;
		}
	}
}
