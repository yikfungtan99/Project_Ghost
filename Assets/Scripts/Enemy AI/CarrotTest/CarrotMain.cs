using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarrotMain : MonoBehaviour
{

    public float patrolSpeed = 1;
    public float chaseSpeed = 3;
    public Transform[] patrolSpots = new Transform[2];

    public bool chasing = false;

    public bool canChangeRoom = false;
    public Transform doorToUse = null;

    //JIN JIN==============================
    public int TalismanStunTime;
    public Transform[] moveSpots;
    public Transform[] Set1;
    public float detectRange;
    public float stopRange;
    public float LightSeekingRange;
    public LayerMask Layer;
    public LayerMask LightLayer;
    public LayerMask NoRayLayer;
    public Animator anima;
    public GameObject player;
    public Transform CurrentLight;
    public GhostManager ghostMan;
    RaycastHit2D hitInfo;
    RaycastHit2D LightSeeker;
    
    // Start is called before the first frame update
    void Start()
    {

        anima = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        if (anima.GetBool("isIdle") == true)
        {
            //hitInfo = Physics2D.BoxCast(transform.position, new Vector2(6,1)*2, 0f,transform.right, Layer);
            hitInfo = Physics2D.Raycast(transform.position, transform.right, detectRange, NoRayLayer);
            LightSeeker = Physics2D.Raycast(transform.position, transform.right, 0, NoRayLayer);
        }
        else
        {
            //hitInfo = Physics2D.BoxCast(transform.position, new Vector2(1, 1) * 2, 0f, transform.right, Layer);
            hitInfo = Physics2D.Raycast(transform.position, transform.right, detectRange, Layer);
            LightSeeker = Physics2D.Raycast(transform.position + new Vector3(0, 1f,0f), transform.right, LightSeekingRange, LightLayer);
            
        }
        

        if (hitInfo.collider != null)
        {
            Debug.DrawLine(transform.position, hitInfo.point, Color.red);

            if (hitInfo.collider.CompareTag("Player") && hitInfo.collider.gameObject.GetComponent<Player>().hidden == false)
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

            }
            

        }
        else
        {
            Debug.DrawLine(transform.position + new Vector3(0, 1f, 0f), transform.position + transform.right * LightSeekingRange + new Vector3(0, 1f, 0f), Color.yellow);
        }

    }
   
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
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

        }

    }
    IEnumerator EnemyWake(int stuntime)
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
        if (GameObject.Find("GhostSpawner").GetComponent<GhostManager>().Triggered == true)
        {
            transform.position = GameObject.Find("GhostSpawner").GetComponent<GhostManager>().Trigger[0].position;
            GameObject.Find("GhostSpawner").GetComponent<GhostManager>().Triggered = false;
            //moveSpots = Set2;

        }
    }

    public void TeleportMirror()
    {
        transform.position = ghostMan.GetComponent<GhostManager>().currentMirror.position;
    }

    public void UpdatePlayerRoom()
    {
        if (anima.GetBool("isChasing"))
        {

            Debug.Log("I can change room!");
            canChangeRoom = true;

        }

    }

}
