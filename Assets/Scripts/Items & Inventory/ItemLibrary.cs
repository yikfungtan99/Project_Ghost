using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemLibrary : MonoBehaviour
{
    public Item[] items;

    public Sprite GetSprite(string itemName)
    {
        Sprite target = null;

        for (int i = 0; i < items.Length; i++)
        {

            if (itemName == items[i].itemName)
            {
                target = items[i].itemSprite;
            }
            
        }

        return target;
    }

}
