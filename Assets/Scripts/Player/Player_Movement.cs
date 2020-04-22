using System.Collections;
using System.Collections.Generic;
using UnityEngine.Experimental.Rendering.LWRP;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Player_Movement : MonoBehaviour
{
   
    private GameManager gm;
    private Rigidbody2D rb;

    public int direction = 1;

    [Header("Movement")]
    //Movement variables
    public float safeArea;
    private Vector2 targetPos;
    private Vector2 waypointPos;
    public bool enRoute = false;
    public bool haveWayPoint;

    public float moveSpeed;

    public float distancePandG;
	
	public bool stopMoving = false;
	
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

    bool randomized = false;

    bool pressedButton = false;
	
	[Header("Saving")]
    public Transform savedLocation;
    public int saveStationNum;
    public Transform defaultSpawn;
	
	[Header("Unhiding Delay")]
	[SerializeField, ReadOnly] float holdStartTime;
	[SerializeField] float unhideTime;
	
    void Start()
    {
		holdStartTime = 0f;
        savedLocation = defaultSpawn;
        gm = GetComponent<Player>().gm;
        rb = this.gameObject.GetComponent<Rigidbody2D>();
        anim = transform.GetChild(0).GetComponent<Animator>();
        sprite = transform.GetChild(0).GetComponent<SpriteRenderer>();

        staminaRemaining = stamina;
    }

    // Update is called once per frame
    void Update()
    {
        if(gm.ghostMain != null)
		{
			distancePandG = Mathf.Abs(this.transform.position.x - gm.ghostMain.transform.position.x);
		}
        //Debug.Log("Distnace of player and ghost"+distancePandG);

        if (!GameManager.Instance.gamePaused)
        {
            if (pressedButton)
            {
                pressedButton = false;
            }
            else
            {
                //Get waypoint
                if (Input.GetMouseButtonDown(0))
                {
                    if (EventSystem.current.IsPointerOverGameObject())
                        return;

                    if (gm.player.hidden)
                    {
						//! This still set a start time since the mouse button was first held down
						if(Input.GetMouseButtonDown(0))
						{
							Debug.LogWarning("Mouse held down");
							holdStartTime = Time.time;
						}
                        //gm.player.curHidable.GetComponent<Hidable>().Unhide();
                    }
                    else
                    {
						if(stopMoving)
						{
							return;
						}
						
                        if (GetComponentInChildren<Player_Interactable>().CheckForInteractables())
                        {
                            haveWayPoint = false;
                            enRoute = false;
                            sprint = false;
                        }
                        else
                        {
                            //Get target position of the mouse
                            targetPos = gm.mouseControl.target;

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

                            if (haveWayPoint)
                            {
                           
                                // contexual running
                                if (GameObject.FindGameObjectWithTag("Enemy"))
                                {
                                    if (GameObject.FindGameObjectWithTag("Enemy").GetComponent<CarrotMain>().anima.GetBool("isChase") == true)
                                    {
                                        staminaRemaining = stamina;
                                        sprint = true;

                                    }

                                }

                                //Change direction
                                if (waypointPos.x < transform.position.x)
                                {
                                    transform.rotation = Quaternion.Euler(0, 180, 0);
                                    direction = -1;

                                }
                                else
                                {
                                    transform.rotation = Quaternion.Euler(0, 0, 0);
                                    direction = 1;
                                }
                            }
                            else
                            {
                                if (sprint) sprint = false;
                                sprintMouseDelayTimer = 0;
                            }
                        }
                    }
                }
				
				//! This will continue running as long as mouse button is down (update visuals)
				if(Input.GetMouseButton(0))
				{
					if(gm.player.hidden)
					{
						if(holdStartTime == 0)
						{
							return;
						}
						
						float holdDownTime = Time.time - holdStartTime;
						Debug.LogWarning("hold down time: " + holdDownTime);
						Debug.LogWarning("hold start time: " + holdStartTime);
						
						//! --> Function: Show visual indicator for timer
						if(gm.unhidingUICanvas.transform.GetChild(0).GetComponent<Image>().enabled == false)
						{
							gm.unhidingUICanvas.transform.GetChild(0).GetComponent<Image>().enabled = true;
						}
						gm.unhidingUICanvas.transform.GetChild(0).GetComponent<WindUpRing>().UpdateFill(holdDownTime);
						
						if(GetComponent<Player>().curHidable != null)
						{
							if(holdDownTime >= GetComponent<Player>().curHidable.GetComponent<Hidable>().hideDelay)
							{
								CheckForUnhideTime(holdDownTime);
							}
						}
					}
				}
				
				//! This will detect when the mouse button is let go (use to cancel hold timer)
				if(Input.GetMouseButtonUp(0))
				{
					if(gm.player.hidden)
					{
						Debug.LogWarning("Mouse let go");
						Debug.LogWarning(Time.time - holdStartTime + " seconds");
						
						holdStartTime = 0f;
						gm.unhidingUICanvas.transform.GetChild(0).GetComponent<Image>().fillAmount = 0f;
						gm.unhidingUICanvas.transform.GetChild(0).GetComponent<Image>().enabled = false;
					}
				}
            }


            // sprint volunteering
            /*if (sprint)
            {
                //insert animation bool
                if (!randomized)
                {
                    randomized = true;
                    transform.GetComponent<Player>().iv.RandomizePosition();
                }
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

                randomized = false;

            }*/

            //contextual running
            if (sprint)
            {
                //insert animation bool
                if (!randomized)
                {
                    randomized = true;
                    gm.inventory.RandomizePosition();
                }
                if (staminaRemaining > 0)
                {
                    staminaRemaining -= Time.fixedDeltaTime;
                }
                else
                {
                    staminaRemaining = 0;

                    sprint = false;
                }
            }
            else
            {


                randomized = false;

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

            anim.SetBool("Run", sprint);

        }


    }

    public void CheckButton()
    {
        pressedButton = true;
    }

    private void OnDisable()
    {
        haveWayPoint = false;
        enRoute = false;
        rb.velocity = new Vector2(0, rb.velocity.y);

        anim.SetBool("Move", false);

    }

    private void OnDrawGizmos()
    {

        Gizmos.color = new Color(0, 1, 0, 0.5f);
        Gizmos.DrawCube(transform.position, new Vector2((safeArea * 2), 3));

    }

    public void LoadSave(Transform savePoint, int saveNum, bool isTalisman)
    {
        savedLocation = savePoint;
        saveStationNum = saveNum;
        if(!isTalisman)
        {
            UpdateMonologue(1);
        }
        Debug.Log("GAME SAVED");
    }
	
	void CheckForUnhideTime(float holdTime)
	{
		if(holdTime >= unhideTime)
		{
			Debug.LogWarning("Hold on...");
			
			holdStartTime = 0f;
			gm.unhidingUICanvas.transform.GetChild(0).GetComponent<Image>().fillAmount = 0f;
			gm.unhidingUICanvas.transform.GetChild(0).GetComponent<Image>().enabled = false;
			
			gm.player.curHidable.GetComponent<Hidable>().Unhide();
		}
	}
	
	public float UnhideTime
	{
		get
		{
			return unhideTime;
		}
		set
		{
			unhideTime = value;
		}
	}
	
    void UpdateMonologue(int displayIndex)
    {
        switch (displayIndex)
        {
            case 1:
                gm.monologueManager.DisplaySentence(89);
                break;
            default:
                Debug.LogWarning("Save notification could not be displayed.");
                break;
        }
    }
}
