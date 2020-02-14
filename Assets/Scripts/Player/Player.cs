using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [HideInInspector]
    public GameObject gm;
    public Transform respawnPoint;
    public Inventory iv;
    public bool hidden;
    public bool playerFainted;
    private bool faintDebugMsg;
   
    //Player bool
    public bool inventoryOn = false;
    public bool haveLighter = false;

    private void Awake()
    {
        gm = GameObject.Find("GameManager");
        respawnPoint = GameObject.Find("Respawn Point").transform;
        iv = transform.GetChild(2).GetComponent<Inventory>();
        if (gm == null)
        {
            Debug.Log("Player not linked to Game Manager");
        }

        playerFainted = false;
        faintDebugMsg = false;
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Player_Interactable>().enabled = !inventoryOn;
        GetComponent<Player_Movement>().enabled = !inventoryOn;

        // GetComponent<Player_Movement>().enabled = !hidden;
        if(!playerFainted)
        {
            faintDebugMsg = false;
        }

        if(playerFainted && !faintDebugMsg)
        {
            faintDebugMsg = true;
            Debug.Log("Player has fainted!");
        }

        //! Temporary Debug tool: Instantly revive player character
        if(Input.GetKeyDown(KeyCode.Space))
        {
            playerFainted = false;
            faintDebugMsg = false;
            Debug.Log("Player revived");
        }

    }//End Update

   

    
}
