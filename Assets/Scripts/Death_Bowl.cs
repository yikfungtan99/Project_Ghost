using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death_Bowl : Dining_Bowl
{
    public new void Interact()
    {
        Debug.Log("Clickin Death bowl");
        if (!GameObject.Find("Player").GetComponent<Player>().playerFainted)
        {
            disablePuzzle = false;
        }

        //! Kill Player
        if (!disablePuzzle)
        {
            disablePuzzle = true;
            GameObject.Find("Player").GetComponent<Player>().playerFainted = true;
        }
    }
}
