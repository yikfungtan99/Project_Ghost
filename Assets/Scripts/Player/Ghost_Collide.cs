using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost_Collide : MonoBehaviour
{

    GameManager gm;

    //public int talisman = 3;
    public int TalismanStunTime;
    public int DiscoverPlayerStunTime;

    private void Start()
    {

        gm = GameManager.Instance;

    }

    void OnTriggerStay2D(Collider2D collision)
    {

        
            
         if (collision.CompareTag("Enemy"))
         {
            if (gm.player.hidden == false)
            {
                if (gm.holdPanel.transform.childCount != 0 && gm.holdPanel.transform.GetChild(0).GetComponent<Item_Inventory>().itemName == "talisman")
                {


                    collision.gameObject.GetComponent<CarrotMain>().anima.SetBool("isIdle", true);
                    collision.gameObject.GetComponent<CarrotMain>().anima.SetBool("isPatrol", false);
                    collision.gameObject.GetComponent<CarrotMain>().anima.SetBool("isLight", false);
                    collision.gameObject.GetComponent<CarrotMain>().anima.SetBool("isChase", false);
                    Destroy(GameObject.Find("Hold Panel").transform.GetChild(0).gameObject);
                    StartCoroutine(EnemyWake(TalismanStunTime));



                }
                else
                {

                    // Destroy(gm.playerObject);
                    
                    gm.playerObject.GetComponent<Transform>().position = gm.playerMovement.savedLocation.position;
                    
                    // gm.deathScreenObject.transform.GetChild(0).gameObject.SetActive(true);


                }

            }
            if(gm.player.hidden == true && collision.GetComponent<CarrotMain>().anima.GetBool("isChase")==true)
            {
                
                collision.gameObject.GetComponent<CarrotMain>().anima.SetBool("isIdle", true);
                collision.gameObject.GetComponent<CarrotMain>().anima.SetBool("isPatrol", false);
                collision.gameObject.GetComponent<CarrotMain>().anima.SetBool("isLight", false);
                collision.gameObject.GetComponent<CarrotMain>().anima.SetBool("isChase", false);
                foreach (GameObject fooObj in GameObject.FindGameObjectsWithTag("Hiding_Spot"))
                {
                    fooObj.GetComponent<Hidable>().Unhide();
                    
                }
                gm.playerObject.GetComponent<Player_Movement>().enabled = false;
                StartCoroutine(EnemyWake(DiscoverPlayerStunTime));
            }

         }
           

        
        IEnumerator EnemyWake(int stunTime)
        {
            //This is a coroutine
            yield return new WaitForSeconds(stunTime);    //Wait one frame
                                                    /* GameObject.FindGameObjectWithTag("Enemy").GetComponent<MainGhost>().enabled = true;
                                                     GameObject.FindGameObjectWithTag("Enemy").GetComponent<CapsuleCollider2D>().enabled = true;*/
            collision.gameObject.GetComponent<CarrotMain>().anima.SetBool("isPatrol", true);
            collision.gameObject.GetComponent<CarrotMain>().anima.SetBool("isChase", false);
            collision.gameObject.GetComponent<CarrotMain>().anima.SetBool("isIdle", false);
            collision.gameObject.GetComponent<CarrotMain>().anima.SetBool("isLight", false);

        }


    }
    
}
