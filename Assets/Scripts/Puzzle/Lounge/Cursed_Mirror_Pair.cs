using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cursed_Mirror_Pair : Lounge_Pairs
{
    private bool isMirrorCovered = false;
    private bool triggerOnce = false;

    //! always tracks the isDisabled bool from the mirror_interactable script
    void Update()
    {
        if (!pm.isLoungePuzzleClear)
        {
            if (!pm.disableLoungePuzzle)
            {
                isMirrorCovered = GetComponent<Mirror_Interactable>().isDisabled;

                if (isMirrorCovered && !triggerOnce)
                {
                    triggerOnce = true;

                    pm.isCursedMirrorPairComplete = true;

                    UpdateMonologue(2, "");
                }
            }
        }
    }

    public override void Interact()
    {
        base.Interact();

        if (!isMirrorCovered)
        {
            UpdateMonologue(1, "");
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
