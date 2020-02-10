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

    public void SwitchRoom(string direction)
    {
        Debug.Log("Move" + direction);
        roomState.SetTrigger("Move" + direction);
    }

}
