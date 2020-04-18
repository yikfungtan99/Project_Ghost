using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenLore : Interactable
{
    [Header("Kitchen Lore")]
    [SerializeField] bool isEmptyCupboard = false;
    [SerializeField] bool isCookingStation = false;
    [SerializeField] bool isForegroundTable = false;

    public override void Interact()
    {
        base.Interact();

        IdentifyObject();
    }

    void IdentifyObject()
    {
        if (isEmptyCupboard)
        {
            UpdateMonologue(1);
        }
        if (isCookingStation)
        {
            UpdateMonologue(2);
        }
        if (isForegroundTable)
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
                case 1: //! Empty Cupboard
                    gm.monologueManager.DisplaySentence(43);
                    break;
                case 2: //! Cooking Station
                    gm.monologueManager.DisplaySentence(44);
                    break;
                case 3: //! Foreground Table
                    gm.monologueManager.DisplaySentence(45);
                    break;
                default:
                    break;
            }
        }
    }
}
