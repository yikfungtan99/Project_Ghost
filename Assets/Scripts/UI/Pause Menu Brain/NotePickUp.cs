using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotePickUp : Interactable
{
    
    static int NoteCount = 0;
    public static bool Note1PickedUp = false;
    public static bool Note2PickedUp = false;
    public static bool Note3PickedUp = false;
    public static bool Note4PickedUp = false;
    public static bool Note5PickedUp = false;
    public static bool Note6PickedUp = false;
    public static bool Note7PickedUp = false;
    public static bool Note8PickedUp = false;
    public override void Awake()
    {
        base.Awake();
        NoteCount = 0;
        Note1PickedUp = false;
        Note2PickedUp = false;
        Note3PickedUp = false;
        Note4PickedUp = false;
        Note5PickedUp = false;
        Note6PickedUp = false;
        Note7PickedUp = false;
        Note8PickedUp = false;
    }

   

    public override void Interact()
    {
        base.Interact();
        NoteCount += 1;
        Debug.Log(NoteCount);
        Destroy(gameObject);
        Debug.Log("Note picked up");

        if (NoteCount == 1)
        {
            Note1PickedUp = true;
          
            Debug.Log("Note1 has been picked up");
            UpdateMonologue();
        }
        if (NoteCount == 2)
        {
            Note2PickedUp = true;
            Note3PickedUp = true;
            Note4PickedUp = true;
            Debug.Log("Note2 has been picked up");
            UpdateMonologue();
        }
        if (NoteCount == 3)
            {
            Note5PickedUp = true;
            Note6PickedUp = true;
            
            Debug.Log("Note3 has been picked up");
                UpdateMonologue();
            }
          /*  if (NoteCount == 4)
            {
                Note4PickedUp = true;
                Debug.Log("Note4 has been picked up");
                UpdateMonologue();
            }
            if (NoteCount == 5)
            {
                Note5PickedUp = true;
                Debug.Log("Note5 has been picked up");
                UpdateMonologue();
            }
            if (NoteCount == 6)
            {
                Note6PickedUp = true;
                Debug.Log("Note6 has been picked up");
                UpdateMonologue();
            }
            if (NoteCount == 7)
            {
                Note7PickedUp = true;
                Debug.Log("Note7 has been picked up");
                UpdateMonologue();
            }
            if (NoteCount == 8)
            {
                Note8PickedUp = true;
                Debug.Log("Note8 has been picked up");
                UpdateMonologue();
            }*/
    }
   
    void UpdateMonologue()
    {
        GameObject.Find("MonologueManager").GetComponent<MonologueManager>().DisplaySentence(3);
    }
}
