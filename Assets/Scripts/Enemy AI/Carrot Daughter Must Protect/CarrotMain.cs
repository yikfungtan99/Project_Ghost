using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//delete afterwards
using TMPro;

public class CarrotMain : MonoBehaviour
{

    public float patrolSpeed = 1;
    public float chaseSpeed = 3;
    public Transform[] patrolSpots = new Transform[2];

    public bool chasing = false;

    public bool canChangeRoom = false;
    public Transform doorToUse = null;

    public int heading = 0;

    //debugHUD
    public TextMeshProUGUI debug;
    public TextMeshProUGUI debug1;
    public bool showDebug = false;

    //JIN JIN==============================
    public float TalismanStunTime;
    public float DiscoverPlayerStunTime;
    public Transform[] moveSpots;
    public Transform[] Set1;
    public float detectRange;
    public float stopRange;
    public float LightSeekingRange;
    public LayerMask Layer;
    public LayerMask LightLayer;
    public LayerMask NoRayLayer;
    public LayerMask HideLayer;
    public Animator anima;
    public GameObject player;
    public Transform CurrentLight;
    public GhostManager ghostMan;
    RaycastHit2D hitInfo;
    RaycastHit2D LightSeeker;

    public RoomChecker curRoom;
    public bool stopMove = false;

    public bool isCalled = false;

    public int vanishChance = 0;
    private bool needNewNumber = false;
    
    // Start is called before the first frame update
    void Start()
    {

        anima = GetComponent<Animator>();
        ghostMan = GameManager.Instance.ghostManager;

        vanishChance = Random.Range(0, 2);

        Debug.Log(vanishChance);
    }

