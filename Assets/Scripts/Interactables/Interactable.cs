using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public bool inRange = false;
    public bool interactable = true;
    public bool isSeen = false;

    string[] stringtags = new string[] { "Door", "Prop", "Dropped_Item","Hiding_Spot", "Candle", "Safe_Bowl", "Death_Bowl", "Note"};

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
        else if (gameObject.tag == stringtags[3])
        {
            GetComponent<Hidable>().Hide();
        }
        else if (gameObject.tag == stringtags[4])
        {
            GetComponent<Candle>().LightCandle();
        }
        else if (gameObject.tag == stringtags[5])
        {
            GetComponent<Safe_Bowl>().Interact();
        }
        else if (gameObject.tag == stringtags[6])
        {
            GetComponent<Death_Bowl>().Interact();
        }
        else if(gameObject.tag == stringtags[7])
        {
            GetComponent<RealNotePickUp>().Interact();
        }

        if (GetComponent<Interactable_Give_Item>())
        {

            GetComponent<Interactable_Give_Item>().Give_Item();

        }

        if (GetComponent<pocTrigger>())
        {

            GetComponent<pocTrigger>().ActivateTrigger();

        }
    }

    void EnableParticles()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).GetComponent<ParticleSystem>())
            {
                transform.GetChild(i).gameObject.SetActive(inRange);
            }
        }
    }

    private void Update()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).GetComponent<ParticleSystem>())
            {
                transform.GetChild(i).gameObject.SetActive(isSeen);
            }
        }
    }

    private void LateUpdate()
    {
        inRange = false;
        isSeen = false;
    }

}
