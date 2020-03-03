using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mirror_Interactable : Interactable
{
    public bool isDisabled = false;
    public bool ghostTeleporting = false;

    public int countDownTime;
    public int chanceRange;

    private void Start()
    {

    }
    public override void Interact()
    {

        base.Interact();

        //! Putting cloth on mirror
        if (!isDisabled)
        {
            if(gm.holdPanel.transform.childCount == 0)
            {
                Debug.Log("Held Item cannot be found in hand.");
                return;
            }

            GameObject heldItem = gm.holdPanel.transform.GetChild(0).gameObject;

            if (heldItem.GetComponent<Item_Inventory>().itemName == "red cloth")
            {
                isDisabled = true;

                Destroy(heldItem);
                Debug.Log("Red Cloth transferred from Inventory to Mirror");
            }
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        ChanceToTeleportGhost(col);
    }

    private void ChanceToTeleportGhost(Collider2D collider)
    {
        if (!isDisabled)
        {
            if (collider.gameObject == gm.playerObject)
            {
                gm.ghostManager.currentMirror = transform;
                Debug.Log("Whoosh! You walked past the mirror.");

                int randomChance = Random.Range(1, chanceRange);
                int target = 1;

                if (target == randomChance)
                {
                    if(!ghostTeleporting)
                    {
                        StartCoroutine(CountDown(countDownTime));
                    }
                    else
                    {
                        Debug.Log("Ghost is already teleporting!");
                    }
                }
            }
        }
    }

    IEnumerator CountDown(int countDownTime)
    {
        ghostTeleporting = true;
        
        for (int i = countDownTime; i > 0; i--)
        {
            Debug.Log("Warning! Ghost will teleport to the mirror in " + i + " second(s)!");
            yield return new WaitForSeconds(1);
        }
        SpawnGhost();

        ghostTeleporting = false;
    }
    

    void SpawnGhost()
    {
        if(isDisabled)
        {
            return;
        }

        gm.carrotMain.TeleportMirror();
        Debug.Log("HMMMMMM?");
        Debug.LogWarning("BBABAM GHOST HAS SPAWNED TO KILL YOU!!!!!");

    }
}
