using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Drop : MonoBehaviour
{
    public GameObject itemType = null;

    public void Pickup()
    {
        GameObject.Find("Inventory").GetComponent<Inventory>().AddItem(itemType);
        Destroy(gameObject);
    }

}
