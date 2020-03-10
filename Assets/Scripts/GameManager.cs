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
    public GameObject audioManager;
    public MonologueManager monologueManager;
    private GameObject roomManagerObject;
    public RoomManager roomManager;
    public ItemLibrary itemLibrary;
    public GameObject pauseMenuManager;
    public RealNotePickUp realPauseMenuScript;
    public GameObject deathScreenManager;
    public GameObject winScreenManager;
    public GameObject puzzleManager; //! if needed
    public MouseControls mouseControl;

    [Header("Player Components")]
    public GameObject playerObject;
    public Player player;
    public Player_Interactable playerInteractable;
    public Player_Movement playerMovement;
    public Player_Inventory playerInventory;
    public Inventory inventory;
    public Player_Lighter playerLighter;
    public GameObject lighterObject;
    public GameObject holdPanel;

    [Header("Ghost/Enemy Components")] //! variables under Ghost Components is subject to change under Jin's new code
    public GameObject ghostMain;
    public CarrotMain carrotMain;
    public GameObject allMoveSpots;
    public GhostManager ghostManager;

    [Header("Room Components")]
    public GameObject mainEntrance;
    public GameObject centralCourtYard;
    public GameObject altarRoom;
    public GameObject diningRoom;
    public GameObject bridalRoom;

    [Header("Lights")]
    public Light2D GlobalLight;

    [Header("Camera Components")]
    public Camera mainCamera;
    public GameObject cinemachine;
    
    [Header("Misc Game Components")]
    public GameObject eventSystem;

    //! All Global Game Variables
    [Header("Global Game Variables")]
    public static bool gamePaused = false;
    public bool playerHide = false;

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

        roomManagerObject = transform.GetChild(0).gameObject;
        roomManager = roomManagerObject.GetComponent<RoomManager>();

    }

    private void Update()
    {
        //! disable inventory (regardless of state) when game paused
        if(gamePaused)
        {
            DisableInventory();
        }

        playerHide = player.hidden;
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
}
