using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Inventory : MonoBehaviour
{
    public GameObject inventory;
    public bool inventoryOn = false;

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
        if(!GameManager.gamePaused)
        {
            if (!GameManager.Instance.player.hidden)
            {
                if (Input.GetMouseButtonDown(1))
                {
                    ToggleInventory();
                    GetComponent<Player>().inventoryOn = inventoryOn;
                    GetComponent<Player>().gm.mouseControl.exitCursor();
                }

            }
           
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

    }
}
