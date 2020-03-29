using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotePickup : Interactable
{
    public bool isPickedUp = false;
    public int noteIndex;

  /*  private void Update()
    {
        if(JournalManager.Instance.CheckNoteStatus(noteIndex))
        {
            this.gameObject.SetActive(false);
        }
    }*/

    public override void Interact()
    {
        base.Interact();
        this.gameObject.SetActive(false);
        JournalManager.Instance.PickedUp(noteIndex);
    }
}
