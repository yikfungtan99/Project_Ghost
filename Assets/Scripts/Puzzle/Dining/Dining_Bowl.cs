using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dining_Bowl : Interactable
{
    protected PuzzleManager pm;
    
    // Start is called before the first frame update
    public override void Awake()
    {
        base.Awake();
        pm = gm.puzzleManager;

        pm.isDiningPuzzleClear = false;
        pm.spoonTarget = null;
        pm.disableDiningPuzzle = false;
        pm.diningForewarnPlayer = false;
        pm.diningPuzzleClearMsgTrigger = false;
    }

    void Update()
    {
        if(pm.isDiningPuzzleClear && !pm.diningPuzzleClearMsgTrigger)
        {
            pm.diningPuzzleClearMsgTrigger = true;
            Debug.Log("Congrats! You cleared the puzzle! You obtained a 'Key' Item!");
            return;
        }

        //Debug.Log(disablePuzzle);
    }

    void ResetPuzzleOnPlayerRespawn()
    {
        if (gm.player.playerFainted == false)
        {
            pm.disableDiningPuzzle = false;
            pm.diningForewarnPlayer = false;
        }
    }

    //! Checks if spoon is inside player inventory.
    //  If spoon is not found, return. If spoon is found, its parent's child index will be recorded (position in inventory)
    protected void CheckForSpoon()
    {
        pm.spoonTarget = null;
        if(pm.isDiningPuzzleClear)
        {
            return;
        }

        if (gm.holdPanel.transform.childCount == 0 || gm.holdPanel.transform.GetChild(0).GetComponent<Item_Inventory>().itemName != "spoon")
        {
            Debug.Log("You can do nothing with the bowl as it is. Maybe a spoon will help.");

            UpdateMonologue(1, "");
            return;
        }
        else
        {
            if (pm.isDiningPuzzleClear)
            {
                return;
            }

            if (!pm.diningForewarnPlayer)
            {
                pm.diningForewarnPlayer = true;
                Debug.Log("I think I have to be a little more cautious about this...");

                UpdateMonologue(2, "");
            }
            else
            {
                pm.spoonTarget = gm.holdPanel.transform.GetChild(0).gameObject;
                ResetPuzzleOnPlayerRespawn();
            }
        }
    }

    public override void UpdateMonologue(int displayIndex, string itemName)
    {
        if(displayIndex == 1)
        {
            gm.monologueManager.GetComponent<MonologueManager>().DisplaySentence(5);
        }
        else if(displayIndex == 2)
        {
            gm.monologueManager.GetComponent<MonologueManager>().DisplaySentence(6);
        }
        else
        {
            Debug.LogError("displayIndex in UpdateMonologue() is out of bounds.");
        }
    }
}
