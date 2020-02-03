using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject gm;

    //Player bool
    public bool inventoryOn = false;
    public bool targetOnInteractable = false;

    private void Awake()
    {
        gm = GameObject.Find("Gamemanager");
        if (gm == null)
        {
            Debug.Log("Player not linked to Game Manager");
        }
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Player_Interactable>().enabled = !inventoryOn;
        GetComponent<Player_Movement>().enabled = !inventoryOn;
        GetComponent<Player_Movement>().enabled = !targetOnInteractable;
    }//End Update

}
