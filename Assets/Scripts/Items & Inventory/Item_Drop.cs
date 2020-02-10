using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Drop : MonoBehaviour
{
    private ItemLibrary il;
    public string itemName;
    public GameObject item_ui;
    Interactable it;

    // Start is called before the first frame update
    void Start()
    {
        it = GetComponent<Interactable>();
        il = GameObject.Find("ItemLibrary").GetComponent<ItemLibrary>();
        transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = il.GetSprite(itemName);
    }

    public void Pickup()
    {
        GameObject inventory = GameObject.Find("Inventory_UI");

        GameObject instance = Instantiate(item_ui, inventory.transform.GetChild(0));

        instance.GetComponent<Item_Inventory>().itemName = itemName;

        inventory.GetComponent<Inventory>().RandomizePosition();

        Destroy(gameObject);
    }


}
