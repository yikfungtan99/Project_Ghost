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
    public bool isKitchenPuzzleClear = false;
    public bool disableKitchenPuzzle = true;
    public bool isSteamerMakingKuih = false;
    public bool isKuihReady = false;
    public int ingredientCount = 0;
    public string targetIngredient;
    public bool makingKuihOnce = false;
    public bool kitchenPuzzleClearMsgTrigger = false;
    public int makingKuihCountDownTime;

    [Header("Lounge Puzzle")]
    public GameObject tempSlot2;

    private void Awake()
    {
        disableKitchenPuzzle = true;
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
