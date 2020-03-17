using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death_Bowl : Dining_Bowl
{
    public override void Interact()
    {

        Debug.Log("Clickin Death bowl");

        CheckForSpoon();

        if (pm.spoonTarget == null)
        {
            return;
        }

        //! Kill Player
        if (!pm.disableDiningPuzzle)
        {
            pm.disableDiningPuzzle = true;

            if (GetComponent<pocTrigger>())
            {
                GetComponent<pocTrigger>().ActivateTrigger();
            }
        }
    }
}
