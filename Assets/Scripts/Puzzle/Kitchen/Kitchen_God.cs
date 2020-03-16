using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kitchen_God : Interactable
{
    private PuzzleManager pm;
    protected GameObject currentHeldItem;

    public override void Awake()
    {
        base.Awake();

        pm = gm.puzzleManager;
    }

    public override void Interact()
    {
        base.Interact();

        //! this interact function will not run until previous Dining Puzzle is completed
        if (pm.disableKitchenPuzzle)
        {
            Debug.Log("Puzzle Disabled either due to (1) Dining Puzzle not cleared or (2) Kitchen Puzzle already cleared.");
            return;
        }
        //! puzzle cannot be done again when it is cleared for the first time
        if (pm.isKitchenPuzzleClear)
        {
            return;
        }
        //! cannot be fully interacted with until kuih is obtained
        if(!pm.isKitchenGodEnabled)
        {
            UpdateMonologue(1, "");
            return;
        }

        if(!itemGiver)
        {
            //! Update player's current held item into this variable for easier checking with if statements
            if (gm.holdPanel.transform.childCount != 0)
            {
                currentHeldItem = gm.holdPanel.transform.GetChild(0).gameObject;
            }
            else
            {
                UpdateMonologue(1, "");
                return;
            }

            //! Comparing current player held item
            if (currentHeldItem.GetComponent<Item_Inventory>().itemName == pm.kitchenGodItemRequiredName)
            {
                Destroy(currentHeldItem);

                UpdateMonologue(3, "");

                itemGiver = true;
            }
            else
            {
                UpdateMonologue(2, "");
            }
        }
        else
        {
            SetPuzzleCompletion();

            UpdateMonologue(4, "");
        }
    }

    public override void UpdateMonologue(int displayIndex, string itemName)
    {
        switch(displayIndex)
        {
            case 1: //! kitchen altar disabled
                gm.monologueManager.DisplaySentence(24);
                break;
            case 2: //! enabled & wrong item
                gm.monologueManager.DisplaySentence(25);
                break;
            case 3: //! enabled & right item
                gm.monologueManager.DisplaySentence(26);
                break;
            case 4: //! obtain key item
                gm.monologueManager.DisplaySentence(27);
                break;
        }
    }

    protected void SetPuzzleCompletion()
    {
        pm.targetIngredient = "";
        pm.isSteamerMakingKuih = false;
        pm.disableKitchenPuzzle = true;
        pm.isKitchenPuzzleClear = true;

        if (!pm.kitchenPuzzleClearMsgTrigger)
        {
            pm.kitchenPuzzleClearMsgTrigger = true;

            Debug.Log("kitchen puzzle clear!");
        }
    }
}
