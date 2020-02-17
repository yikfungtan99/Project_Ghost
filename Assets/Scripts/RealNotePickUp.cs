using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RealNotePickUp : MonoBehaviour
{
    private Interactable it;
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;
    public Canvas canvas;
    static int NoteCount = 0;
    public static bool Note1PickedUp = false;
    public static bool Note2PickedUp = false;

    void Update()
    {
        if (GameIsPaused == true)
        {
            canvas.transform.GetChild(2).gameObject.SetActive(true);
            GameIsPaused = false;
        }
    }
    public void ToPause()
    {
        GameIsPaused = true;
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
        GameIsPaused = true;
    }


    private void Awake()
    {
        it = GetComponent<Interactable>();
    }

    public void Interact()
    {
        
            NoteCount += 1;
            Debug.Log(NoteCount);
            Destroy(gameObject);
            Debug.Log("Note picked up");

            if (NoteCount == 1)
            {
                Note1PickedUp = true;
                Debug.Log("Note1 has been picked up");
            }
            if (NoteCount == 2)
            {
                Note2PickedUp = true;
                Debug.Log("Note2 has been picked up");
            }
    }
    public void ToNotes()
    {
        Debug.Log(Note1PickedUp);
        Debug.Log(Note2PickedUp);
        if (Note1PickedUp == false && Note2PickedUp == false)
        {
            canvas.transform.GetChild(2).gameObject.SetActive(false);
            canvas.transform.GetChild(3).gameObject.SetActive(true);
            canvas.transform.GetChild(4).gameObject.SetActive(false);
            canvas.transform.GetChild(5).gameObject.SetActive(false);
        }
        else if (Note1PickedUp == true && Note2PickedUp == false)
        {
            canvas.transform.GetChild(2).gameObject.SetActive(false);
            canvas.transform.GetChild(3).gameObject.SetActive(false);
            canvas.transform.GetChild(4).gameObject.SetActive(true);
            canvas.transform.GetChild(4).GetChild(0).gameObject.SetActive(false);

            GameObject.Find("NotesMenu1").SetActive(true);
            Debug.Log("Set Note1Active");
        }
        else if (Note1PickedUp == true && Note2PickedUp == true)
        {
            canvas.transform.GetChild(2).gameObject.SetActive(false);
            canvas.transform.GetChild(3).gameObject.SetActive(false);
            canvas.transform.GetChild(4).gameObject.SetActive(true);
            canvas.transform.GetChild(4).GetChild(0).gameObject.SetActive(true);
        }
    }

    public void AllowedToGoToNextNote()
    {
        canvas.transform.GetChild(2).gameObject.SetActive(false);
        canvas.transform.GetChild(3).gameObject.SetActive(false);
        canvas.transform.GetChild(4).gameObject.SetActive(false);
        canvas.transform.GetChild(5).gameObject.SetActive(true);
    }

    public void ReturnToPreviousNote()
    {
        canvas.transform.GetChild(2).gameObject.SetActive(false);
        canvas.transform.GetChild(3).gameObject.SetActive(false);
        canvas.transform.GetChild(4).gameObject.SetActive(true);
        canvas.transform.GetChild(5).gameObject.SetActive(false);
    }

    public void NotesBack()
    {
        canvas.transform.GetChild(2).gameObject.SetActive(true);
        canvas.transform.GetChild(3).gameObject.SetActive(false);
        canvas.transform.GetChild(4).gameObject.SetActive(false);
        canvas.transform.GetChild(5).gameObject.SetActive(false);
    }
}
