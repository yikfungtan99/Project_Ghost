using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomChecker : MonoBehaviour
{
    private CarrotMain mainGhost;
    public Transform[] patrolSpots;
    public GameObject trigger;

    private void Awake()
    {

        mainGhost = GameManager.Instance.carrotMain;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Enemy"))
        {
            if(trigger != null)
            {
                trigger.GetComponent<Trigger>().isDisabled = true;
            }
            else
            {

                Debug.LogWarning("WARNING NO TRIGGER IN THIS ROOM IGNORING TRIGGER");

            }
            

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

    private void OnTriggerExit2D(Collider2D collision)
    {

        if (collision.CompareTag("Enemy"))
        {
            if (trigger)
            {
                trigger.GetComponent<Trigger>().isDisabled = false;
            }
            else
            {

                Debug.LogWarning("WARNING NO TRIGGER IN THIS ROOM IGNORING TRIGGER");

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
