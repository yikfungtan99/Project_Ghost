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
    public bool Up = false;
    public bool Down = false;

    private Interactable it;

    private RoomManager rm;

    public GameObject linkedDoor;
    public Transform doorSpawnPoint;

    private void Start()
    {
        rm = GameObject.Find("RoomManager").GetComponent<RoomManager>();
        it = GetComponent<Interactable>();
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
        else if (Up)
        {
            rm.SwitchRoom("Up");

        }
        else if (Down)
        {
            rm.SwitchRoom("Down");
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

        if (isLocked && isClosed){

            GetComponent<SpriteRenderer>().color = Color.red;

        }else if (!isLocked && isClosed){

            GetComponent<SpriteRenderer>().color = Color.yellow;

        }else if(!isLocked && !isClosed){

            GetComponent<SpriteRenderer>().color = Color.green;

        }

        it.inRange = false;

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

    private void Teleport()
    {

        if(SceneManager.GetActiveScene().buildIndex == 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        }
        
    }

    IEnumerator AutoClose(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        Closed();
    }

}
