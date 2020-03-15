using System.Collections;
using System.Collections.Generic;
using UnityEngine.Experimental.Rendering.LWRP;
using UnityEngine;

public class Item_Drop : Interactable
{
    private ItemLibrary il;
    public GameObject item_ui;

    // Start is called before the first frame update
    void Start()
    {
        il = gm.itemLibrary.GetComponent<ItemLibrary>();
        transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = il.GetSprite(itemName);
    }

    public override void Interact()
    {
        base.Interact();
        Pickup();
    }

    private void Pickup()
    {
        GameObject inventory = GameObject.Find("Inventory_UI");

        GameObject instance = Instantiate(item_ui, inventory.transform.GetChild(0));

        instance.GetComponent<Item_Inventory>().itemName = itemName;

        inventory.GetComponent<Inventory>().RandomizePosition();

        UpdateMonologue(-1, itemName);
        UpdateAudio(1);

        Destroy(gameObject);
    }

    public override void UpdateMonologue(int displayIndex, string itemName)
    {
        if(displayIndex == -1)
        {
            gm.monologueManager.DisplayPickUpSentence(itemName, false);

            return;
        }
    }

    public override void UpdateAudio(int index)
    {
        switch(index)
        {
            case 1:
                gm.audioManager.ForcePlayAudio("obtain item");
                break;
        }
    }
}
