using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiningLore : Interactable
{
    [Header("Dining Room Lore")]
    [SerializeField] bool isBrokenMirror = false;
    [SerializeField] bool isLongTable = false;
    [SerializeField] bool isLeftmostShelf = false;
    [SerializeField] bool isGrandfatherClockReplica = false;

    public override void Interact()
    {
        base.Interact();

        IdentifyObject();
    }

    void IdentifyObject()
    {
        if (isBrokenMirror)
        {
            UpdateMonologue(1);
        }
        if (isLongTable)
        {
            UpdateMonologue(2);
        }
        if (isLeftmostShelf)
        {
            UpdateMonologue(3);
        }
        if (isGrandfatherClockReplica)
        {
            UpdateMonologue(4);
        }
    }

    void UpdateMonologue(int displayIndex)
    {
        if (!gm.inTutorial)
        {
            switch (displayIndex)
            {
                case 1: //! Broken Mirror
                    gm.monologueManager.DisplaySentence(54);
                    break;
                case 2: //! Long Dining Table
                    gm.monologueManager.DisplaySentence(55);
                    break;
                case 3: //! Leftmost Shelf
                    gm.monologueManager.DisplaySentence(56);
                    break;
                case 4: //! Grandfather Clock Replica
                    gm.monologueManager.DisplaySentence(57);
                    break;
                default:
                    break;
            }
        }
    }
}
