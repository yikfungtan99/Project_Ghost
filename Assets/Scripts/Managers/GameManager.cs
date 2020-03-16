using System.Collections;
using System.Collections.Generic;
using UnityEngine.Experimental.Rendering.LWRP;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //! Singleton
    private static GameManager thisInstance;
    public static GameManager Instance { get { return thisInstance; } }

    //! all instances game library
    [Header("All Technical Managers")]
    public GameObject audioManagerObject;
    public GameObject monologueManagerObject;
    public GameObject roomManagerObject;
    public GameObject puzzleManagerObject;
    public GameObject pauseMenuManagerObject;
    public Canvas masterUICanvas;
    public GameObject deathScreenObject;
    public GameObject winScreenObject;
    public GameObject itemLibraryObject;
    
    public AudioManager audioManager;
    public MonologueManager monologueManager;
    public RoomManager roomManager;
    public PuzzleManager puzzleManager;
    public RealNotePickUp pauseMenuManager;
    public ItemLibrary itemLibrary;
    public MouseControls mouseControl;

    [Header("Player Components")]
    public GameObject playerObject;
    public GameObject lighterObject;
    
    public Player player;
    public Player_Interactable playerInteractable;
    public Player_Movement playerMovement;
    public Player_Inventory playerInventory;
    public Inventory inventory;
    public Player_Lighter playerLighter;
    public GameObject holdPanel;

    public Tutorial TutorialNavi;
    public Animator BagAnim;
    public Animator FadeInOutAnim;
    public bool tutorialFirst = false;
    public bool tutorialComplete = false;
    public bool tutorialSleep = false;

    [Header("Ghost/Enemy Components")] //! variables under Ghost Components is subject to change under Jin's new code
    public GameObject ghostMain;
    public GameObject ghostManagerObject;
    
    public CarrotMain carrotMain;
    public GhostManager ghostManager;
    public GameObject allMoveSpots;

    [Header("Room Components")]
    public GameObject outside;
    public GameObject mainEntrance;
    public GameObject centralCourtYard;
    public GameObject altarRoom;
    public GameObject diningRoom;
    public GameObject bridalRoom;
    public GameObject kitchen;
    public GameObject loungeRoom;
    public GameObject toilet;
    public GameObject livingRoom;
    public GameObject storageRoom;

    [Header("Literally Just Doors")]
    public Door doorScript;
    public GameObject doorHorizontalOutsideToMain;
    public GameObject doorHorizontalMainToCentral;
    public GameObject doorVerticalMainToAltar;
    public GameObject doorVerticalMainToStorage;
    public GameObject doorHorizontalCentralToLiving;
    public GameObject doorHorizontalLivingToHall3;
    public GameObject doorVerticalLivingToHall2;
    public GameObject doorVerticalLivingToHall1;
    public GameObject doorHorizontalHall3ToDining;
    public GameObject doorHorizontalHall2ToLounge;
    public GameObject doorVerticalHall1ToBridal;
    public GameObject doorHorizontalDiningToHall4;
    public GameObject doorHorizontalHall4ToKitchen;
    public GameObject doorVerticalKitchenToToilet;
    public GameObject doorHorizontalToiletToLounge;
    
    [Header("Lights")]
    public Light2D GlobalLight;
    public bool debugLight;
    public float debugLightIntensity = 1;

    [Header("Camera Components")]
    public Camera mainCamera;
    public GameObject cinemachine;
    
    [Header("Misc Game Components")]
    public GameObject eventSystem;

    //! All Global Game Variables
    [Header("Global Game Variables")]
    public bool gamePaused = false;

    [Header("Save")]
    public Vector2 playerCheckpointPosition;

    private void Awake()
    {
        //! first check if got singleton duplicate (purge all clones)
        if (thisInstance != null && thisInstance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            thisInstance = this;
        }
    }

    private void Start()
    {
        if (debugLight)
        {
            GlobalLight.intensity = debugLightIntensity;
        }
    }

    private void Update()
    {
        //! disable inventory (regardless of state) when game paused
        if(gamePaused)
        {
            DisableInventory();
        }
    }
    
    //! Player Inventory Manipulation Functions
    private void DisableInventory()
    {
        playerInventory.inventoryOn = false;
        playerInventory.inventory.transform.GetChild(0).gameObject.SetActive(playerInventory.inventoryOn);
    }

    //! Player Manipulation Functions
    public void RefreshPlayerUnpausedState()
    {
        playerMovement.enabled = !playerInventory.inventoryOn;
        playerInteractable.enabled = !playerInventory.inventoryOn;
    }
    
    //! Game Pause Manipulation Functions
    public void SetPause(bool statement)
    {
        Debug.Log("SetPause()");
        Debug.Log(gamePaused);
        gamePaused = statement;
    }

    public void StartGameScene()
    {

        Debug.Log("Next Scene");

    }

    public void TutorialGhostTrigger(bool onOff)
    {

        ghostMain.SetActive(onOff);

    }

}
