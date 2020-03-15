using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lounge_Pairs : Interactable
{
    protected PuzzleManager pm;
    protected SpriteRenderer spriteRenderer;
    protected GameObject currentHeldItem;

    public override void Awake()
    {
        base.Awake();

        pm = gm.puzzleManager;

        pm.rewardObject.SetActive(false);
    }
    
    public override void Update()
    {
        base.Update();

        if (pm.isLoungePuzzleClear)
        {
            return;
        }

        //! enable lounge puzzle only if kitchen puzzle has been completed prior
        if (pm.isKitchenPuzzleClear)
        {
            pm.disableLoungePuzzle = false;
        }
        else
        {
            return;
        }

        //! check completion conditions for all object pairs
        if (pm.isStatuePairComplete && pm.isPhotoFramePairComplete && pm.isCursedMirrorPairComplete)
        {
            SetPuzzleCompletion();
        }
    }

    private void SetPuzzleCompletion()
    {
        pm.disableLoungePuzzle = true;
        pm.isLoungePuzzleClear = true;
        StartCoroutine(SpawnLoungePuzzleReward());

        if (!pm.loungePuzzleClearMsgTrigger)
        {
            pm.loungePuzzleClearMsgTrigger = true;

            Debug.Log("lounge puzzle clear!");
        }
    }

    IEnumerator SpawnLoungePuzzleReward()
    {
        for (int i = pm.loungePuzzleCompletionRewardSpawnDelay; i > 0; i--)
        {
            Debug.Log("Reward will be delivered in " + i + " second(s)!");
            yield return new WaitForSeconds(1);
        }
        Debug.Log("Whoosh! Something has spawned in the Lounge Room");

        pm.UpdatePuzzleCompleteMonologue(3, "");

        pm.rewardObject.SetActive(true);

        gm.doorScript.SetIsLockedOnDoor(gm.doorVerticalMainToStorage, false);
    }

    public override void UpdateAudio(int index)
    {
        switch(index)
        {
            case 1:
                gm.audioManager.PlayAudio("place down item");
                break;
        }
    }
}
