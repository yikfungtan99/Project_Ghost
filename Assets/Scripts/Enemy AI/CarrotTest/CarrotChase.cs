using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarrotChase : StateMachineBehaviour
{

    private CarrotMain ghost;
    private Transform transform;
    public Transform playerPos;
    public float speed;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        ghost = animator.GetComponent<CarrotMain >();
        playerPos = GameManager.Instance.playerObject.transform;
        transform = animator.transform;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        Debug.Log("Chase");
        //Remove move towards
        //transform.position = Vector2.MoveTowards(transform.position, playerPos.position, speed * Time.deltaTime);

        if(transform.position.x < playerPos.position.x)
        {

            transform.position = new Vector2(transform.position.x + (speed * Time.deltaTime), transform.position.y);

        }
        else
        {

            transform.position = new Vector2(transform.position.x - (speed * Time.deltaTime), transform.position.y);

        }

    }

    

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
