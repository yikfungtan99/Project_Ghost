using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hallway2Lore : Interactable
{
    [Header("Hallway 2 Lore")]
    [SerializeField] bool isAnything = false;

    public override void Interact()
    {
        base.Interact();

        IdentifyObject();
    }

    void IdentifyObject()
    {
        if (isAnything)
        {
            UpdateMonologue(1);
        }
    }

    void UpdateMonologue(int displayIndex)
    {
        if(!gm.inTutorial)
        {
            switch (displayIndex)
            {
                case 1: //! Any Item
                    gm.monologueManager.DisplaySentence(46);
                    break;
                default:
                    break;
            }
        }
    }
}
