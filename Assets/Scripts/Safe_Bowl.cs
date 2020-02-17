using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Safe_Bowl : Dining_Bowl
{
    public new void Interact()
    {
        Debug.Log("Clickin Safe bowl");

        CheckForSpoon();

        if (spoonTarget == null)
        {
            return;
        }

        //! Give Player key item
        if (!disablePuzzle)
        {
            disablePuzzle = true;
            isPuzzleClear = true;

            //! Destroy spoon object in player inventory
            if(spoonTarget != null)
            {
                Destroy(spoonTarget);
            }
            
            //! Instantiate key item reward in player inventory
            ConvertItemStringToInstance("stairwell key");

            UpdateMonologue();
        }
    }

    //Creating a new item to be slotted under Inventory (--> inventory panel) gameObject
    void ConvertItemStringToInstance(string name)
    {
        GameObject inventory = GameObject.Find("Inventory_UI");
        GameObject instance = Instantiate(item_ui, inventory.transform.GetChild(0));

        instance.GetComponent<Item_Inventory>().itemName = name;
    }

    void UpdateMonologue()
    {
        GameObject.Find("MonologueManager").GetComponent<MonologueManager>().DisplaySentence(7);
    }
}
