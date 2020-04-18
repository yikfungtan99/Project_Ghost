using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToiletLore : Interactable
{
    [Header("Toilet Lore")]
    [SerializeField] bool isBrokenMirror = false;
    [SerializeField] bool isToiletBowl = false;
    [SerializeField] bool isWaterTap = false;

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
        if (isToiletBowl)
        {
            UpdateMonologue(2);
        }
        if (isWaterTap)
        {
            UpdateMonologue(3);
        }
    }

    void UpdateMonologue(int displayIndex)
    {
        if(!gm.inTutorial)
        {
            switch (displayIndex)
            {
                case 1: //! Broken Mirror
                    gm.monologueManager.DisplaySentence(59);
                    break;
                case 2: //! Toilet Bowl
                    gm.monologueManager.DisplaySentence(60);
                    break;
                case 3: //! Water Tap
                    gm.monologueManager.DisplaySentence(61);
                    break;
                default:
                    break;
            }
        }
    }
}
