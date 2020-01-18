using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb;
    public GameObject gm;

    private int direction = 1;

    private Vector2 targetPos;
    private bool enRoute = false;

    public float moveSpeed;
    public float sprintSpeed;

    private bool sprint = false;
    public float sprintAvailable;
    private float sprintRemaining;

    // Start is called before the first frame update
    void Start()
    {
        rb = this.gameObject.GetComponent<Rigidbody2D>();
        sprintRemaining = sprintAvailable;
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

    }

}
