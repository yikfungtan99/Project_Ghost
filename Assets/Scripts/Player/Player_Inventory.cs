﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Inventory : MonoBehaviour
{ 
    private GameObject inventory;
    private bool inventoryOn = false;

    // Start is called before the first frame update
    void Start()
    {
        inventory = transform.GetChild(2).gameObject;

        if (inventory == null)
        {
            Debug.Log("Inventory not found");
        }

    }

    // Update is called once per frame
    void Update()
    {
        //Inventory
        if (Input.GetMouseButtonDown(1))
        {
            ToggleInventory();
            GetComponent<Player>().inventoryOn = inventoryOn;
        }
    }

    private void ToggleInventory()
    {
        if (inventoryOn)
        {
            inventoryOn = false;
        }
        else
        {
            inventoryOn = true;
        }

        inventory.transform.GetChild(0).gameObject.SetActive(inventoryOn);
        inventory.transform.GetChild(1).gameObject.SetActive(inventoryOn);
    }
}
