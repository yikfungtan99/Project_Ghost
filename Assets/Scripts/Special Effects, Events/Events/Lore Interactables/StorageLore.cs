using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorageLore : Interactable
{
    [Header("Storage Lore")]
    [SerializeField] bool isAnyShelf = false;
    [SerializeField] bool isTableCandle = false;

    public override void Interact()
    {
        base.Interact();

        IdentifyObject();
    }

    void IdentifyObject()
    {
        if (isAnyShelf)
        {
            UpdateMonologue(1);
        }
        if (isTableCandle)
        {
            UpdateMonologue(2);
        }
    }

    void UpdateMonologue(int displayIndex)
    {
        if (!gm.inTutorial)
        {
            switch (displayIndex)
            {
                case 1: //! Any Shelf
                    gm.monologueManager.DisplaySentence(62);
                    break;
                case 2: //! Candle on Table
                    gm.monologueManager.DisplaySentence(63);
                    break;
                default:
                    break;
            }
        }
    }
}
