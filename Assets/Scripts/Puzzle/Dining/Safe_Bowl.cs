﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Safe_Bowl : Dining_Bowl
{
    public override void Interact()
    {
        Debug.Log("Clickin Safe bowl");

        CheckForSpoon();

        if (pm.spoonTarget == null)
        {
            return;
        }

        //! Give Player key item
        if (!pm.disableDiningPuzzle)
        {
            pm.disableDiningPuzzle = true;
            pm.isDiningPuzzleClear = true;

            //! Destroy spoon object in player inventory
            if(pm.spoonTarget != null)
            {
                Destroy(pm.spoonTarget);
            }

            gm.inventory.ObtainItem("zinc key");

            pm.UpdatePuzzleCompleteMonologue(1, "");

            gm.doorScript.SetIsLockedOnDoor(gm.doorHorizontalDiningToKitchen, false);
        }
    }
}
