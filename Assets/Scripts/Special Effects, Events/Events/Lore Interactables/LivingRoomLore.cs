using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivingRoomLore : Interactable
{
    [Header("Living Room Lore")]
    [SerializeField] bool isGrandfatherClock = false;
    [SerializeField] bool isBench = false;
    [SerializeField] bool isRightCabinet = false;

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
        if(isBench)
        {
            UpdateMonologue(2);
        }
        if(isRightCabinet)
        {
            UpdateMonologue(3);
        }
    }

    void UpdateMonologue(int displayIndex)
    {
        switch(displayIndex)
        {
            case 1: //! Grandfather Clock
                gm.monologueManager.DisplaySentence(40);
                break;
            case 2: //! Bench(es)
                gm.monologueManager.DisplaySentence(41);
                break;
            case 3: //! Rightmost Cabinet
                gm.monologueManager.DisplaySentence(42);
                break;
            default:
                break;
        }
    }
}
