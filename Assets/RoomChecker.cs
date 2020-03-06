using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomChecker : MonoBehaviour
{
    private CarrotMain mainGhost;
    public Transform[] patrolSpots;

    private void Awake()
    {

        mainGhost = GameManager.Instance.carrotMain;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        Debug.Log("Something has entered");

        if (collision.CompareTag("Enemy"))
        {
            Debug.Log("Detected Ghost");
            for (int i = 0; i < patrolSpots.Length; i++)
            {
                if (mainGhost)
                {
                    if (i < 2)
                    {
                        mainGhost.patrolSpots[i] = patrolSpots[i];
                    }

                }
                else
                {

                    Debug.Log("Can't Find MainGhost");

                }


            }

        }

    }


    //private void OnTriggerStay2D(Collider2D collision)
    //{

    //    Debug.Log("Something has entered");

    //    if (collision.CompareTag("Enemy"))
    //    {
    //        Debug.Log("Detected Ghost");
    //        for (int i = 0; i < patrolSpots.Length; i++)
    //        {
    //            if (mainGhost)
    //            {
    //                if (i < 2)
    //                {
    //                    mainGhost.patrolSpots[i] = patrolSpots[i];
    //                }
                    
    //            }
    //            else
    //            {

    //                Debug.Log("Can't Find MainGhost");

    //            }
                

    //        }

    //    }

    //}
}
