using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [HideInInspector]
    public GameManager gm;
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

    public GameObject curHidable;

    public RoomChecker curRoom;

    public float warningTime = 3f;

    public bool isDead = false;

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

        //GetComponent<Player_Movement>().enabled = !hidden;
        if (!playerFainted)
        {
            faintDebugMsg = false;
        }

        if(playerFainted && !faintDebugMsg)
        {
            faintDebugMsg = true;
            Debug.Log("Player has fainted!");
        }

        UpdateHidingHeartbeatPitch();

        //! Temporary Debug tool: Instantly revive player character
        /*
        if(Input.GetKeyDown(KeyCode.Space))
        {
            playerFainted = false;
            faintDebugMsg = false;
            Debug.Log("Player revived");
        }*/

    }//End Update

    public void Death()
    {

        isDead = true;
        gameObject.SetActive(false);

        int child = GameObject.Find("DeathCanvas").transform.childCount;
        for (int i = 0; i < child; i++)
        {
            GameObject.Find("DeathCanvas").transform.GetChild(i).gameObject.SetActive(true);
        }

    }

    void UpdateHidingHeartbeatPitch()
    {
        if (hidden)
        {
            float newPitch;

            if (gm.playerMovement.distancePandG > 0 && gm.playerMovement.distancePandG <= 10)
            {
                newPitch = (1.0f / gm.playerMovement.distancePandG) + 1.0f;

                if (newPitch >= 2.0f)
                {
                    newPitch = 2.0f;
                    gm.audioManager.UpdateAudioPitch("heart beating", newPitch);
                    return;
                }

                gm.audioManager.UpdateAudioPitch("heart beating", newPitch);
            }
            else if (gm.playerMovement.distancePandG == 0)
            {
                newPitch = 2.0f;
                gm.audioManager.UpdateAudioPitch("heart beating", newPitch);
            }
            else if(gm.playerMovement.distancePandG > 10)
            {
                gm.audioManager.UpdateAudioPitch("heart beating", 1f);
            }
        }
        else if (!hidden)
        {
            gm.audioManager.UpdateAudioPitch("heart beating", 1f);
        }
    }
}
