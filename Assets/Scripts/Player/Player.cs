using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject gm;

    //Inventory
    public bool inventoryOn = false;

    // Start is called before the first frame update

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
    }//End Update

}
