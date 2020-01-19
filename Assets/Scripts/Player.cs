using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb;
    private GameObject gm;

    
    private int direction = 1;

    //Movement variables
    private Vector2 targetPos;
    private bool enRoute = false;

    public float moveSpeed;
    public float sprintSpeed;

    private bool sprint = false;
    public float sprintAvailable;
    private float sprintRemaining;

    //interaction variables
    public LayerMask interactableLayer;
    public float interactableSize = 1;
    public float interactableOffset = 1;

    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.Find("Gamemanager");
        rb = this.gameObject.GetComponent<Rigidbody2D>();
        sprintRemaining = sprintAvailable;

        if(gm == null)
        {
            Debug.Log("Player not linked to Game Manager");
        }
    }

    float clicked = 0;
    float clicktime = 0;
    float clickdelay = 0.7f;

    bool DoubleClick()
    {

        if (Input.GetMouseButtonDown(0))
        {
            clicked++;
            if (clicked == 1) clicktime = Time.time;
        }
        if (clicked > 1 && Time.time - clicktime < clickdelay)
        {
            clicked = 0;
            clicktime = 0;
            return true;
        }
        else if (clicked > 2 || Time.time - clicktime > 1) clicked = 0;
        return false;
    }

    // Update is called once per frame
    void Update()
    {
        //---------------------------------------------------------------Movement--------------------------------------------------------------
        //Sprint
        if (DoubleClick())
        {
            sprint = true;
        }
        
        //Get target
        if (Input.GetMouseButton(0))
        {
            enRoute = true;

            targetPos = gm.transform.GetChild(0).gameObject.GetComponent<MouseControls>().target;

            //Change direction
            if (targetPos.x < transform.position.x)
            {

                direction = -1;

            }
            else
            {
                direction = 1;
            }
        }

        //Move to target now
        if (enRoute)
        {
            if (targetPos.x - transform.position.x > 0.5 || targetPos.x - transform.position.x < -0.5)
            {
                if (sprint)
                {
                    rb.velocity = new Vector2(sprintSpeed * 100 * direction * Time.fixedDeltaTime, rb.velocity.y);
                }
                else
                {
                    rb.velocity = new Vector2(moveSpeed * 100 * direction * Time.fixedDeltaTime, rb.velocity.y);
                }

            }
            else
            {
                rb.velocity = new Vector2(0, rb.velocity.y);
                enRoute = false;

                if (sprint)
                {
                    sprint = false;
                }
            }
        }

        //Sprint cooldown
        if (sprint)
        {
            if(sprintRemaining > 0)
            {
                sprintRemaining -= Time.fixedDeltaTime;
            }
            else
            {
                sprint = false;
            }
        }
        else
        {

            if(sprintRemaining < sprintAvailable)
            {
                sprintRemaining += Time.fixedDeltaTime;
            }
            
            if(sprintRemaining >= sprintAvailable)
            {
                sprintRemaining = sprintAvailable;
            }

        }

        //---------------------------------------------------------------Interact with Objects------------------------------------------------
        Collider2D[] interactable = Physics2D.OverlapBoxAll(new Vector2(transform.position.x, transform.position.y + interactableOffset), new Vector2(interactableSize, interactableSize), 0, interactableLayer);

        if(interactable != null)
        {

            for (int i = 0; i < interactable.Length; i++)
            {
                if (interactable[i].gameObject.tag == "Door")
                {
                    interactable[i].gameObject.GetComponent<Door>().inRange = true;
                }
            }
            
        }

        //Clicking interactables

        RaycastHit2D mouseHit = Physics2D.Raycast(targetPos, Vector2.zero, 0.1f, interactableLayer);

        if (Input.GetMouseButtonDown(0))
        {
            if (mouseHit.collider != null)
            {
                if(mouseHit.collider.gameObject.tag == "Door")
                {
                    Door door = mouseHit.collider.gameObject.GetComponent<Door>();

                    if (door.inRange)
                    {
                        Debug.Log("I am in range of the door");
                        if (door.isLocked)
                        {
                            door.Unlock();
                        }
                        else
                        {
                            if (door.isClosed)
                            {
                                door.Open();
                            }
                        }
                    }
                }
            }
        }

    }//End Update
   
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1, 0, 0, 0.5f);
        Gizmos.DrawCube(new Vector2(transform.position.x, transform.position.y + interactableOffset), new Vector2(interactableSize, interactableSize));
    }

    

}
