using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kitchen_Steamer : Interactable
{
    private PuzzleManager pm;
    private GameObject currentHeldItem;
    
    public override void Awake()
    {
        base.Awake();

        pm = gm.puzzleManager;
    }
    
    void Update()
    {
        //! enable kitchen puzzle only if dining puzzle has been completed prior
        if(pm.isDiningPuzzleClear)
        {
            pm.disableKitchenPuzzle = false;
        }
        else
        {
            return;
        }

        //! switch case to check how many ingredients are put in the steamer & set variables respectively
        switch(pm.sequenceCount)
        {
            case 0:
                pm.targetIngredient = "flour";
                break;
            case 1:
                pm.targetIngredient = "sugar";
                break;
            case 2:
                pm.targetIngredient = "baking soda";
                break;
            case 3:
                pm.targetIngredient = "pink dye";
                break;
            case 4:
                pm.isSteamerMakingKuih = true;
                break;
            case 5:
                SetPuzzleCompletion();
                break;
        }

        //! Runs countdown when isSteamerMakingKuih is true
        if (pm.isSteamerMakingKuih)
        {
            if(!pm.makingKuihOnce)
            {
                pm.makingKuihOnce = true;

                UpdateMonologue(4, "");

                StartCoroutine(MakingKuihCountDown());
            }
        }
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

        if(!itemGiver)
        {
            //! Update player's current held item into this variable for easier checking with if statements
            if (gm.holdPanel.transform.childCount != 0)
            {
                currentHeldItem = gm.holdPanel.transform.GetChild(0).gameObject;
            }
            else
            {
                Debug.Log("No items held in hand.");

                UpdateMonologue(1, "");
                return;
            }

            //! Comparing current player held item to essential key (target) ingredient
            if (currentHeldItem.GetComponent<Item_Inventory>().itemName == pm.targetIngredient)
            {
                Destroy(currentHeldItem);

                UpdateMonologue(3, "");

                pm.sequenceCount += 1;
            }
            else
            {
                UpdateMonologue(2, "");
            }
        }
        else
        {
            pm.UpdatePuzzleCompleteMonologue(2, "");
            pm.sequenceCount += 1;
        }
        
    }

    IEnumerator MakingKuihCountDown()
    {
        pm.disableKitchenPuzzle = true;

        for (int i = pm.makingKuihCountDownTime; i > 0; i--)
        {
            Debug.Log("Kuih will be made in " + i + " second(s)!");
            yield return new WaitForSeconds(1);
        }
        Debug.Log("Ding! Ding! Something smells good.");

        UpdateMonologue(5, "");

        pm.disableKitchenPuzzle = false;
        itemGiver = true;
    }

    public override void UpdateMonologue(int displayIndex, string itemName)
    {
        switch(displayIndex)
        {
            case 1:
                gm.monologueManager.DisplaySentence(9);
                break;
            case 2:
                gm.monologueManager.DisplaySentence(10);
                break;
            case 3:
                gm.monologueManager.DisplaySentence(11);
                break;
            case 4:
                gm.monologueManager.DisplaySentence(12);
                break;
            case 5:
                gm.monologueManager.DisplaySentence(21);
                break;
        }
    }

    private void SetPuzzleCompletion()
    {
        pm.targetIngredient = "";
        pm.isSteamerMakingKuih = false;
        pm.disableKitchenPuzzle = true;
        pm.isKitchenPuzzleClear = true;

        gm.doorScript.SetIsLockedOnDoor(gm.doorVerticalLivingToLounge, false);
        gm.doorScript.SetIsLockedOnDoor(gm.doorVerticalKitchenToToilet, false);

        if(!pm.kitchenPuzzleClearMsgTrigger)
        {
            pm.kitchenPuzzleClearMsgTrigger = true;

            Debug.Log("kitchen puzzle clear!");
        }
    }
}
