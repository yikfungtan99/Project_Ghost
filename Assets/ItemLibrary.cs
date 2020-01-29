using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemLibrary : MonoBehaviour
{

    public GameObject[] items;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    public GameObject FindItem(string itemName)
    {
        int counter = 0;

        if (itemName == items[counter].name)
        {
            return items[counter];
        }
        else
        {

        }


        Debug.Log("Item not found!");
        return null;
        
    }

}
