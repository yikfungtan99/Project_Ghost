using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Safe_Bowl : Dining_Bowl
{
    public new void Interact()
    {
        Debug.Log("Clickin Safe bowl");
        if (!GameObject.Find("Player").GetComponent<Player>().playerFainted)
        {
            disablePuzzle = false;
        }

        //! Give Player key item
        if (!disablePuzzle)
        {
            disablePuzzle = true;
            ConvertItemStringToInstance("stairwell key");
            isPuzzleClear = true;
        }
    }

    //Creating a new item to be slotted under Inventory (--> inventory panel) gameObject
    void ConvertItemStringToInstance(string name)
    {
        GameObject inventory = GameObject.Find("Inventory_UI");
        GameObject instance = Instantiate(item_ui, inventory.transform.GetChild(0));

        instance.GetComponent<Item_Inventory>().itemName = name;
    }
}
