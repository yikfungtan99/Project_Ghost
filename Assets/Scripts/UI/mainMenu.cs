using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mainMenu : MonoBehaviour
{

    public void PlayButton()
    {

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        //SceneManager.LoadScene(0);
    }

    public void QuitButton()
    {
        Debug.Log("Quit game");
        Application.Quit();

    }
}
