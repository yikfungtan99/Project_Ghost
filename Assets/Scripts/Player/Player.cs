using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject gm;
    public bool hidden;
    public int talisman=3;
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
       // GetComponent<Player_Movement>().enabled = !hidden;
    }//End Update

    void OnTriggerEnter2D(Collider2D other)
    {

        if (hidden == false)
        {


           
            if (other.CompareTag("Enemy"))
            {
                if(talisman>0)
                {
                    
                    if(GameObject.FindGameObjectWithTag("Enemy").GetComponent<MainGhost>().enabled==true)
                    {
                        GameObject.FindGameObjectWithTag("Enemy").GetComponent<MainGhost>().enabled = false;
                        StartCoroutine(EnemyWake());
                        talisman--;
                    }
                    Debug.Log(talisman);
                }
                


            }


        }




    }
    IEnumerator EnemyWake()
    {
        //This is a coroutine
       

        yield return new WaitForSeconds(10);    //Wait one frame
        GameObject.FindGameObjectWithTag("Enemy").GetComponent<MainGhost>().enabled = true;

    }
}
