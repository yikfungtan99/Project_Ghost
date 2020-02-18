using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewPauseScript : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;
    public Canvas canvas;
    int NoteCount = 0;
    public static bool Note1PickedUp = false;
    public static bool Note2PickedUp = false;
    void Start()
    {

    }

    // Update is called once per frame
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

    public void ToNotes()
    {
        if(Note1PickedUp == false && Note2PickedUp == false)
        {
            canvas.transform.GetChild(2).gameObject.SetActive(false);
            canvas.transform.GetChild(3).gameObject.SetActive(true);
            canvas.transform.GetChild(4).gameObject.SetActive(false);
            canvas.transform.GetChild(5).gameObject.SetActive(false);
        }
        else if(Note1PickedUp == true && Note2PickedUp == false)
        {
            canvas.transform.GetChild(2).gameObject.SetActive(false);
            canvas.transform.GetChild(3).gameObject.SetActive(false);
            canvas.transform.GetChild(4).gameObject.SetActive(true);
            canvas.transform.GetChild(4).GetChild(0).gameObject.SetActive(false);
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

    public void OnMouseDown()
    {
        if (Input.GetMouseButtonDown(0))
        {
         //   Note1PickedUp = true;
         //   Note2PickedUp = true;
            NoteCount += 1;
            Destroy(gameObject);
            Debug.Log("Pressed Once");
            
            if(NoteCount == 1)
            {
                Note1PickedUp = true;
                Debug.Log("Note1 has been picked up");
             //   Note2PickedUp = true;
            }
            if (NoteCount == 2)
            {
                Note2PickedUp = true;
                Debug.Log("Note2 has been picked up");
            }
        }
            
        
       /* if (Note1PickedUp == true)
        {

            IEnumerator ShowNoteText()
            {

                canvas.transform.GetChild(0).gameObject.SetActive(true);

                yield return new WaitForSeconds(3); // set for however long you want it open.

                canvas.transform.GetChild(0).gameObject.SetActive(false);

            }


            for (int i = 0; i < 3; i++)
            {
                canvas.transform.GetChild(0).gameObject.SetActive(true);
                Debug.Log("Counting...");
            }
            canvas.transform.GetChild(0).gameObject.SetActive(false);
        }*/
    }

  /*  public void OnMouseDown1()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Destroy(gameObject);
            Debug.Log("Pressed Twice");
            Note2PickedUp = true;
            Debug.Log("Note2 has been picked up");
        }
        if (Note2PickedUp == true)
        {
            IEnumerator ShowNoteText()
            {

                canvas.transform.GetChild(0).gameObject.SetActive(true);

                yield return new WaitForSeconds(3); // set for however long you want it open.

                canvas.transform.GetChild(0).gameObject.SetActive(false);

            }
            for (int i = 0; i < 3; i++)
            {
                canvas.transform.GetChild(0).gameObject.SetActive(true);
                Debug.Log("Counting2...");
            }
            canvas.transform.GetChild(0).gameObject.SetActive(false);
        }
    }*/

    public void QuitButton()
    {

        Application.Quit();

    }

}
