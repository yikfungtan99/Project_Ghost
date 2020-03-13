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

            //! Make player faint
            gm.player.playerFainted = true;

            Destroy(gm.playerObject); // <- this has to be changed or the player reference will be lost lol
            

            //! need to wait for Master UI Canvas to be implemented before optimising the codes below
            int child = GameObject.Find("DeathCanvas").transform.childCount;
            for (int i = 0; i < child; i++)
            {
                GameObject.Find("DeathCanvas").transform.GetChild(i).gameObject.SetActive(true);
            }
        }
    }
}
