using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainEntranceLore : Interactable
{
    [Header("Main Entrance Lore")]
    [SerializeField] bool isColouredFlowers = false;
    [SerializeField] bool isPaintingFrameSet = false;
    [SerializeField] bool isPottedPlant = false;

    public override void Interact()
    {
        base.Interact();

        IdentifyObject();
    }

    void IdentifyObject()
    {
        if (isColouredFlowers)
        {
            UpdateMonologue(1);
        }
        if (isPaintingFrameSet)
        {
            UpdateMonologue(2);
        }
        if (isPottedPlant)
        {
            UpdateMonologue(3);
        }
    }

    void UpdateMonologue(int displayIndex)
    {
        switch (displayIndex)
        {
            case 1: //! Coloured Flowers
                gm.monologueManager.DisplaySentence(73);
                break;
            case 2: //! Painting Frame Set
                gm.monologueManager.DisplaySentence(74);
                break;
            case 3: //! Potted Plant
                gm.monologueManager.DisplaySentence(75);
                break;
            default:
                break;
        }
    }
}
