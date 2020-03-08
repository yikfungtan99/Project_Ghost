using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Photo_Frame_Pair : Lounge_Pairs
{
    public override void Awake()
    {
        base.Awake();

        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.enabled = false;
    }

    public override void Interact()
    {
        base.Interact();

        //! this interact function will not run until previous Kitchen Puzzle is completed
        if (pm.disableLoungePuzzle)
        {
            Debug.Log("Puzzle Disabled either due to (1) Kitchen Puzzle not cleared or (2) Lounge Puzzle already cleared.");
            return;
        }
        //! puzzle cannot be done again when it is cleared for the first time
        if (pm.isLoungePuzzleClear)
        {
            return;
        }

        if (gm.holdPanel.transform.childCount != 0)
        {
            currentHeldItem = gm.holdPanel.transform.GetChild(0).gameObject;

            if (currentHeldItem.GetComponent<Item_Inventory>().itemName == pm.photoframeRequiredName)
            {
                Destroy(currentHeldItem);

                spriteRenderer.enabled = true;
                pm.isPhotoFramePairComplete = true;

                UpdateMonologue(3, "");
            }
            else
            {
                UpdateMonologue(2, "");
            }
        }
        else
        {
            UpdateMonologue(1, "");
        }
    }

    public override void UpdateMonologue(int displayIndex, string itemName)
    {
        switch (displayIndex)
        {
            case 1:
                gm.monologueManager.GetComponent<MonologueManager>().DisplaySentence(14);
                break;
            case 2:
                gm.monologueManager.GetComponent<MonologueManager>().DisplaySentence(15);
                break;
            case 3:
                gm.monologueManager.GetComponent<MonologueManager>().DisplaySentence(18);
                break;
        }
    }
}
