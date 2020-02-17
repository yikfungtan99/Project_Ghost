using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable_Give_Item : MonoBehaviour
{
    public string itemName;

    public int charges = 1;

    public void Give_Item()
    {
        if(charges > 0)
        {
            GameObject.Find("Player").transform.GetChild(2).GetComponent<Inventory>().ObtainItem(itemName);
            charges -= 1;
        }

    }

}
