using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    private GameManager gm;

    //! All Puzzle Components
    [Header("Dining Puzzle")]
    public Dining_Bowl diningBowl;
    public bool isDiningPuzzleClear = false;
    public bool disableDiningPuzzle = false;
    public GameObject spoonTarget;
    public bool diningForewarnPlayer = false;
    public bool diningPuzzleClearMsgTrigger = false;

    [Header("Kitchen Puzzle")]
    public Kitchen_Steamer kitchenSteamer;
    public GameObject kitchenGodAltar;
    public string firstIngredientRequiredName;
    public string secondIngredientRequiredName;
    public string thirdIngredientRequiredName;
    public string fourthIngredientRequiredName;
    public bool isKitchenPuzzleClear = false;
    public bool disableKitchenPuzzle = true;
    public bool isSteamerMakingKuih = false;
    public bool isKuihReady = false;
    public bool isKitchenGodEnabled = false;
    public int sequenceCount = 0;
    public string targetIngredient;
    public string kitchenGodItemRequiredName;
    public bool makingKuihOnce = false;
    public bool kitchenPuzzleClearMsgTrigger = false;
    public int makingKuihCountDownTime;

    [Header("Lounge Puzzle")]
    public Lounge_Pairs loungePairs;
    public string loungeStatueRequiredName;
    public string photoframeRequiredName;
    [Tooltip("Lounge Puzzle Completion Reward Spawn Delay")]
    public int loungePuzzleCompletionRewardSpawnDelay;
    public GameObject rewardObject;
    public bool isLoungePuzzleClear = false;
    public bool disableLoungePuzzle = true;
    public bool isStatuePairComplete = false;
    public bool isCursedMirrorPairComplete = false;
    public bool isPhotoFramePairComplete = false;
    public bool loungePuzzleClearMsgTrigger = false;

    public string stairsItemRequiredName;

    private void Awake()
    {
        gm = GameManager.Instance;

        //! Dining Puzzle Settings
        spoonTarget = null;
        gm.doorScript.SetIsLockedOnDoor(gm.doorHorizontalDiningToHall4, true);

        //! Kitchen Puzzle Settings
        disableKitchenPuzzle = true;
        gm.doorScript.SetIsLockedOnDoor(gm.doorVerticalLivingToHall2, true);
        gm.doorScript.SetIsLockedOnDoor(gm.doorVerticalKitchenToToilet, true);

        //! Lounge Puzzle Settings
        disableLoungePuzzle = true;
        gm.doorScript.SetIsLockedOnDoor(gm.doorVerticalMainToStorage, true);
    }

    public void UpdatePuzzleCompleteMonologue(int displayIndex, string itemName)
    {
        switch(displayIndex)
        {
            case 1:
                //! Dining Puzzle Complete
                gm.monologueManager.DisplaySentence(7);
                break;
            case 2:
                //! Kitchen Puzzle Complete
                gm.monologueManager.DisplaySentence(13);
                break;
            case 3:
                //! Lounge Puzzle Complete
                gm.monologueManager.DisplaySentence(20);
                break;
        }
    }
}
