using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public bool inRange = false;
    public bool interactable = true;

    string[] stringtags = new string[] { "Door", "Prop", "Dropped_Item"};

    public void Interact()
    {
        if(gameObject.tag == stringtags[0])
        {
            GetComponent<Door>().Interact();
        }
        else if(gameObject.tag == stringtags[1])
        {
            GetComponent<Prop_Interactable>().Interact();
        }
        else if (gameObject.tag == stringtags[2])
        {
            GetComponent<Item_Drop>().Pickup();
        }
    }
}
