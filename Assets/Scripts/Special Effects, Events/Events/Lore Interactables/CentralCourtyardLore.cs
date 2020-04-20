using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CentralCourtyardLore : Interactable
{
    [Header("Central Courtyard Lore")]
    [SerializeField] bool isOrchid = false;
    [SerializeField] bool isPictureFrames = false;
	[SerializeField] bool isStairs = false;
    
    public override void Interact()
    {
        base.Interact();

        IdentifyObject();
    }

    void IdentifyObject()
    {
        if (isOrchid)
        {
            UpdateMonologue(1);
        }
        if (isPictureFrames)
        {
            UpdateMonologue(2);
        }
		if(isStairs)
		{
			UpdateMonologue(3);
		}
    }

    void UpdateMonologue(int displayIndex)
    {
		switch (displayIndex)
		{
			case 1: //! Orchid Plant
				gm.monologueManager.DisplaySentence(68);
				break;
			case 2: //! Picture Frames
				gm.monologueManager.DisplaySentence(69);
				break;
			default:
				break;
		}
		
        if(gm.inTutorial)
		{
			switch(displayIndex)
			{
				case 3:
					gm.monologueManager.DisplaySentence(93);
					break;
			}
		}
    }
}
