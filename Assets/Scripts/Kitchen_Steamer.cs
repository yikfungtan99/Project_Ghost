﻿using System.Collections;
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

    void Start()
    {
        
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
        switch(pm.ingredientCount)
        {
            case 0:
                pm.targetIngredient = "flour";
                //Debug.Log("Ingredient needed: Flour");
                break;
            case 1:
                pm.targetIngredient = "sugar";
                //Debug.Log("Ingredient needed: Sugar");
                break;
            case 2:
                pm.targetIngredient = "baking soda";
                //Debug.Log("Ingredient needed: Baking Soda");
                break;
            case 3:
                pm.targetIngredient = "pink dye";
                //Debug.Log("Ingredient needed: Pink Dye");
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

                UpdateMonologue(4);

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
            Debug.Log("Need to complete dining puzzle first");
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

                UpdateMonologue(1);
                return;
            }

            //! Comparing current player held item to essential key (target) ingredient
            if (currentHeldItem.GetComponent<Item_Inventory>().itemName == pm.targetIngredient)
            {
                Destroy(currentHeldItem);

                UpdateMonologue(3);

                pm.ingredientCount += 1;
            }
            else
            {
                UpdateMonologue(2);
            }
        }
        else
        {
            UpdateMonologue(5);
        }
        
    }

    IEnumerator MakingKuihCountDown()
    {
        pm.disableKitchenPuzzle = true;

        yield return new WaitForSeconds(pm.makingKuihCountDownTime);

        pm.disableKitchenPuzzle = false;
        itemGiver = true;
    }

    public override void UpdateMonologue(int displayIndex)
    {
        switch(displayIndex)
        {
            case 1:
                gm.monologueManager.GetComponent<MonologueManager>().DisplaySentence(9);
                break;
            case 2:
                gm.monologueManager.GetComponent<MonologueManager>().DisplaySentence(10);
                break;
            case 3:
                gm.monologueManager.GetComponent<MonologueManager>().DisplaySentence(11);
                break;
            case 4:
                gm.monologueManager.GetComponent<MonologueManager>().DisplaySentence(12);
                break;
            case 5:
                gm.monologueManager.GetComponent<MonologueManager>().DisplaySentence(13);
                break;
        }
    }

    private void SetPuzzleCompletion()
    {
        pm.targetIngredient = "";
        pm.isSteamerMakingKuih = false;
        pm.disableKitchenPuzzle = true;
        pm.isKitchenPuzzleClear = true;

        if(!pm.kitchenPuzzleClearMsgTrigger)
        {
            pm.kitchenPuzzleClearMsgTrigger = true;

            Debug.Log("kitchen puzzle clear!");
        }
    }
}