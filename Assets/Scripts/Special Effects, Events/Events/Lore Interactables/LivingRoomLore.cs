using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivingRoomLore : Interactable
{
    [Header("Living Room Lore")]
    [SerializeField] bool isGrandfatherClock = false;

    public override void Interact()
    {
        base.Interact();

        IdentifyObject();
    }

    void IdentifyObject()
    {
        if(isGrandfatherClock)
        {
            UpdateMonologue(1);
        }
    }

    void UpdateMonologue(int displayIndex)
    {
        switch(displayIndex)
        {
            case 1: //! Grandfather Clock
                gm.monologueManager.DisplaySentence(40);
                break;
            default:
                break;
        }
    }
}
