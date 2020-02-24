using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost_Patrol : StateMachineBehaviour
{

    public float speed;
    public Transform[] moveSpots;
    public Transform[] Set1;
    public Transform[] Set2;
    Transform transform;

    private int randomspot;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        randomspot = 1;
        transform = GameObject.FindGameObjectWithTag("Enemy").transform;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        transform.position = Vector2.MoveTowards(transform.position, moveSpots[randomspot].position, speed * Time.deltaTime);

        if (moveSpots[randomspot].position.x < transform.position.x && transform.eulerAngles.y == 0)
        {
            transform.eulerAngles = new Vector3(0, -180, 0);
        }
        else if (moveSpots[randomspot].position.x > transform.position.x && transform.eulerAngles.y == 180)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }

        if (Vector2.Distance(transform.position, moveSpots[randomspot].position) < 0.2f)
        {
            randomspot = Random.Range(0, moveSpots.Length);
        }

    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }

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
