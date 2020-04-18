using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridalBedroomLore : Interactable
{
    [Header("Bridal Bedroom Lore")]
    [SerializeField] bool isPictureFrame = false;
    [SerializeField] bool isMakeupTable = false;
    [SerializeField] bool isFaceWash = false;
    [SerializeField] bool isVase = false;

    public override void Interact()
    {
        base.Interact();

        IdentifyObject();
    }

    void IdentifyObject()
    {
        if (isPictureFrame)
        {
            UpdateMonologue(1);
        }
        if (isMakeupTable)
        {
            UpdateMonologue(2);
        }
        if (isFaceWash)
        {
            UpdateMonologue(3);
        }
        if(isVase)
        {
            UpdateMonologue(4);
        }
    }

    void UpdateMonologue(int displayIndex)
    {
        switch (displayIndex)
        {
            case 1: //! Picture Frame
                gm.monologueManager.DisplaySentence(64);
                break;
            case 2: //! Makeup Table
                gm.monologueManager.DisplaySentence(65);
                break;
            case 3: //! Face Wash Bowl
                gm.monologueManager.DisplaySentence(66);
                break;
            case 4: //! Giant Vase
                gm.monologueManager.DisplaySentence(67);
                break;
            default:
                break;
        }
    }
}
