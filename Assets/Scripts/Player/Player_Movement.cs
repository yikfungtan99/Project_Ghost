﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    private GameObject gm;
    private Rigidbody2D rb;

    private int direction = 1;

    [Header("Movement")]
    //Movement variables
    public float safeArea;
    private Vector2 targetPos;
    private Vector2 waypointPos;
    private bool enRoute = false;
    private bool haveWayPoint;

    public float moveSpeed;

    //Sprint variables
    private bool sprint = false;
    [Header("Sprint")]
    [Space(10)]
    public float sprintMouseDelay = 0.25f;
    private float sprintMouseDelayTimer = 0;

    public float sprintSpeed;

    public float stamina;
    private float staminaRemaining;
    private bool sprintOnCooldown = false;

    private SpriteRenderer sprite;
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        gm = GetComponent<Player>().gm;
        rb = this.gameObject.GetComponent<Rigidbody2D>();
        anim = transform.GetChild(0).GetComponent<Animator>();
        sprite = transform.GetChild(0).GetComponent<SpriteRenderer>();

        staminaRemaining = stamina;
    }

    // Update is called once per frame
    void Update()
    {
        //Get target position of the mouse
        targetPos = gm.transform.GetChild(0).gameObject.GetComponent<MouseControls>().target;

        //Get waypoint
        if (Input.GetMouseButton(0))
        {
            if (targetPos.x - transform.position.x < -safeArea || targetPos.x - transform.position.x > safeArea)
            {
                waypointPos = targetPos;
                haveWayPoint = true;
                enRoute = true;

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
                if (sprintMouseDelayTimer < sprintMouseDelay)
                {

                    sprintMouseDelayTimer += Time.fixedDeltaTime;

                }
                else
                {
                    sprintMouseDelayTimer = 0;
                    sprint = true;
                }
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
            sprite.color = Color.red;
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
            sprite.color = Color.white;

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
        if (enRoute)
        {
            if (direction > 0 && transform.position.x > waypointPos.x)
            {
                enRoute = false;    
                rb.velocity = new Vector2(0, rb.velocity.y);
            }

            if (direction < 0 && transform.position.x < waypointPos.x)
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
            rb.velocity = new Vector2(0, rb.velocity.y);

            if (sprint)
            {
                sprint = false;
            }
        }

        //Animation
        if (rb.velocity.x > 1 || rb.velocity.x < -1)
        {
            anim.SetBool("Move", true);
        }
        else
        {
            anim.SetBool("Move", false);
        }
    }

    private void OnDisable()
    {
        haveWayPoint = false;
        enRoute = false;
        rb.velocity = new Vector2(0, rb.velocity.y);

        anim.SetBool("Move", false);

    }
}
