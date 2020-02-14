﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [HideInInspector]
    public GameObject gm;
    public Inventory iv;
    public bool hidden;
   
    //Player bool
    public bool inventoryOn = false;
    public bool lighterOn = false;
    public bool holdingLighter = false;

    private void Awake()
    {
        gm = GameObject.Find("GameManager");
        iv = transform.GetChild(2).GetComponent<Inventory>();
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
        lighterOn = GetComponent<Player_Lighter>().lighterOn;

        // GetComponent<Player_Movement>().enabled = !hidden;


    }//End Update

   

    
}
