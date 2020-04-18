using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AltarLore : Interactable
{
    [Header("Altar Room Lore")]
    [SerializeField] bool isThreeBuddhas = false;
    [SerializeField] bool isGuanYin = false;
    [SerializeField] bool isWindow = false;

    public override void Interact()
    {
        base.Interact();

        IdentifyObject();
    }

    void IdentifyObject()
    {
        if (isThreeBuddhas)
        {
            UpdateMonologue(1);
        }
        if (isGuanYin)
        {
            UpdateMonologue(1);
        }
        if (isWindow)
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
                case 1: //! Three Buddhas Painting
                    gm.monologueManager.DisplaySentence(70);
                    break;
                case 2: //! Windows
                    gm.monologueManager.DisplaySentence(72);
                    break;
                default:
                    break;
            }
        }
        else
        {
            switch(displayIndex)
            {
                case 1: //! Guan Yin Statue
                    gm.monologueManager.DisplaySentence(71);
                    break;
            }
        }
    }
}
