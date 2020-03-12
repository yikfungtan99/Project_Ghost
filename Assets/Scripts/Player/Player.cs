using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [HideInInspector]
    public GameManager gm;
    public Transform respawnPoint;
    public Inventory iv;
    public bool hidden;
    public bool playerFainted;
    private bool faintDebugMsg;
   
    //Player bool
    public bool inventoryOn = false;
    public bool haveLighter = false;
    public bool lighterOn = false;
    public GameObject WarningLeft;
    public GameObject WarningRight;

    private void Awake()
    {
        gm = GameManager.Instance;
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
        /*GetComponent<Player_Interactable>().enabled = !inventoryOn;
        GetComponent<Player_Movement>().enabled = !inventoryOn;*/

        //! this function replaces the above codes, also fixes a refresh bug where Player Update() does not call
       gm.RefreshPlayerUnpausedState();

        // GetComponent<Player_Movement>().enabled = !hidden;
        if (!playerFainted)
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
