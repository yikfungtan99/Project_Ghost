using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dining_Bowl : MonoBehaviour
{
    protected Interactable it;
    protected GameObject inventory;
    protected static bool disablePuzzle;
    protected static bool isPuzzleClear;
    protected GameObject spoonTarget;
    private bool forewarnPlayer;
    private bool puzzleClearDebugMsg;
    public GameObject item_ui;

    // Start is called before the first frame update
    void Awake()
    {
        isPuzzleClear = false;
        spoonTarget = null;
        it = GetComponent<Interactable>();
        inventory = GameObject.Find("Inventory_UI").gameObject;
        disablePuzzle = false;
        forewarnPlayer = false;
        puzzleClearDebugMsg = false;
    }

    void Update()
    {
        if(isPuzzleClear && !puzzleClearDebugMsg)
        {
            puzzleClearDebugMsg = true;
            Debug.Log("Congrats! You cleared the puzzle! You obtained a 'Key' Item!");
            return;
        }

        //Debug.Log(disablePuzzle);
    }

    virtual public void Interact() {}

    void ResetPuzzleOnPlayerRespawn()
    {
        if (GameObject.Find("Player").GetComponent<Player>().playerFainted == false)
        {
            disablePuzzle = false;
            forewarnPlayer = false;
        }
    }

    //! Checks if spoon is inside player inventory.
    //  If spoon is not found, return. If spoon is found, its parent's child index will be recorded (position in inventory)
    protected void CheckForSpoon()
    {
        spoonTarget = null;

        if(isPuzzleClear)
        {
            return;
        }
        
        if (GameObject.Find("Hold Panel").transform.childCount == 0 || GameObject.Find("Hold Panel").transform.GetChild(0).GetComponent<Item_Inventory>().itemName != "spoon")
        {
            Debug.Log("You can do nothing with the bowl as it is. Maybe a spoon will help.");
            return;
        }
        else
        {
            if (!forewarnPlayer)
            {
                forewarnPlayer = true;
                Debug.Log("I think I have to be a little more cautious about this...");
            }
            else
            {
                spoonTarget = GameObject.Find("Hold Panel").transform.GetChild(0).gameObject;
                ResetPuzzleOnPlayerRespawn();
            }
        }
    }
}
