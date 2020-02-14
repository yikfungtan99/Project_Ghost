using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death_Bowl : Dining_Bowl
{
    public new void Interact()
    {
        Debug.Log("Clickin Death bowl");

        CheckForSpoon();

        if (spoonTarget == null)
        {
            return;
        }

        //! Kill Player
        if (!disablePuzzle)
        {
            disablePuzzle = true;

            //! Make player faint
            GameObject.Find("Player").GetComponent<Player>().playerFainted = true;
        }
    }
}
