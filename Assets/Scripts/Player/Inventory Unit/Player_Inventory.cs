using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Inventory : MonoBehaviour
{
    private GameManager gm;
    public GameObject inventory;
    public bool inventoryOn = false;

    // Start is called before the first frame update
    void Start()
    {
        gm = GetComponent<Player>().gm;
        inventory = gm.inventory.gameObject;

        if (inventory == null)
        {
            Debug.Log("Inventory not found");
        }

    }

    // Update is called once per frame
    void Update()
    {
        //Inventory
        if(!GameManager.Instance.gamePaused)
        {
            if (!GameManager.Instance.player.hidden)
            {
                if (Input.GetMouseButtonDown(1))
                {
                    ToggleInventory();
                    GameManager.Instance.player.inventoryOn = inventoryOn;
                }

            }
           
        }
    }

    private void ToggleInventory()
    {
        if (inventoryOn)
        {
            inventoryOn = false;
            gm.mouseControl.exitCursor();
        }
        else
        {
            inventoryOn = true;
            gm.mouseControl.changeCursor("item");
        }

        inventory.transform.GetChild(0).gameObject.SetActive(inventoryOn);

    }
}
