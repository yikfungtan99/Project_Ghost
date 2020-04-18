using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoungeLore : Interactable
{
    [Header("Lounge Lore")]
    [SerializeField] bool isTableWithPlants = false;
    [SerializeField] bool isTwinPaintings = false;

    public override void Interact()
    {
        base.Interact();

        IdentifyObject();
    }

    void IdentifyObject()
    {
        if (isTableWithPlants)
        {
            UpdateMonologue(1);
        }
        if (isTwinPaintings)
        {
            UpdateMonologue(2);
        }
    }

    void UpdateMonologue(int displayIndex)
    {
        if(!gm.inTutorial)
        {
            switch (displayIndex)
            {
                case 1: //! Table with Plants
                    gm.monologueManager.DisplaySentence(47);
                    break;
                case 2: //! Twin Paintings
                    gm.monologueManager.DisplaySentence(48);
                    break;
                default:
                    break;
            }
        }
    }
}
