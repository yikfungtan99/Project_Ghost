﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Inventory : MonoBehaviour
{
    private GameManager gm;
    public GameObject inventory;
    public bool inventoryOn = false;
    public bool firstTime = false;
    public bool secondTime = false;
    public GameObject hand;

    public float bagDelayTime = 0.5f;

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
                    if (firstTime)
                    {
                        if (!secondTime)
                        {
                            gm.TutorialNavi.UpdateBoard(3);
                        }
                        
                    }
                    

                    if (GameManager.Instance.inTutorial)
                    {
                        hand.gameObject.SetActive(firstTime);
                    }
                    
                }

            }
           
        }
    }

    private void ToggleInventory()
    {
        if (inventoryOn)
        {
            GameManager.Instance.playerMovement.enabled = true;
            GameManager.Instance.playerInteractable.enabled = true;
            UpdateAudio(2);
            inventoryOn = false;
            gm.mouseControl.exitCursor();
            inventory.transform.GetChild(0).gameObject.SetActive(inventoryOn);

            GameManager.Instance.player.inventoryOn = false;

        }
        else
        {
            GameManager.Instance.playerMovement.enabled = false;
            GameManager.Instance.playerInteractable.enabled = false;
            UpdateAudio(1);
            gm.mouseControl.changeCursor("item");
            gm.playerObject.transform.GetChild(0).GetComponent<Animator>().SetTrigger("Bag");
            
            StartCoroutine(BagDelay(bagDelayTime));


        }
    }

    IEnumerator BagDelay(float delay)
    {
        
        yield return new WaitForSeconds(delay);
        inventoryOn = true;
        inventory.transform.GetChild(0).gameObject.SetActive(true);
        GameManager.Instance.player.inventoryOn = true;
    }

    void UpdateAudio(int index)
    {
        switch(index)
        {
            case 1:
                gm.audioManager.PlayAudio("open bag");
                break;
            case 2:
                gm.audioManager.PlayAudio("close bag");
                break;
        }
    }
}
