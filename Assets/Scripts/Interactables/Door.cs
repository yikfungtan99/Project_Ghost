using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    public bool isLocked = false;
    public bool isClosed = true;

    [Header("Orientation")]

    public bool LeftRight = false;
    public bool UpDown = false;

    private Interactable it;

    private RoomManager rm;

    public GameObject linkedDoor;
    private Transform doorSpawnPoint;

    private void Start()
    {
        rm = GameObject.Find("RoomManager").GetComponent<RoomManager>();
        it = GetComponent<Interactable>();

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
        Debug.Log("Enter Door");

        isClosed = false;

        if (LeftRight)
        {
            if (GameObject.Find("Player").transform.position.x > transform.position.x)
            {
                rm.SwitchRoom("Left");
                GameObject.Find("Player").transform.rotation = Quaternion.Euler(0, 180, 0);
            }
            else
            {
                rm.SwitchRoom("Right");
                GameObject.Find("Player").transform.rotation = Quaternion.Euler(0, 0, 0);
            }

        }
        else if (UpDown)
        {
            if (GameObject.Find("Player").transform.position.y > transform.position.y)
            {
                rm.SwitchRoom("Down");
            }
            else
            {
                rm.SwitchRoom("Up");
            }

        }

        GameObject.Find("Player").transform.position = doorSpawnPoint.position;

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
            it.interactable = true;
        }
        else
        {
            GetComponent<BoxCollider2D>().enabled = false;
            it.interactable = false;
        }
    }

    public void Interact()
    {
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

    private void LateUpdate()
    {
        it.inRange = false;
    }

}
