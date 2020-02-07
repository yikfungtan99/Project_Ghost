using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    public bool isLocked = false;
    public bool isClosed = true;

    public enum Orientation { LeftRight, Up, Down }

    public Orientation orientation;

    private Interactable it;

    private RoomManager rm;

    private void Start()
    {
        it = GetComponent<Interactable>();
        rm = GameObject.Find("RoomManager").GetComponent<RoomManager>();
    }

    public void Unlock()
    {
        isLocked = false;
        Debug.Log("I unlock the door");
    }

    public void Open()
    {
        isClosed = false;
        Debug.Log("I open the door");
        //Teleport();

        if(orientation == Orientation.LeftRight)
        {
            if(GameObject.Find("Player").transform.position.x > transform.position.x)
            {
                rm.SwitchRoom("Left");
            }
            else
            {
                rm.SwitchRoom("Right");
            }
            
        }
        else if(orientation == Orientation.Up)
        {
            rm.SwitchRoom("Up");

        }else if (orientation == Orientation.Down)
        {
            rm.SwitchRoom("Down");
        }
    }

    public void Closed()
    {
        isClosed = true;
        Debug.Log("I close the door");
    }

    private void Update()
    {
        if (it.inRange && isLocked)
        {
            transform.GetChild(0).gameObject.SetActive(true);
        }
        else
        {
            transform.GetChild(0).gameObject.SetActive(false);
        }
        
        if(it.inRange && !isLocked)
        {
            transform.GetChild(0).gameObject.SetActive(false);
        }

        if (isClosed)
        {
            GetComponent<BoxCollider2D>().enabled = true;
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

}
