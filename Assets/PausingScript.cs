using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PausingScript : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;
    public Canvas canvas;
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if(GameIsPaused == true)
        {
            canvas.transform.GetChild(2).gameObject.SetActive(true);
            GameIsPaused = false;
        }
    }

    public void ToPause()
    {
        GameIsPaused = true;
        Pause();
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
    }

    public void LoadOptions()
    {
        Debug.Log("Loading options...");
        canvas.transform.GetChild(2).gameObject.SetActive(false);
        canvas.transform.GetChild(3).gameObject.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void OptionsBack()
    {
        Debug.Log("Returning to menu...");
        canvas.transform.GetChild(2).gameObject.SetActive(true);
        canvas.transform.GetChild(3).gameObject.SetActive(false);

    }
}
