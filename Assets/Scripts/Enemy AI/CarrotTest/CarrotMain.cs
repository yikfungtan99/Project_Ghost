using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarrotMain : MonoBehaviour
{
    
    public int TalismanStunTime;
    public Transform[] moveSpots;
    public Transform[] Set1;
    public float detectRange;
    public float stopRange;
    public LayerMask Layer;
    public LayerMask NoRayLayer;
    public Animator anima;
    public GameObject player;
    RaycastHit2D hitInfo;
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
            hitInfo = Physics2D.Raycast(transform.position, transform.right, 0, NoRayLayer);
        }
        else
        {
             hitInfo = Physics2D.Raycast(transform.position, transform.right, detectRange, Layer);
        }
        

        if (hitInfo.collider != null)
        {
            Debug.DrawLine(transform.position, hitInfo.point, Color.red);
            if (hitInfo.collider.CompareTag("Player") && hitInfo.collider.gameObject.GetComponent<Player>().hidden == false)
            {
                anima.SetBool("isChase", true);
                anima.SetBool("isPatrol", false);
                gameObject.GetComponent<Renderer>().material.color = Color.red;
            }
            else
            {
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
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawRay(transform.position, transform.right * detectRange);
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

   
}
