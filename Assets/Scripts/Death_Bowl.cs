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
            GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().playerFainted = true;

            Destroy(GameObject.FindGameObjectWithTag("Player"));

            int child = GameObject.Find("DeathCanvas").transform.childCount;
            for (int i = 0; i < child; i++)
            {
                GameObject.Find("DeathCanvas").transform.GetChild(i).gameObject.SetActive(true);
            }
        }
    }
}