    // Update is called once per frame
    void Update()
    {

        if (curRoom)
        {
            if (!isCalled)
            {
                if (!curRoom.playerInRoom && !canChangeRoom)
                {
                    stopMove = true;
                    needNewNumber = true;

                    if (vanishChance == 0 && needNewNumber)
                    {

                        vanishChance = Random.Range(0, 2);
                        transform.position = new Vector2(0, 0);
                        needNewNumber = false;
                    }

                }
                else
                {

                    stopMove = false;

                }
            }
            else
            {

                stopMove = false;

            }
            

        }


        if (debug1 != null)
        {

            if (showDebug)
            {
                debug1.text = "\nPatrol: " + anima.GetBool("isPatrol") + " ||Chase: " + anima.GetBool("isChase") + " ||Stunned: " + anima.GetBool("isIdle") + " ||LightSeeking: " + anima.GetBool("isLight");
            }

        }
       

        if (anima.GetBool("isIdle") == true)
        {
            
            hitInfo = Physics2D.Raycast(transform.position, transform.right, detectRange, NoRayLayer);
            LightSeeker = Physics2D.Raycast(transform.position, transform.right, 0, NoRayLayer);
        }
        else if(anima.GetBool("isPatrol") == true)
        {
            
            hitInfo = Physics2D.Raycast(transform.position, transform.right, detectRange, Layer);
            LightSeeker = Physics2D.Raycast(transform.position + new Vector3(0, 1f,0f), transform.right, LightSeekingRange, LightLayer);
            
        }
        else if (anima.GetBool("isChase") == true)
        {
            hitInfo = Physics2D.Raycast(transform.position, transform.right, detectRange, HideLayer);
        }
        else if (anima.GetBool("isLight") == true)
        {
           
            LightSeeker = Physics2D.Raycast(transform.position + new Vector3(0, 1f, 0f), transform.right, LightSeekingRange, LightLayer);
        }



        if (hitInfo.collider != null)
        {
            Debug.DrawLine(transform.position, hitInfo.point, Color.red);

            if (hitInfo.collider.CompareTag("Player") /*&& hitInfo.collider.gameObject.GetComponent<Player>().hidden == false*/)
            {
                
                
                chasing = true;
                anima.SetBool("isChase", true);
                anima.SetBool("isPatrol", false);
                anima.SetBool("isLight", false);
                gameObject.GetComponent<Renderer>().material.color = Color.red;
            }
            else
            {
                chasing = false;
                anima.SetBool("isChase", false);
                anima.SetBool("isPatrol", true);
                gameObject.GetComponent<Renderer>().material.color = Color.green;
            }
        }
        else if (anima.GetBool("isIdle") == false)
        {

            chasing = false;
            anima.SetBool("isChase", false);
            anima.SetBool("isPatrol", true);
            gameObject.GetComponent<Renderer>().material.color = Color.green;
            Debug.DrawLine(transform.position, transform.position + transform.right * detectRange, Color.green);
            

        }
        if(LightSeeker.collider!=null )
        {
            Debug.DrawLine(transform.position + new Vector3(0, 1f, 0f), LightSeeker.point, Color.magenta);
            if (anima.GetBool("isPatrol") == true)
            {
                

                Debug.Log("LightSpotted!");
                CurrentLight = LightSeeker.collider.GetComponentInParent<Transform>().transform;
                anima.SetBool("isPatrol", false);
                anima.SetBool("isLight", true);

                if (LightSeeker.collider.CompareTag("PlayerLight"))
                {
                    hitInfo = Physics2D.Raycast(transform.position, transform.right, detectRange, HideLayer);
                    Debug.Log("PlayerLightSpotted!");
                }

            }
            

        }
        else
        {
            Debug.DrawLine(transform.position + new Vector3(0, 1f, 0f), transform.position + transform.right * LightSeekingRange + new Vector3(0, 1f, 0f), Color.yellow);
        }

    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (player.GetComponent<Player>().hidden == false)
            {


                if (GameObject.Find("Hold Panel").transform.childCount != 0 && GameObject.Find("Hold Panel").transform.GetChild(0).GetComponent<Item_Inventory>().itemName == "talisman")
                {
                    if (anima.GetBool("isIdle") == false)
                    {
                        anima.SetBool("isPatrol", false);
                        anima.SetBool("isChase", false);
                        anima.SetBool("isIdle", true);

                        this.gameObject.GetComponent<CapsuleCollider2D>().enabled = false;
                        Destroy(GameObject.Find("Hold Panel").transform.GetChild(0).gameObject);
                        StartCoroutine(EnemyWake(TalismanStunTime));

                    }

                }
                else
                {


                    Destroy(GameObject.FindGameObjectWithTag("Player"));

                    int child = GameObject.Find("DeathCanvas").transform.childCount;
                    for (int i = 0; i < child; i++)
                    {
                        GameObject.Find("DeathCanvas").transform.GetChild(i).gameObject.SetActive(true);
                    }


                }
            }
            else if (player.GetComponent<Player>().hidden == true&&anima.GetBool("isChase")==true)
            {
                anima.SetBool("isLight", false);
                anima.SetBool("isPatrol", false);
                anima.SetBool("isChase", false);
                anima.SetBool("isIdle", true);

                this.gameObject.GetComponent<CapsuleCollider2D>().enabled = false;

                GameManager.Instance.player.curHidable.GetComponent<Hidable>().Unhide();

                /*
                foreach (GameObject fooObj in GameObject.FindGameObjectsWithTag("Hiding_Spot"))
                {
                    fooObj.GetComponent<Hidable>().Unhide();
                }
                */

                StartCoroutine(EnemyWake(DiscoverPlayerStunTime));
            }

        }

    }
    IEnumerator EnemyWake(float stuntime)
    {
        //This is a coroutine
        yield return new WaitForSeconds(stuntime);    //Wait one frame
        anima.SetBool("isPatrol", true);
        anima.SetBool("isChase", false);
        anima.SetBool("isIdle", false);

        this.gameObject.GetComponent<CapsuleCollider2D>().enabled = true;

    }

    public void TeleportTrigger()
    {

        if (anima.GetBool("isPatrol") == true || anima.GetBool("isLight") == true)
        {
            isCalled = true;
            transform.position = new Vector3(ghostMan.GetComponent<GhostManager>().currentDoor.GetChild(0).position.x, ghostMan.GetComponent<GhostManager>().currentDoor.GetChild(0).position.y, 0);
            StartCoroutine(stopMoveDelay());
        }

    }

    public void TeleportMirror()
    {
        if (anima.GetBool("isPatrol") == true || anima.GetBool("isLight") == true)
        {
            isCalled = true;
            transform.position = ghostMan.GetComponent<GhostManager>().currentMirror.position;
            StartCoroutine(stopMoveDelay());
        }

    }

    public void UpdatePlayerRoom()
    {
        if (anima.GetBool("isChasing"))
        {

            Debug.Log("I can change room!");
            canChangeRoom = true;

        }

    }

    IEnumerator stopMoveDelay()
    {
        yield return new WaitForSeconds(3);
        isCalled = false;
    }

}
