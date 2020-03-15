using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomChecker : MonoBehaviour
{
    private CarrotMain mainGhost;
    public Transform[] patrolSpots;
    public GameObject trigger;

    public bool ghostInRoom = false;
    public bool playerInRoom = false;

    public Transform staySpots;

    private void Awake()
    {

        mainGhost = GameManager.Instance.carrotMain;

    }

    private void Update()
    {

        if (trigger)
        {

            if(ghostInRoom && playerInRoom)
            {

                trigger.GetComponent<Trigger>().canAutoRecover = false;

            }
            else
            {

                trigger.GetComponent<Trigger>().canAutoRecover = true;

            }

        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Enemy"))
        {

            mainGhost.curRoom = this;

            if (trigger != null)
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

        if (collision.CompareTag("Player"))
        {

            GameManager.Instance.player.curRoom = this;

        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {

        if (collision.CompareTag("Enemy"))
        {

            ghostInRoom = false;

            if (trigger)
            {
                trigger.GetComponent<Trigger>().isDisabled = false;
            }
            else
            {

                Debug.LogWarning("WARNING NO TRIGGER IN THIS ROOM IGNORING TRIGGER");

            }

        }

        if (collision.CompareTag("Player"))
        {

            playerInRoom = false;

        }

    }

    private void OnTriggerStay2D(Collider2D collision)
    {

        if (collision.CompareTag("Enemy"))
        {

            ghostInRoom = true;


        }

        if (collision.CompareTag("Player"))
        {

            playerInRoom = true;
           

        }

    }
}
