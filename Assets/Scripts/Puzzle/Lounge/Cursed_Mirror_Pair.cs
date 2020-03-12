using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cursed_Mirror_Pair : Lounge_Pairs
{
    public bool isMirrorCovered = false;
    private bool triggerOnce1 = false;

    public void LoungeMirrorInteract(bool isCalledFirst)
    {

        Debug.Log("Attempt to play Monologue index : 16");

        if (!pm.isLoungePuzzleClear)
        {
            if (!pm.disableLoungePuzzle)
            {
                isMirrorCovered = GetComponent<Mirror_Interactable>().isDisabled;

                if(!isMirrorCovered)
                {
                    if (isCalledFirst)
                    {
                        if (gm.holdPanel.transform.childCount == 0)
                        {
                            UpdateMonologue(1, "");
                        }
                    }
                    else
                    {
                        if (gm.holdPanel.transform.childCount != 0)
                        {
                            UpdateMonologue(1, "");
                        }
                    }
                }
                else if (isMirrorCovered && !triggerOnce1)
                {
                    triggerOnce1 = true;

                    pm.isCursedMirrorPairComplete = true;

                    UpdateMonologue(2, "");

                    Debug.Log("Calling Update function");
                }
            }
        }
    }

    public override void UpdateMonologue(int displayIndex, string itemName)
    {
        switch (displayIndex)
        {
            case 1:
                gm.monologueManager.DisplaySentence(16);
                break;
            case 2:
                gm.monologueManager.DisplaySentence(19);
                break;
        }
    }
}
