using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hallway4Lore : Interactable
{
    [Header("Hallway 4 Lore")]
    [SerializeField] bool isGrandfatherClockReplica = false;

    public override void Interact()
    {
        base.Interact();

        IdentifyObject();
    }

    void IdentifyObject()
    {
        if (isGrandfatherClockReplica)
        {
            UpdateMonologue(1);
        }
    }

    void UpdateMonologue(int displayIndex)
    {
        if (!gm.inTutorial)
        {
            switch (displayIndex)
            {
                case 1: //! Grandfather Clock Replica
                    gm.monologueManager.DisplaySentence(58);
                    break;
                default:
                    break;
            }
        }
    }
}
