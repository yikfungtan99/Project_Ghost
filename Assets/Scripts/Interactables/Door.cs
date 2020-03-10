﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : Interactable
{
    private RoomManager rm;
    private GameObject player;

    public bool isLocked = false;
    public bool isClosed = true;

    [Header("Orientation")]

    public bool LeftRight = false;
    public bool UpDown = false;

    public GameObject linkedDoor;
    private Transform doorSpawnPoint;

    private void Start()
    {

        if (gm)
        {

            rm = gm.roomManager;

        }
        else
        {

            Debug.Log("Cant find gameManager");

        }

        

        player = gm.playerObject;

        if(linkedDoor == null)
        {

            Debug.LogError(gameObject.name + " have missing door connection!!!");

        }
        else
        {
            doorSpawnPoint = linkedDoor.transform.GetChild(0);
        }
    }

    public override void UpdateCursor()
    {
        gm.mouseControl.changeCursor("door");
    }

    public void Unlock()
    {
        isLocked = false;
    }

    public void Open()
    {

        isClosed = false;

        if (LeftRight)
        {
            if (player.transform.position.x > doorSpawnPoint.position.x)
            {
                rm.SwitchRoom("Left", transform);
                player.transform.rotation = Quaternion.Euler(0, 180, 0);
            }
            else
            {
                rm.SwitchRoom("Right", transform);
                player.transform.rotation = Quaternion.Euler(0, 0, 0);
            }

        }
        else if (UpDown)
        {
            if (player.transform.position.y > doorSpawnPoint.position.y)
            {
                rm.SwitchRoom("Down", transform);
            }
            else
            {
                rm.SwitchRoom("Up", transform);
            }

        }

        player.transform.position = doorSpawnPoint.position;

        StartCoroutine(AutoClose(1.0f));

    }

    public void Closed()
    {

        isClosed = true;

    }

    private void Update()
    {
        /*
        if (isClosed)
        {
            GetComponent<BoxCollider2D>().enabled = true;
        }
        else
        {
            GetComponent<BoxCollider2D>().enabled = false;
        }
        */
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        //! For left right doors
        if(col.gameObject == gm.playerObject)
        {
            Interact();
        }

        if (col.gameObject.CompareTag("Enemy"))
        {

            if (col.gameObject.GetComponent<CarrotMain>())
            {
                if (col.gameObject.GetComponent<CarrotMain>().canChangeRoom)
                {

                    EnemyOpen(col.gameObject);

                }
                
            }
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {

            if (collision.gameObject.GetComponent<CarrotMain>())
            {
                if (collision.gameObject.GetComponent<CarrotMain>().canChangeRoom)
                {

                    EnemyOpen(collision.gameObject);

                }

            }
        }

    }

    public override void Interact()
    {
        base.Interact();

        if (isLocked)
        {
            Unlock();
        }
        else if(isClosed)
        {
            Open();
        }
    }

    public void EnemyOpen(GameObject enemy)
    {

        enemy.transform.position = doorSpawnPoint.position;
        enemy.GetComponent<CarrotMain>().canChangeRoom = false;

    }

    IEnumerator AutoClose(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        Closed();
    }

}
