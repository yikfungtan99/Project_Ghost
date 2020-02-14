using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dining_Bowl : MonoBehaviour
{
    protected Interactable it;
    protected static bool disablePuzzle;
    protected static bool isPuzzleClear;
    private bool puzzleClearDebugMsg;
    public GameObject item_ui;

    // Start is called before the first frame update
    void Awake()
    {
        isPuzzleClear = false;
        it = GetComponent<Interactable>();
        disablePuzzle = false;
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
    }

    virtual public void Interact()
    {

    }
}
