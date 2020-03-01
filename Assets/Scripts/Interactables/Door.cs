using System.Collections;
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
                rm.SwitchRoom("Left");
                player.transform.rotation = Quaternion.Euler(0, 180, 0);
            }
            else
            {
                rm.SwitchRoom("Right");
                player.transform.rotation = Quaternion.Euler(0, 0, 0);
            }

        }
        else if (UpDown)
        {
            if (player.transform.position.y > doorSpawnPoint.position.y)
            {
                rm.SwitchRoom("Down");
            }
            else
            {
                rm.SwitchRoom("Up");
            }

        }

        player.transform.position = doorSpawnPoint.position;

        StartCoroutine(AutoClose(2));

    }

    public void Closed()
    {
        isClosed = true;
        Debug.Log("I close the door");
    }

    private void Update()
    {
        if (isClosed)
        {
            GetComponent<BoxCollider2D>().enabled = true;
        }
        else
        {
            GetComponent<BoxCollider2D>().enabled = false;
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

    IEnumerator AutoClose(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        Closed();
    }

}
