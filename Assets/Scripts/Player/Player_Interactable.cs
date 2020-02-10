using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Interactable : MonoBehaviour
{
    private GameObject gm;

    private Vector2 targetPos;

    //interaction variables
    public LayerMask interactableLayer;
    public float interactableSize = 1;
    public float interactableOffset = 1;
    private bool targetOnInteractable = false;

    [HideInInspector]
    public bool showMonolog = false;
    public float showMonologTimer;
    private float showMonologTimerCounter;

    // Start is called before the first frame update
    void Start()
    {
        gm = GetComponent<Player>().gm;

        showMonologTimerCounter = showMonologTimer;
    }

    // Update is called once per frame
    void Update()
    {
        transform.GetChild(1).gameObject.SetActive(showMonolog);

        if (showMonolog && showMonologTimerCounter > 0)
        {
            showMonologTimerCounter -= Time.deltaTime;
        }
        else
        {
            showMonolog = false;
            showMonologTimerCounter = showMonologTimer;
        }

        //Get target position of the mouse
        targetPos = gm.GetComponent<MouseControls>().target;

        //---------------------------------------------------------------Interact with Objects------------------------------------------------
        Collider2D[] interactable = Physics2D.OverlapBoxAll(new Vector2(transform.position.x, transform.position.y + interactableOffset), new Vector2(interactableSize, interactableSize), 0, interactableLayer);

        if (interactable != null)
        {
            for (int i = 0; i < interactable.Length; i++)
            {

                if (interactable[i].gameObject.GetComponent<Interactable>())
                {

                    interactable[i].gameObject.GetComponent<Interactable>().inRange = true;

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
                        Debug.Log(targetPos);
                        mouseHit.collider.gameObject.GetComponent<Interactable>().Interact();
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
        Gizmos.DrawCube(new Vector2(transform.position.x, transform.position.y + interactableOffset), new Vector2(interactableSize, interactableSize));
    }
}
