using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public void AddItem(GameObject itemType)
    {
        Instantiate(itemType, transform.GetChild(0));
    }
}
