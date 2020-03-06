using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    private Animator roomState;

    // Start is called before the first frame update
    void Start()
    {
        roomState = transform.GetComponent<Animator>();
    }


    public void SwitchRoom(string direction, Transform door)
    {
        Debug.Log("Move" + direction);
        roomState.SetTrigger("Move" + direction);
        
        //return a room for the enemy to use

        //if not chasing dun do it
        //enemy.canChangedRoom
        //enemy.doorToUse = door;

    }

   
}
