using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    //! All Puzzle Components
    [Header("Dining Puzzle")]
    public Dining_Bowl diningBowl;
    public Death_Bowl deathBowl1st;
    public Death_Bowl deathBowl2nd;
    public Safe_Bowl safeBowl3rd;
    public Death_Bowl deathBowl4th;
    public bool isDiningPuzzleClear = false;
    public bool disableDiningPuzzle = false;
    public GameObject spoonTarget;
    public bool diningForewarnPlayer = false;
    public bool diningPuzzleClearMsgTrigger = false;

    [Header("Kitchen Puzzle")]
    public Kitchen_Steamer kitchenSteamer;
    public string firstIngredientRequiredName;
    public string secondIngredientRequiredName;
    public string thirdIngredientRequiredName;
    public string fourthIngredientRequiredName;
    public bool isKitchenPuzzleClear = false;
    public bool disableKitchenPuzzle = true;
    public bool isSteamerMakingKuih = false;
    public bool isKuihReady = false;
    public int sequenceCount = 0;
    public string targetIngredient;
    public bool makingKuihOnce = false;
    public bool kitchenPuzzleClearMsgTrigger = false;
    public int makingKuihCountDownTime;

    [Header("Lounge Puzzle")]
    public Lounge_Pairs loungePairs;
    public string stoneStatueRequiredName;
    public string photoframeRequiredName;
    [Tooltip("Lounge Puzzle Completion Reward Spawn Delay")]
    public int loungePuzzleCompletionRewardSpawnDelay;
    public GameObject rewardObject;
    public bool isLoungePuzzleClear = false;
    public bool disableLoungePuzzle = true;
    public bool isStoneStatuePairComplete = false;
    public bool isCursedMirrorPairComplete = false;
    public bool isPhotoFramePairComplete = false;
    public bool loungePuzzleClearMsgTrigger = false;

    private void Awake()
    {
        disableKitchenPuzzle = true;
        disableLoungePuzzle = true;
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
