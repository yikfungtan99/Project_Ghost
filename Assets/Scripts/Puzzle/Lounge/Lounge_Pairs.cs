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
    }
    
    void Update()
    {
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
        if (pm.isStoneStatuePairComplete && pm.isPhotoFramePairComplete && pm.isCursedMirrorPairComplete)
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

        UpdateMonologue(-1, "");

        pm.rewardObject.SetActive(true);
    }

    public override void UpdateMonologue(int displayIndex, string itemName)
    {
        gm.monologueManager.DisplaySentence(20);
    }
}
