using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseControls : MonoBehaviour
{

    public Texture2D cursorMove;
    public Texture2D cursorOnInteract;
    public Texture2D cursorOnDoor;
    public Texture2D cursorOnInventoryItem;
    public Texture2D cursorDrag;
    public Texture2D cursorHide;

    private GameManager gm;
    private Camera cam;

    public Vector2 target;

    public LayerMask interactableLayer;

    private void Start()
    {

        gm = GameManager.Instance;
        cam = Camera.main;

    }

    // Update is called once per frame
    void Update()
    {

        Vector3 mousePos = Input.mousePosition;

        target = cam.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, cam.transform.position.z));

       // Debug.Log(target);


        if (!detectInteractable() && !gm.player.inventoryOn)
        {
            exitCursor();

        }


    }

    public void changeCursor(string type)
    {

        Texture2D cursorToSwitch = null;

        switch (type)
        {

            case "interact":

                cursorToSwitch = cursorOnInteract;
                break;

            case "door":

                cursorToSwitch = cursorOnDoor;
                break;

            case "item":

                cursorToSwitch = cursorOnInventoryItem;
                break;

            case "grab":
                cursorToSwitch = cursorDrag;
                break;

            case "hide":
                cursorToSwitch = cursorHide;
                break;

        }

        Cursor.SetCursor(cursorToSwitch, new Vector2(cursorToSwitch.width/2, cursorToSwitch.height / 2), CursorMode.Auto);

    }

    public void exitCursor()
    {

        Cursor.SetCursor(cursorMove, new Vector2(0, 0), CursorMode.Auto);

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
