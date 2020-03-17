using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stairs : Interactable
{
    PuzzleManager pm;
    GameObject doorBlocker;

    bool triggerOnce;

    public override void Awake()
    {
        base.Awake();

        pm = gm.puzzleManager;
        doorBlocker = transform.GetChild(0).gameObject;

        triggerOnce = false;
    }

    private void Update()
    {
        if(!triggerOnce)
        {
            if (gm.inTutorial)
            {
                doorBlocker.SetActive(false);
            }
            else
            {
                triggerOnce = true;
                doorBlocker.SetActive(true);
            }
        }
    }

    public override void Interact()
    {
        base.Interact();

        if(gm.inTutorial)
        {
            return;
        }

        if(gm.holdPanel.transform.childCount != 0)
        {
            if(gm.holdPanel.transform.GetChild(0).GetComponent<Item_Inventory>().itemName == pm.stairsItemRequiredName)
            {
                doorBlocker.SetActive(false);

                //! show win screen here
            }
            else
            {
                UpdateAudio(1);
                UpdateMonologue(1, "");
            }
        }
        else
        {
            UpdateAudio(1);
            UpdateMonologue(1, "");
        }
    }

    public override void UpdateMonologue(int displayIndex, string itemName)
    {
        switch(displayIndex)
        {
            case 1: //! ghost block to stairs tells player something is missing

                break;
        }
    }

    public override void UpdateAudio(int index)
    {
        switch(index)
        {
            case 1:
                gm.audioManager.ForcePlayAudio("locked door");
                break;
        }
    }
}
