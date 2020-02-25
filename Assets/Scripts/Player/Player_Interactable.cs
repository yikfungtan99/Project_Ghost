using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Interactable : MonoBehaviour
{
    private GameObject gm;

    private Vector2 targetPos;

    //interaction variables
    public LayerMask interactableLayer;
    public Vector2 interactableSize;
    public Vector2 interactableOffset;
    private int dir = 1;
    private bool targetOnInteractable = false;

    // Start is called before the first frame update
    void Start()
    {
        gm = GetComponent<Player>().gm;

        
    }

    // Update is called once per frame
    void Update()
    {

        //Get target position of the mouse
        targetPos = gm.GetComponent<MouseControls>().target;

        dir = transform.GetComponent<Player_Movement>().direction;

        //---------------------------------------------------------------Interact with Objects------------------------------------------------

        Collider2D[] interactable = Physics2D.OverlapBoxAll(new Vector2(transform.position.x + interactableOffset.x * dir, transform.position.y + interactableOffset.y), new Vector2(interactableSize.x, interactableSize.y), 0, interactableLayer);

        if (interactable != null)
        {
            for (int i = 0; i < interactable.Length; i++)
            {

                if (interactable[i].gameObject.GetComponent<Interactable>())
                {

                    interactable[i].gameObject.GetComponent<Interactable>().inRange = true;

                    if (transform.GetComponent<Player_Lighter>().lighterOn && !GetComponent<Player>().hidden)
                    {
                        interactable[i].gameObject.GetComponent<Interactable>().isSeen = true;
                    }

                }

            }

        }

    }

    public bool CheckForInteractables() //This will prevent any unnecessary movement such as when clicking on an interactable that is within range
    {
        bool onInteractable = false;

        if (Input.GetMouseButtonDown(0))
        {
            targetPos = gm.GetComponent<MouseControls>().target;
            RaycastHit2D mouseHit = Physics2D.Raycast(targetPos, Vector2.zero, 0.1f, interactableLayer);
            //Clicking interactables

            if (mouseHit.collider != null)
            {
                if (mouseHit.collider.gameObject.GetComponent<Interactable>().inRange)
                {
                    if (mouseHit.collider.gameObject.GetComponent<Interactable>().interactable)
                    {

                        if(mouseHit.collider.gameObject.tag == "Candle")
                        {
                            if (GetComponent<Player_Lighter>().lighterOn)
                            {
                                mouseHit.collider.gameObject.GetComponent<Interactable>().Interact();
                            }
                        }
                        else
                        {

                            mouseHit.collider.gameObject.GetComponent<Interactable>().Interact();

                        }
                        
                        onInteractable = true;
                    }

                }
                else
                {
                    if (targetOnInteractable)
                    {
                        onInteractable = false;
                    }

                }
            }
        }

        return onInteractable;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1, 0, 0, 0.5f);
        Gizmos.DrawCube(new Vector2(transform.position.x + interactableOffset.x*dir, transform.position.y + interactableOffset.y), new Vector2(interactableSize.x, interactableSize.y));
    }
}
