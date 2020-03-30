using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PausingScript : MonoBehaviour
{
    public Canvas canvas;
    void Start()
    {
       
    }

    void Update()
    {
        if (GameManager.Instance.gamePaused == true)
        {
            GetCanvasChild(2).SetActive(true);
        }
    }

    GameObject GetCanvasChild(int index)
    {
        return canvas.transform.GetChild(index).gameObject;
    }

    public void ToPause()
    {
        GameManager.Instance.playerMovement.enRoute = false;
        Debug.Log("ToPause()");
        Pause();
    }

    public void Resume()
    {
        // pauseMenuUI.SetActive(false);
        GetCanvasChild(2).SetActive(false);
        Time.timeScale = 1f;
        GameManager.Instance.SetPause(false);
    }
    void Pause()
    {
        Debug.Log("Pause()");
        GetCanvasChild(2).SetActive(true);
        Time.timeScale = 0f;
        GameManager.Instance.SetPause(true);
    }

    public void QuitGame()
    {
        GameManager.Instance.SetPause(false);
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenuIGuess");
        //Application.Quit();
    }

    public void PauseMenuQuitButtonToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void ToNotes()
    {
        Debug.Log(NotePickUp.Note1PickedUp);
        Debug.Log(NotePickUp.Note2PickedUp);
        if (NotePickUp.Note1PickedUp == false && NotePickUp.Note2PickedUp == false)
        {
            GetCanvasChild(2).SetActive(false);
            GetCanvasChild(3).SetActive(true);//NoNotesYetMenu
            GetCanvasChild(4).SetActive(false);
            GetCanvasChild(5).SetActive(false);
        }
        else if (NotePickUp.Note1PickedUp == true && NotePickUp.Note2PickedUp == false)
        {
            GetCanvasChild(2).SetActive(false);
            GetCanvasChild(3).SetActive(false);
            GetCanvasChild(4).SetActive(true);//NotesMenu1
            GetCanvasChild(4).transform.GetChild(0).gameObject.SetActive(false);//NotesMenu1's next button

            GameObject.Find("NotesMenu1").SetActive(true);
            Debug.Log("Set Note1Active");
        }
        else if (NotePickUp.Note1PickedUp == true && NotePickUp.Note2PickedUp == true)
        {
            GetCanvasChild(2).SetActive(false);
            GetCanvasChild(3).SetActive(false);
            GetCanvasChild(4).SetActive(true);
            GetCanvasChild(4).transform.GetChild(0).gameObject.SetActive(true);
        }
    }

    public void AllowedToGoToNextNote() //NOT USED (FOR NOW)
    {
        GetCanvasChild(2).SetActive(false);
        GetCanvasChild(3).SetActive(false);
        GetCanvasChild(4).SetActive(false);
        GetCanvasChild(5).SetActive(false);
    }

    public void CanGoFromNote2ToNote3()
    {
        if(NotePickUp.Note3PickedUp == false)
        {
            GetCanvasChild(5).transform.GetChild(0).gameObject.SetActive(false);
        }
        if(NotePickUp.Note3PickedUp == true)
        {
            GetCanvasChild(5).transform.GetChild(0).gameObject.SetActive(true);
        }
    }

    public void CanGoFromNote3ToNote4()
    {
        if (NotePickUp.Note4PickedUp == false)
        {
            GetCanvasChild(6).transform.GetChild(0).gameObject.SetActive(false);
        }
        if (NotePickUp.Note4PickedUp == true)
        {
            GetCanvasChild(6).transform.GetChild(0).gameObject.SetActive(true);
        }
    }

    public void CanGoFromNote4ToNote5()
    {
        if (NotePickUp.Note5PickedUp == false)
        {
            GetCanvasChild(7).transform.GetChild(0).gameObject.SetActive(false);
        }
        if (NotePickUp.Note5PickedUp == true)
        {
            GetCanvasChild(7).transform.GetChild(0).gameObject.SetActive(true);
        }
    }

    public void CanGoFromNote5ToNote6()
    {
        if (NotePickUp.Note6PickedUp == false)
        {
            GetCanvasChild(8).transform.GetChild(0).gameObject.SetActive(false);
        }
        if (NotePickUp.Note6PickedUp == true)
        {
            GetCanvasChild(8).transform.GetChild(0).gameObject.SetActive(true);
        }
    }

    public void CanGoFromNote6ToNote7()
    {
        if (NotePickUp.Note7PickedUp == false)
        {
            GetCanvasChild(9).transform.GetChild(0).gameObject.SetActive(false);
        }
        if (NotePickUp.Note7PickedUp == true)
        {
            GetCanvasChild(9).transform.GetChild(0).gameObject.SetActive(true);
        }
    }

    public void CanGoFromNote7ToNote8()
    {
        if (NotePickUp.Note8PickedUp == false)
        {
            GetCanvasChild(10).transform.GetChild(0).gameObject.SetActive(false);
        }
        if (NotePickUp.Note8PickedUp == true)
        {
            GetCanvasChild(10).transform.GetChild(0).gameObject.SetActive(true);
        }
    }

    public void ReturnToPreviousNote() //NOT USED
    {
        GetCanvasChild(2).SetActive(false);
        GetCanvasChild(3).SetActive(false);
        GetCanvasChild(4).SetActive(true);
        GetCanvasChild(5).SetActive(false);
    }

    public void NotesBack()
    {
        GetCanvasChild(2).SetActive(true);//PauseMenu
        GetCanvasChild(3).SetActive(false);//NoNotesYetMenu
        GetCanvasChild(4).SetActive(false);//NotesMenu1
        GetCanvasChild(5).SetActive(false);//NotesMenu2
        GetCanvasChild(6).SetActive(false);//NotesMenu3
        GetCanvasChild(7).SetActive(false);//NotesMenu4
        GetCanvasChild(8).SetActive(false);//NotesMenu5
        GetCanvasChild(9).SetActive(false);//NotesMenu6
        GetCanvasChild(10).SetActive(false);//NotesMenu7
        GetCanvasChild(11).SetActive(false);//NotesMenu8
        GetCanvasChild(12).SetActive(false);//NotesMenu9
        GetCanvasChild(13).SetActive(false);//NotesMenu10
    }

}
