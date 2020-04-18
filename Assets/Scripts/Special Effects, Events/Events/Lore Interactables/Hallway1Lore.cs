using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hallway1Lore : Interactable
{
    [Header("Hallway 1 Lore")]
    [SerializeField] bool isDrawerTable = false;
    [SerializeField] bool isGrandchildClock = false;
    [SerializeField] bool isSymmetricalChairs = false;
    [SerializeField] bool isCabinet = false;

    public override void Interact()
    {
        base.Interact();

        IdentifyObject();
    }

    void IdentifyObject()
    {
        if (isDrawerTable)
        {
            UpdateMonologue(1);
        }
        if (isGrandchildClock)
        {
            UpdateMonologue(2);
        }
        if (isSymmetricalChairs)
        {
            UpdateMonologue(3);
        }
        if (isCabinet)
        {
            UpdateMonologue(4);
        }
    }

    void UpdateMonologue(int displayIndex)
    {
        switch (displayIndex)
        {
            case 1: //! Drawer Table
                gm.monologueManager.DisplaySentence(50);
                break;
            case 2: //! Grandchild Clock
                gm.monologueManager.DisplaySentence(51);
                break;
            case 3: //! Symmetrical Chairs
                gm.monologueManager.DisplaySentence(52);
                break;
            case 4: //! Cabinet
                gm.monologueManager.DisplaySentence(53);
                break;
            default:
                break;
        }
    }
}
