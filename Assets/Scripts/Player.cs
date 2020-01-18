using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb;
    public GameObject gm;

    public float maxSpeed = 1;
    private int direction = 1;

    private Vector2 targetPos;
    private bool enRoute = false;

    private float moveSpeed = 5;
    public float acceleration;

    private bool sprint = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = this.gameObject.GetComponent<Rigidbody2D>();
    }

    private float lastClick = 0;
    private float downTime; //internal time from when the key is pressed
    private bool isHandled = false;
    /**
    void CheckForDoubleClick()
    {
        //start recording the time when a key is pressed and held.
        downTime = Time.time;
        isHandled = false;

        //look for a double click
        if (Time.time - lastClick < 0.3)
        {
            // do something
            sprint = true;
            Debug.Log("You double clicked the target.");
        }
        lastClick = Time.time;
    }
    **/
    // Update is called once per frame
    void Update()
    {
        //---------------------------------------------------------------Movement--------------------------------------------------------------
        /**
        if (Input.GetMouseButtonDown(0))
        {
            CheckForDoubleClick();
        }

        float newMspd = 0;

        if (Input.GetMouseButton(0))
        {

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

            if (sprint)
            {
                Debug.Log("I am Sprinting");
            }
            else
            {
                moveSpeed += acceleration * Time.deltaTime;

                newMspd = Mathf.Clamp(moveSpeed, 0, maxSpeed);


                rb.velocity = new Vector2(newMspd * direction, rb.velocity.y);
                //Debug.Log(newMspd);
            }

        }
        else
        {
            if (sprint) sprint = false;
            rb.velocity = new Vector2(0, rb.velocity.y);
            moveSpeed = 0;
        }
        **/

        //Get target
        if (Input.GetMouseButton(0))
        {
            targetPos = gm.transform.GetChild(0).gameObject.GetComponent<MouseControls>().target;
            enRoute = true;

            //Change direction
            if(targetPos.x < transform.position.x)
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
            if (targetPos.x - transform.position.x > 0.1 || targetPos.x - transform.position.x < -0.1)
            {

                rb.velocity = new Vector2(moveSpeed * direction, rb.velocity.y);

            }
            else
            {
                rb.velocity = new Vector2(0, rb.velocity.y);
                enRoute = false;
            }
        }


        /**
        //Move to target if holding mouse
        if (Input.GetMouseButton(0))
        {
            if (targetPos.x - transform.position.x > 0.1 || targetPos.x - transform.position.x < -0.1)
            {
                if (enRoute)
                {
                    rb.velocity = new Vector2(moveSpeed * direction, rb.velocity.y);
                }

            }
            else
            {
                enRoute = false;
            }

            if (!enRoute)
            {
                rb.velocity = new Vector2(0, rb.velocity.y);
            }
        }
        else
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
        **/

    }

}
