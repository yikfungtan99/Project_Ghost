using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    private GameManager gm;
    private Animator roomState;

    // Start is called before the first frame update
    void Start()
    {
        gm = GameManager.Instance;
        roomState = transform.GetComponent<Animator>();
    }


    public void SwitchRoom(string direction, Transform door)
    {
        //Debug.Log("Move" + direction);
        roomState.SetTrigger("Move" + direction);

        //Let Ghost know that it can change room now
        if (gm.ghostMain.GetComponent<CarrotMain>().chasing)
        {

            gm.ghostMain.GetComponent<CarrotMain>().canChangeRoom = true;
            gm.ghostMain.GetComponent<CarrotMain>().doorToUse = door;

        }

    }

   


}
