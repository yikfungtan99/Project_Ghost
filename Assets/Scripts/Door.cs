using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    public bool isLocked = false;
    public bool isClosed = true;
    private Interactable it;

    private void Awake()
    {
        it = GetComponent<Interactable>();
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
        Teleport();
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
