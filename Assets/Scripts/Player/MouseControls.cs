using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseControls : MonoBehaviour
{

    public Texture2D cursorMove;
    public Texture2D cursorOnInteract;
    public Texture2D cursorOnDoor;
    public Texture2D cursorOnInventoryItem;

    private GameManager gm;

    public Vector2 target;

    public LayerMask interactableLayer;

    private void Start()
    {

        gm = GameManager.Instance;

    }

    // Update is called once per frame
    void Update()
    {

        target = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (!detectInteractable() && !gm.player.inventoryOn)
        {
            exitCursor();

        }


    }

    public void changeCursor(string type)
    {
        if(type == "interact")
        {

            Cursor.SetCursor(cursorOnInteract, target, CursorMode.Auto);

        }
        else if(type == "door")
        {

            Cursor.SetCursor(cursorOnDoor, target, CursorMode.Auto);

        }else if(type == "item")
        {

            Cursor.SetCursor(cursorOnInventoryItem, target, CursorMode.Auto);

        }
        

    }

    public void exitCursor()
    {

        Cursor.SetCursor(cursorMove, target, CursorMode.Auto);

    }

    public bool detectInteractable()
    {

        RaycastHit2D mouseDetect = Physics2D.Raycast(target, Vector2.zero, 0.1f, interactableLayer);

        if (mouseDetect.collider != null)
        {

            if (mouseDetect.collider.gameObject.GetComponent<Interactable>().inRange)
            {

                if (mouseDetect.collider.gameObject.GetComponent<Interactable>().interactable)
                {

                    mouseDetect.collider.gameObject.GetComponent<Interactable>().UpdateCursor();
                    return true;

                }
                else
                {

                    return false;

                }

            }
            else
            {
                return false;
            }

        }
        else
        {
            return false;
        }

    }

}
