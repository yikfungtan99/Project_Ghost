using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb;
    private GameObject gm;

    private int direction = 1;

    //Movement variables
    [Header("Movement")]
    public float safeArea;

    private Vector2 targetPos;
    private Vector2 waypointPos;
    private bool enRoute = false;
    private bool haveWayPoint;

    public float moveSpeed;

    //sprit variables
    private bool sprint = false;
    [Header("Sprint")]
    [Space(10)]
    public float sprintMouseDelay = 0.25f;
    private float sprintMouseDelayTimer = 0;

    public float sprintSpeed;

    public float stamina;
    private float staminaRemaining;
    private bool sprintOnCooldown = false;

    [Header("Interactable")]
    [Space(20)]
    //interaction variables
    public LayerMask interactableLayer;
    public float interactableSize = 1;
    public float interactableOffset = 1;
    private bool targetOnInteractable = false;

    //animation && SPrite
    private SpriteRenderer sprite;
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.Find("Gamemanager");
        rb = this.gameObject.GetComponent<Rigidbody2D>();
        anim = transform.GetChild(0).GetComponent<Animator>();
        sprite = transform.GetChild(0).GetComponent<SpriteRenderer>();

        if (gm == null)
        {
            Debug.Log("Player not linked to Game Manager");
        }

        staminaRemaining = stamina;
    }

    // Update is called once per frame
    void Update()
    {
        //Get target position of the mouse
        targetPos = gm.transform.GetChild(0).gameObject.GetComponent<MouseControls>().target;

        //---------------------------------------------------------------Interact with Objects------------------------------------------------
        Collider2D[] interactable = Physics2D.OverlapBoxAll(new Vector2(transform.position.x, transform.position.y + interactableOffset), new Vector2(interactableSize, interactableSize), 0, interactableLayer);

        if (interactable != null)
        {
            for (int i = 0; i < interactable.Length; i++)
            {

                interactable[i].gameObject.GetComponent<Interactable>().inRange = true;

            }

        }

        //Clicking interactables
        RaycastHit2D mouseHit = Physics2D.Raycast(targetPos, Vector2.zero, 0.1f, interactableLayer);

        if (Input.GetMouseButtonDown(0))
        {
            if (mouseHit.collider != null)
            {
                if (mouseHit.collider.gameObject.GetComponent<Interactable>().inRange && mouseHit.collider.gameObject.GetComponent<Interactable>().interactable)
                {
                    mouseHit.collider.gameObject.GetComponent<Interactable>().Interact();
                    targetOnInteractable = true;
                }

            }
            else
            {
                if (targetOnInteractable)
                {
                    targetOnInteractable = false;
                }
            }
        }

        //---------------------------------------------------------------Movement--------------------------------------------------------------
        
        //Get target
        if (Input.GetMouseButton(0))
        {
            if (targetPos.x - transform.position.x < -safeArea || targetPos.x - transform.position.x > safeArea)
            {
                waypointPos = targetPos;
                haveWayPoint = true;

            }
            else
            {
                haveWayPoint = false;
            }
            
        }

        
        if (Input.GetMouseButton(0) && haveWayPoint)
        {
            //Sprint
            if (!sprint && !sprintOnCooldown)
            {
                if(sprintMouseDelayTimer < sprintMouseDelay)
                {

                    sprintMouseDelayTimer += Time.fixedDeltaTime;

                }
                else
                {
                    sprintMouseDelayTimer = 0;
                    sprint = true;
                }
            }

            if (!targetOnInteractable)
            {
                enRoute = true;
            }

            //Change direction
            if (waypointPos.x < transform.position.x)
            {
                sprite.flipX = true;
                direction = -1;

            }
            else
            {
                sprite.flipX = false;
                direction = 1;
            }
        }
        else
        {
            if (sprint) sprint = false;
            sprintMouseDelayTimer = 0;
        }

        if (sprint)
        {
            if (staminaRemaining > 0)
            {
                staminaRemaining -= Time.fixedDeltaTime;
            }
            else
            {
                staminaRemaining = 0;
                sprintOnCooldown = true;
                sprint = false;
            }
        }
        else
        {
            if (staminaRemaining < stamina)
            {
                staminaRemaining += Time.fixedDeltaTime;
            }
            else
            {
                staminaRemaining = stamina;
                sprintOnCooldown = false;
            }
        }

        //Move to target now
        if (enRoute && !targetOnInteractable)
        {
            anim.SetBool("Move", true);

            if(direction > 0 && transform.position.x > waypointPos.x)
            {
                enRoute = false;
                rb.velocity = new Vector2(0, rb.velocity.y);
            }

            if(direction < 0 && transform.position.x < waypointPos.x)
            {
                enRoute = false;
                rb.velocity = new Vector2(0, rb.velocity.y);
            }

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
            anim.SetBool("Move", false);
            rb.velocity = new Vector2(0, rb.velocity.y);

            if (sprint)
            {
                sprint = false;
            }
        }

    }//End Update

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1, 0, 0, 0.5f);
        Gizmos.DrawCube(new Vector2(transform.position.x, transform.position.y + interactableOffset), new Vector2(interactableSize, interactableSize));
    }

}
