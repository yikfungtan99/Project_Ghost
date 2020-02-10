using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost_Collide : MonoBehaviour
{
    public int talisman = 3;


    void OnTriggerEnter2D(Collider2D other)
    {

        if (GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().hidden== false)
        {

            if (other.CompareTag("Enemy"))
            {
                if (talisman > 0)
                {

                    if (GameObject.FindGameObjectWithTag("Enemy").GetComponent<MainGhost>().enabled == true)
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
