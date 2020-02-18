using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost_Collide : MonoBehaviour
{
    //public int talisman = 3;


    void OnTriggerEnter2D(Collider2D other)
    {

        if (GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().hidden== false)
        {
            
            if (other.CompareTag("Enemy"))
            {
                if(GameObject.Find("Hold Panel").transform.childCount != 0 && GameObject.Find("Hold Panel").transform.GetChild(0).GetComponent<Item_Inventory>().itemName == "talisman") 
                {

                    if (GameObject.FindGameObjectWithTag("Enemy").GetComponent<MainGhost>().enabled == true)
                    {
                        GameObject.FindGameObjectWithTag("Enemy").GetComponent<MainGhost>().enabled = false;
                        GameObject.FindGameObjectWithTag("Enemy").GetComponent<CapsuleCollider2D>().enabled = false;
                        Destroy(GameObject.Find("Hold Panel").transform.GetChild(0).gameObject);
                        StartCoroutine(EnemyWake());
                            
                    }

                }
                else
                {

                    /*GameObject.FindGameObjectWithTag("Player").transform.GetChild(0).GetComponent<Renderer>().enabled = false;
                    GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().enabled = false;*/
                    Destroy(GameObject.FindGameObjectWithTag("Player"));

                    int child = GameObject.Find("DeathCanvas").transform.childCount;
                    for(int i=0;i<child;i++)
                    {
                        GameObject.Find("DeathCanvas").transform.GetChild(i).gameObject.SetActive(true);
                    }
                    

                }
               /* if (talisman > 0)
                {

                    if (GameObject.FindGameObjectWithTag("Enemy").GetComponent<MainGhost>().enabled == true)
                    {
                        GameObject.FindGameObjectWithTag("Enemy").GetComponent<MainGhost>().enabled = false;
                        StartCoroutine(EnemyWake());
                        talisman--;
                    }
                    Debug.Log(talisman);
                }*/



            }
           

        }


    }
    IEnumerator EnemyWake()
    {
        //This is a coroutine
        yield return new WaitForSeconds(10);    //Wait one frame
        GameObject.FindGameObjectWithTag("Enemy").GetComponent<MainGhost>().enabled = true;
        GameObject.FindGameObjectWithTag("Enemy").GetComponent<CapsuleCollider2D>().enabled = true;

    }
}
