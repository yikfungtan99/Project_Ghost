using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarrotPatrol : StateMachineBehaviour
{
    public float speed;
    public Transform[] moveSpots;
    public Transform[] Set1;
    private int randomspot;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        randomspot = 1;
        moveSpots = animator.gameObject.GetComponent<CarrotMain>().moveSpots;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
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
