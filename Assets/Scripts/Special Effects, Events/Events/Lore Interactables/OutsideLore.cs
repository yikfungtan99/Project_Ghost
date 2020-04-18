using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutsideLore : Interactable
{
    [Header("Outside Lore")]
    [SerializeField] bool isWindow = false;

    public override void Interact()
    {
        base.Interact();

        IdentifyObject();
    }

    void IdentifyObject()
    {
        if (isWindow)
        {
            UpdateMonologue(1);
        }
    }

    void UpdateMonologue(int displayIndex)
    {
        if(gm.inTutorial)
        {
            switch (displayIndex)
            {
                case 1: //! Window
                    gm.monologueManager.DisplaySentence(49);
                    break;
                default:
                    break;
            }
        }
    }
}
