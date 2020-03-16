using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarrotPatrol : StateMachineBehaviour
{
    private float moveSpeed;

    private Transform transform;

    private Transform[] patrolSpots = new Transform[2];
    
    private int heading = 0;

    private CarrotMain mainGhost;

    /*  Jins stuff
    public Transform[] moveSpots;
    public Transform[] Set1;
    private int randomspot;
    */

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
        //Initialize everything
        mainGhost = animator.gameObject.GetComponent<CarrotMain>();
        
        moveSpeed = mainGhost.patrolSpeed;

        patrolSpots = new Transform[2];

        transform = animator.transform;

        /* Jin
        randomspot = 1;

        moveSpots = animator.gameObject.GetComponent<CarrotMain>().moveSpots;
        */

    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //now always have 2 patrol spots
        //patrol spots now always control by the roomChecker
        //patrol spots is always in the room that the ghost in

        //Debug.Log(heading);

        if (!mainGhost.stopMove)
        {
            if (!mainGhost.canChangeRoom)
            {
                //Get All the points
                if (mainGhost.patrolSpots != null)
                {
                    for (int i = 0; i < mainGhost.patrolSpots.Length; i++)
                    {

                        patrolSpots[i] = mainGhost.patrolSpots[i];

                    }

                }


                //Normal Patrol
                if (patrolSpots[0] != null)
                {
                    //DebugHUD
                    if (mainGhost.showDebug && mainGhost.debug)
                    {
                        mainGhost.debug.text = "Distance: " + Vector2.Distance(transform.position, patrolSpots[heading].localPosition).ToString("####0.00") + " Heading Spot: " + patrolSpots[heading].position;
                        
                    }

                    //Debug.Log(patrolSpots[heading].position);

                    if (patrolSpots[heading].position.x > transform.position.x)
                    {
                        transform.rotation = Quaternion.Euler(0, 0, 0);
                        transform.position = new Vector2(transform.position.x + (moveSpeed * Time.deltaTime), transform.position.y);

                    }

                    if (patrolSpots[heading].position.x < transform.position.x)
                    {
                        transform.rotation = Quaternion.Euler(0, 180, 0);
                        transform.position = new Vector2(transform.position.x - (moveSpeed * Time.deltaTime), transform.position.y);

                    }

                    //if (Vector2.Distance(transform.position, patrolSpots[heading].position) < 1f)
                   // Debug.Log(DistanceBetweenX(transform.position, patrolSpots[heading].position));

                    if(DistanceBetweenX(transform.position, patrolSpots[heading].position) < 1f)
                    {
                        if (heading == 0)
                        {

                            heading = 1;

                        }
                        else
                        {

                            heading = 0;

                        }

                    }

                }
         
            }
            else
            {

                //Head Towards the door
                if(mainGhost.doorToUse.position.x < transform.position.x)
                {
                    transform.rotation = Quaternion.Euler(0, 180, 0);
                    transform.position = new Vector2(transform.position.x - (mainGhost.chaseSpeed * Time.deltaTime), transform.position.y);

                }
                else if(mainGhost.doorToUse.position.x > transform.position.x)
                {
                    transform.rotation = Quaternion.Euler(0, 0, 0);
                    transform.position = new Vector2(transform.position.x + (mainGhost.chaseSpeed * Time.deltaTime), transform.position.y);

                }

            }

        }
        else
        {
            //Set to StaySpot

            if (!mainGhost.inTutorial)
            {
                if (mainGhost.curRoom.staySpots)
                {
                    if (!mainGhost.isCalled)
                    {

                        mainGhost.transform.position = new Vector2(mainGhost.curRoom.staySpots.position.x, mainGhost.transform.position.y);


                    }
                }

            }

        }



        /*  Jin
        animator.transform.position = Vector2.MoveTowards(animator.transform.position, moveSpots[randomspot].position, speed * Time.deltaTime);

        if (moveSpots[randomspot].position.x < animator.transform.position.x && animator.transform.eulerAngles.y == 0)
        {
            animator.transform.eulerAngles = new Vector3(0, -180, 0);
        }
        else if (moveSpots[randomspot].position.x > animator.transform.position.x && animator.transform.eulerAngles.y == 180)
        {
            animator.transform.eulerAngles = new Vector3(0, 0, 0);
        }

        if (Vector2.Distance(animator.transform.position, moveSpots[randomspot].position) < 0.2f)
        {
            randomspot = Random.Range(0, moveSpots.Length);

        }
        */
    }

    float DistanceBetweenX(Vector2 target1, Vector2 target2)
    {

        float answer;

        if(target1.x > target2.x)
        {

            answer = target1.x - target2.x;

        }
        else
        {

            answer = target2.x - target1.x;

        }

        return answer;

    }

    /* void OnTriggerEnter2D(Collider2D other)
     {
         if(other.CompareTag("Candle"))
         {
             if (other.GetComponent<Candle>().isLit == true)
             {


                 other.GetComponent<Candle>().isLit = false;

                 animator.SetBool("isPatrol", false);
                 animator.SetBool("isChase", false);
                 animator.SetBool("isIdle", true);

                 StartCoroutine(EnemyWake());





             }
         }

         else
         {
             Debug.Log("nothing to snuff");
         }

         IEnumerator EnemyWake()
         {
             //This is a coroutine
             yield return new WaitForSeconds(1);    //Wait one frame
             GetComponent<SpriteRenderer>().sprite = spriteNotLit;
             transform.GetChild(0).gameObject.SetActive(isLit);
             GameObject.FindGameObjectWithTag("Enemy").GetComponent<MainGhost>().enabled = true;

             GetComponent<SpriteRenderer>().sprite = spriteNotLit;
             transform.GetChild(0).gameObject.SetActive(isLit);


         }
     }*/

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
