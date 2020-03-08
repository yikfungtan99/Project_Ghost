using System.Collections;
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

            UpdateMonologue(-1);
        }
    }

    public override void UpdateMonologue(int displayIndex)
    {
        gm.monologueManager.GetComponent<MonologueManager>().DisplaySentence(7);
    }
}
