using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{

    public bool isLocked = false;
    public bool isClosed = true;
    public bool inRange = false;

    public void Unlock()
    {
        isLocked = false;
        Debug.Log("I unlock the door");
    }

    public void Open()
    {
        isClosed = false;
        Debug.Log("I open the door");
    }

    public void Closed()
    {
        isClosed = true;
        Debug.Log("I close the door");
    }

    private void Update()
    {
        if (inRange && isLocked)
        {
            transform.GetChild(0).gameObject.SetActive(true);
        }
        
        if(inRange && !isLocked)
        {
            transform.GetChild(0).gameObject.SetActive(false);
        }

        inRange = false;

        if (isClosed)
        {
            GetComponent<BoxCollider2D>().isTrigger = false;
        }
        else
        {
            GetComponent<BoxCollider2D>().isTrigger = true;
        }

        if (isLocked && isClosed){

            GetComponent<SpriteRenderer>().color = Color.red;

        }else if (!isLocked && isClosed){

            GetComponent<SpriteRenderer>().color = Color.yellow;

        }else if(!isLocked && !isClosed){

            GetComponent<SpriteRenderer>().color = Color.green;

        }
        
    }
}
