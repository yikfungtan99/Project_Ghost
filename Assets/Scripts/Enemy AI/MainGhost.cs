using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGhost : MonoBehaviour
{
    public float speed;
    public float ChaseSpeed;
    public float detectRange;
    public float stopRange;
    public LayerMask Layer;

    private float lastX;
    private bool IsMovingRight;
    private Transform Target;
    public Transform[] moveSpots;
    private int randomspot;


    void Start()
    {
        randomspot = Random.Range(0, moveSpots.Length);
        Target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
       // Physics2D.queriesStartInColliders = false;
        lastX = transform.position.x;




    }

    void Update()
    {


        /* if (transform.position.x < lastX)
         {
             IsMovingRight = false;
             Debug.Log("Moving Left");
             lastX = transform.position.x;
         }

         else if (transform.position.x > lastX)
         {


             IsMovingRight = true;
             Debug.Log("Moving Right");
             lastX = transform.position.x;
         }

         if (IsMovingRight == true && transform.rotation.y == 180 )
         {
             transform.LookAt(transform.position + transform.right);
         }
         else if (IsMovingRight == false && transform.rotation.y == 0)
         {
             transform.LookAt(transform.position - transform.right);
         }*/



        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, transform.right, detectRange,Layer);


        if (hitInfo.collider != null)
        {
            Debug.DrawLine(transform.position, hitInfo.point, Color.red);
            if (hitInfo.collider.CompareTag("Player") && hitInfo.collider.gameObject.GetComponent<Player>().hidden == false)
            {
                Chase(true);
                gameObject.GetComponent<Renderer>().material.color = Color.red;
            }
            else
            {
                Chase(false);
                gameObject.GetComponent<Renderer>().material.color = Color.green;
            }
        }
        else
        {
            Chase(false);
            gameObject.GetComponent<Renderer>().material.color = Color.green;
            Debug.DrawLine(transform.position, transform.position + transform.right * detectRange, Color.green);

        }







        //This is the old detection system
        /*
          float distance = Mathf.Sqrt(Mathf.Pow(Target.position.x - transform.position.x, 2) + Mathf.Pow(Target.position.y - transform.position.y, 2));
         if (distance <= detectRange && distance > stopRange && PlayerMovement.hidden==false)
         {
             Chase(true);
             gameObject.GetComponent<Renderer>().material.color = Color.red;
         }
         else
         {
             Chase(false);
             gameObject.GetComponent<Renderer>().material.color = Color.green;
         }
         */





    }






    void Chase(bool check)
    {
        if (check)
        {
            transform.position = Vector2.MoveTowards(transform.position, Target.position, ChaseSpeed * Time.deltaTime);
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, moveSpots[randomspot].position, ChaseSpeed * Time.deltaTime);

            if (Vector2.Distance(transform.position, moveSpots[randomspot].position) < 0.2f)
            {
                randomspot = Random.Range(0, moveSpots.Length);

                if (moveSpots[randomspot].position.x < transform.position.x && transform.eulerAngles.y == 0)
                {
                    transform.eulerAngles = new Vector3(0, -180, 0);
                }
                else if (moveSpots[randomspot].position.x > transform.position.x && transform.eulerAngles.y == 180)
                {
                    transform.eulerAngles = new Vector3(0, 0, 0);
                }


                // transform.Rotate(0, 180f, 0);
                // transform.eulerAngles = new Vector3(0, -180, 0);

            }
        }
    }

}
